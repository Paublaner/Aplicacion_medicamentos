using Backend_Medicamentos.Domain.Model;
using FluentResults;

namespace Backend_Medicamentos.Domain.Ports.Driving;

public interface IRefreshTokenServicio
{
    Task<Result<string>> GenerarRefreshTokenAsync(int usuarioId, CancellationToken ct);
    Task<Result<(string jwt, string refreshToken)>> RefreshTokenAsync(string refreshToken, CancellationToken ct);

}
