using Backend_Medicamentos.Domain.Model;
using FluentResults;

namespace Backend_Medicamentos.Domain.Ports.Driving;

public interface IUsuarioServicio
{
    Task<Result> AñadirUsuarioAsync(string nombre,string correo, string contraseña, CancellationToken cancellationToken);

    Task<Usuario?> ObtenerUsuarioPorNombreAsync(string correo, string contraseña, CancellationToken cancellationToken);

}
