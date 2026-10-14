using Backend_Medicamentos.Domain.Ports.Driving;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.RefreshTokenController;

[ApiController]
[EnableRateLimiting("Fixed")]
public class RefreshTokenController(IRefreshTokenServicio refreshTokenServicio,
    ILogger<RefreshTokenController> log) : ControllerBase
{
    private readonly IRefreshTokenServicio _refreshTokenServicio = refreshTokenServicio;
    private readonly ILogger<RefreshTokenController> _log = log;

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest("El refresh token es requerido");
        }

        var resultado = await _refreshTokenServicio.RefreshTokenAsync(request.RefreshToken, ct);

        if (resultado.IsFailed)
        {
            _log.LogError("Error al renovar el token");
            return Unauthorized(new { mensaje = resultado.Errors.First().Message });
        }

        return Ok(new
        {
            token = resultado.Value.jwt,
            refreshToken = resultado.Value.refreshToken
        });
    }
}
