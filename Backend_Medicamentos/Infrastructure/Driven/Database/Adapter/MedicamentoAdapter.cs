using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Infrastructure.Driven.Database.Context;
using Backend_Medicamentos.Infrastructure.Driven.Database.Entity;
using Backend_Medicamentos.Infrastructure.Driven.Database.Mapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using static Backend_Medicamentos.Domain.Model.Medicamento;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Adapter;

public class MedicamentoAdapter (PostgresContext context) : IMedicamentoPersistencia
{
    private readonly PostgresContext _context = context;


    public async Task<Result> AñadirMedicamento(Medicamento medicamento, CancellationToken cancellationToken)
    {

        if (medicamento == null)
        {
            return Result.Fail("El medicamento no puede ser nulo.");
        }

        var entity = MedicamentoMapper.ToEntity(medicamento);
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<Medicamento[]>> ObtenerMedicamentosPorDia(Medicamento.DiaSemana dia, Medicamento.MomentoDia momentoDia, CancellationToken cancellationToken)
    {
        var entidades = await ObtenerEntidadesDelDia(dia, cancellationToken);

        // null = día que no existe. Una lista vacía es un resultado válido (ese día no hay medicamentos)
        if (entidades == null)
        {
            return Result.Fail($"Día de la semana no válido: {dia}");
        }

        var medicamentos = entidades
            .Select(MedicamentoMapper.ToDomain)
            .Where(m => m.momentoDia == momentoDia)
            .ToArray();

        return Result.Ok(medicamentos);
    }

    public async Task<Result<Medicamento>> BuscarMedicamentoPorNombre(string nombre, Medicamento.DiaSemana dia, CancellationToken cancellationToken)
    {
        var entidades = await ObtenerEntidadesDelDia(dia, cancellationToken);

        if (entidades == null)
        {
            return Result.Fail($"Día de la semana no válido: {dia}");
        }

        var medicamento = entidades
            .Select(MedicamentoMapper.ToDomain)
            .FirstOrDefault(m => m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (medicamento == null)
        {
            return Result.Fail($"No se encontró el medicamento '{nombre}' para el día {dia}.");
        }

        return Result.Ok(medicamento);
    }


    public async Task<Result> EliminarMedicamento(string nombre, Medicamento.DiaSemana dia, Medicamento.MomentoDia momentoDia, CancellationToken cancellationToken)
    {
        var medicamentos = await ObtenerMedicamentosPorDia(dia, momentoDia, cancellationToken);

        if (medicamentos.IsFailed)
        {
            return Result.Fail(medicamentos.Errors);
        }

        var medicamentoAEliminar = medicamentos.Value
            .FirstOrDefault(m => m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (medicamentoAEliminar == null)
        {
            return Result.Fail("No se encontró el medicamento con el nombre, día y momento especificados.");
        }

        // FindAsync recibe las claves en un array; si se le pasa (id, cancellationToken) sueltos,
        // interpreta el token como una segunda clave y lanza una excepción
        object[] clave = [medicamentoAEliminar.Id];

        MedicamentoEntity? entidad = dia switch
        {
            DiaSemana.Lunes => await _context.Lunes.FindAsync(clave, cancellationToken),
            DiaSemana.Martes => await _context.Martes.FindAsync(clave, cancellationToken),
            DiaSemana.Miercoles => await _context.Miercoles.FindAsync(clave, cancellationToken),
            DiaSemana.Jueves => await _context.Jueves.FindAsync(clave, cancellationToken),
            DiaSemana.Viernes => await _context.Viernes.FindAsync(clave, cancellationToken),
            DiaSemana.Sabado => await _context.Sabado.FindAsync(clave, cancellationToken),
            DiaSemana.Domingo => await _context.Domingo.FindAsync(clave, cancellationToken),
            _ => null
        };

        if (entidad == null)
        {
            return Result.Fail("No se encontró el medicamento con el nombre, día y momento especificados.");
        }

        _context.Remove(entidad);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    private async Task<MedicamentoEntity[]?> ObtenerEntidadesDelDia(Medicamento.DiaSemana dia, CancellationToken cancellationToken)
    {
        return dia switch
        {
            Medicamento.DiaSemana.Lunes => await _context.Lunes.ToArrayAsync(cancellationToken),
            Medicamento.DiaSemana.Martes => await _context.Martes.ToArrayAsync(cancellationToken),
            Medicamento.DiaSemana.Miercoles => await _context.Miercoles.ToArrayAsync(cancellationToken),
            Medicamento.DiaSemana.Jueves => await _context.Jueves.ToArrayAsync(cancellationToken),
            Medicamento.DiaSemana.Viernes => await _context.Viernes.ToArrayAsync(cancellationToken),
            Medicamento.DiaSemana.Sabado => await _context.Sabado.ToArrayAsync(cancellationToken),
            Medicamento.DiaSemana.Domingo => await _context.Domingo.ToArrayAsync(cancellationToken),
            _ => null
        };
    }

}
