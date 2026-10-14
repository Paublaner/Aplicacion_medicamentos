using Backend_Medicamentos.Domain.Model;
using FluentResults;
using static Backend_Medicamentos.Domain.Model.Medicamento;

namespace Backend_Medicamentos.Domain.Ports.Driven;

public interface IMedicamentoPersistencia
{
    Task<Result> AñadirMedicamento(Medicamento medicamento, CancellationToken cancellationToken);
    Task<Result<Medicamento[]>> ObtenerMedicamentosPorDia(DiaSemana dia, MomentoDia momentoDia, CancellationToken cancellationToken);
    Task<Result> EliminarMedicamento(string nombre, DiaSemana dia, MomentoDia momentoDia, CancellationToken cancellationToken);

    Task<Result<Medicamento>> BuscarMedicamentoPorNombre(string nombre, DiaSemana dia, CancellationToken cancellationToken);
}
