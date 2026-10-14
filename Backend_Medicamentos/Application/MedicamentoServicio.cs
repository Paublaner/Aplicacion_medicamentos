using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Domain.Ports.Driving;
using FluentResults;

namespace Backend_Medicamentos.Application;

public class MedicamentoServicio(IMedicamentoPersistencia persistencia, ILogger<MedicamentoServicio> log) : IMedicamentoServicio
{

    private readonly IMedicamentoPersistencia _persistencia = persistencia;
    private readonly ILogger<MedicamentoServicio> _log = log;

    public async Task<Result> AñadirMedicamentoAsync(string nombre, Medicamento.OpciónCantidad cantidad, string descripcion, Medicamento.DiaSemana diaSemana, Medicamento.MomentoDia momentoDia, CancellationToken cancellationToken)
    {
        Medicamento medicamento = new Medicamento(nombre, cantidad, descripcion, diaSemana, momentoDia);
        var resultado = await _persistencia.AñadirMedicamento(medicamento, cancellationToken);

        if (resultado.IsFailed)
        {
            _log.LogError("No se ha podido añadir el medicamento {Nombre}", nombre);
        }

        return resultado;
    }

    public async Task<Result> EliminarMedicamentoAsync(
        string nombre,
        Medicamento.DiaSemana dia,
        Medicamento.MomentoDia momentoDia,
        CancellationToken cancellationToken)
    {
        // La búsqueda la hace el adaptador; antes se buscaba aquí y otra vez allí, con criterios distintos
        var resultado = await _persistencia.EliminarMedicamento(nombre, dia, momentoDia, cancellationToken);

        if (resultado.IsFailed)
        {
            _log.LogError("No se encontró el medicamento {Nombre} para el día {Dia} y momento {MomentoDia}", nombre, dia, momentoDia);
        }

        return resultado;
    }

    public async Task<Result<Medicamento[]>> ObtenerMedicamentosPorDiaAsync(Medicamento.DiaSemana dia, Medicamento.MomentoDia momentoDia, CancellationToken cancellationToken)
    {
        var medicamentos = await _persistencia.ObtenerMedicamentosPorDia(dia, momentoDia, cancellationToken);

        if (medicamentos.IsFailed)
        {
            _log.LogError("No se han podido obtener los medicamentos del día {Dia}", dia);
        }

        return medicamentos;
    }

    public async Task<Result<Medicamento>> BuscarMedicamentoPorNombreAsync(string nombre, Medicamento.DiaSemana dia, CancellationToken cancellationToken)
    {
        var medicamento = await _persistencia.BuscarMedicamentoPorNombre(nombre, dia, cancellationToken);

        if (medicamento.IsFailed)
        {
            _log.LogError("Ha habido un error al obtener el medicamento {Nombre}", nombre);
        }

        return medicamento;
    }
}
