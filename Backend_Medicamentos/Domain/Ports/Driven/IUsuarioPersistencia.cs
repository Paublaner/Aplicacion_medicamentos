using Backend_Medicamentos.Domain.Model;
using FluentResults;

namespace Backend_Medicamentos.Domain.Ports.Driven
{
    public interface IUsuarioPersistencia
    {

        Task<Result<Usuario?>> ObtenerUsuarioPorNombreAsync(string correo, CancellationToken cancellationToken);
        Task<Result<Usuario?>> ObtenerUsuarioPorIdAsync(int id, CancellationToken cancellationToken);
        Task<Result> AñadirUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
    }
}
