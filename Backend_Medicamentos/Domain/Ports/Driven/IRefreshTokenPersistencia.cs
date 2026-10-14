using Backend_Medicamentos.Domain.Model;
using FluentResults;

namespace Backend_Medicamentos.Domain.Ports.Driven;

public interface IRefreshTokenPersistencia
{
    Task<Result> GuardarRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct);
    Task<Result<RefreshToken?>> ObtenerPorTokenAsync(string token, CancellationToken ct);
    Task<Result> RevocarRefreshTokenAsync(string token, CancellationToken ct);

}
