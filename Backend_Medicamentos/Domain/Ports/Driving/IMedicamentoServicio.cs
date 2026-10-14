using Backend_Medicamentos.Domain.Model;
using FluentResults;
using static Backend_Medicamentos.Domain.Model.Medicamento;

namespace Backend_Medicamentos.Domain.Ports.Driving;

public interface IMedicamentoServicio
{

    Task<Result> AñadirMedicamentoAsync(string nombre, OpciónCantidad cantidad, string descripcion, DiaSemana diaSemana, MomentoDia momentoDia, CancellationToken cancellationToken);

    Task<Result<Medicamento[]>> ObtenerMedicamentosPorDiaAsync(DiaSemana dia, MomentoDia momentoDia, CancellationToken cancellationToken);

    Task<Result> EliminarMedicamentoAsync(string nombre, DiaSemana dia, MomentoDia momentoDia, CancellationToken cancellationToken);

    Task<Result<Medicamento>> BuscarMedicamentoPorNombreAsync(string nombre, DiaSemana dia, CancellationToken cancellationToken);
}
