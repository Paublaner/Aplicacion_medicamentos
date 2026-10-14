using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Domain.Ports.Driving;
using FluentResults;

namespace Backend_Medicamentos.Application;

public class RefreshTokenServicio(
    IRefreshTokenPersistencia persistencia,
    IJwtServicio jwtServicio,
    IUsuarioPersistencia usuarioPersistencia,
    ILogger<RefreshTokenServicio> log) : IRefreshTokenServicio
{
    private readonly IRefreshTokenPersistencia _persistencia = persistencia;
    private readonly IJwtServicio _jwtServicio = jwtServicio;
    private readonly IUsuarioPersistencia _usuarioPersistencia = usuarioPersistencia;
    private readonly ILogger<RefreshTokenServicio> _log = log;

    public async Task<Result<string>> GenerarRefreshTokenAsync(int usuarioId, CancellationToken ct)
    {
        var refreshToken = new RefreshToken(
            Guid.NewGuid().ToString(),
            usuarioId,
            Guid.NewGuid().ToString(),
            DateTime.UtcNow.AddDays(7),
            DateTime.UtcNow,
            false);

        var resultado = await _persistencia.GuardarRefreshTokenAsync(refreshToken, ct);

        if (resultado.IsFailed)
        {
            _log.LogError("Error al guardar el refresh token");
            return Result.Fail("Error al generar el refresh token");
        }

        return Result.Ok(refreshToken.Token);
    }

    public async Task<Result<(string jwt, string refreshToken)>> RefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        var resultado = await _persistencia.ObtenerPorTokenAsync(refreshToken, ct);

        if (resultado.IsFailed || resultado.Value == null)
        {
            _log.LogError("Refresh token no encontrado");
            return Result.Fail("Refresh token inválido");
        }

        var token = resultado.Value;

        if (token.Revocado)
        {
            _log.LogError("Refresh token ya fue usado");
            return Result.Fail("Refresh token inválido");
        }

        if (token.ExpiraEn < DateTime.UtcNow)
        {
            _log.LogError("Refresh token expirado");
            return Result.Fail("Refresh token expirado");
        }

        var usuarioResultado = await _usuarioPersistencia.ObtenerUsuarioPorIdAsync(token.UsuarioId, ct);

        if (usuarioResultado.IsFailed || usuarioResultado.Value == null)
        {
            _log.LogError("Usuario del refresh token no encontrado");
            return Result.Fail("Usuario no encontrado");
        }

        var revocado = await _persistencia.RevocarRefreshTokenAsync(refreshToken, ct);

        // Si no se pudo revocar el token antiguo no se emite uno nuevo: seguiría siendo válido
        if (revocado.IsFailed)
        {
            _log.LogError("No se pudo revocar el refresh token");
            return Result.Fail("Refresh token inválido");
        }

        var nuevoRefreshResult = await GenerarRefreshTokenAsync(token.UsuarioId, ct);

        if (nuevoRefreshResult.IsFailed)
        {
            return Result.Fail("Error al renovar el refresh token");
        }

        var usuario = usuarioResultado.Value;
        var nuevoJwt = _jwtServicio.generarToken(usuario);

        _log.LogInformation("Token renovado correctamente");

        return Result.Ok((nuevoJwt, nuevoRefreshResult.Value));
    }
}
