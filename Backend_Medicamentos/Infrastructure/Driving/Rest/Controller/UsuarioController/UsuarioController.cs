using Backend_Medicamentos.Domain.Ports.Driving;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.ComponentModel.DataAnnotations;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.UsuarioController;

[ApiController]
[EnableRateLimiting("Fixed")]
public class UsuarioController (IUsuarioServicio usuarioServicio,
    IJwtServicio jwtServicio, IRefreshTokenServicio refreshTokenServicio, ILogger<UsuarioController> log) : ControllerBase
{

    private readonly IUsuarioServicio _usuarioServicio = usuarioServicio;
    private readonly IJwtServicio _jwtServicio = jwtServicio;
    private readonly IRefreshTokenServicio _refreshTokenServicio = refreshTokenServicio;
    private readonly ILogger<UsuarioController> _log = log;

    [HttpPost("usuarios")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AñadirUsuario([FromBody] UsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(usuarioDto.Nombre) )
        {
            _log.LogError("Error en el nombre");
            return BadRequest("El nombre de usuario no puede estar vacío.");
        }
        if (string.IsNullOrEmpty(usuarioDto.Correo))
        {
            _log.LogError("Error en el correo");
            return BadRequest("El correo no puede estar vacío.");
        }

        if (string.IsNullOrEmpty(usuarioDto.Contrasena))
        {
            _log.LogError("Error en la contraseña");
            return BadRequest("La contraseña no puede estar vacía.");
        }

        var usuarioNuevo = UsuarioDtoMapper.ToDomain(usuarioDto);

        var result = await _usuarioServicio.AñadirUsuarioAsync(usuarioNuevo.nombre, usuarioNuevo.correo, usuarioNuevo.contrasena, cancellationToken);

        if (!result.IsSuccess)
        {
            _log.LogError("Error al añadir el usuario");
            return BadRequest(new { mensaje = result.Errors.First().Message });
        }

        _log.LogInformation("El usuario se ha añadido correctamente");
        return Ok();
    }

    [HttpGet("usuarios")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObtenerUsuarioPorNombre(string correo, string contrasena, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(correo))
        {
            _log.LogError("Error en el correo");
            return BadRequest("El nombre de usuario no puede estar vacío.");
        }
        if (string.IsNullOrEmpty(contrasena))
        {
            _log.LogError("Error en la contraseña");
            return BadRequest("La contraseña no puede estar vacía.");
        }
        var usuario = await _usuarioServicio.ObtenerUsuarioPorNombreAsync(correo, contrasena, cancellationToken);

        // 401 y el mismo mensaje tanto si el correo no existe como si la contraseña es incorrecta,
        // para no revelar qué correos están registrados
        if (usuario == null)
        {
            _log.LogError("Credenciales incorrectas");
            return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
        }

        var token = _jwtServicio.generarToken(usuario);
        var refreshToken = await _refreshTokenServicio.GenerarRefreshTokenAsync(usuario.id, cancellationToken);

        if (refreshToken.IsFailed)
        {
            return StatusCode(500, new { mensaje = "Error al generar el refresh token" });
        }

        // No se devuelve el objeto Usuario entero porque incluye el hash de la contraseña
        return Ok(new
        {
            token,
            refreshToken = refreshToken.Value,
            usuario = new { usuario.id, usuario.nombre, usuario.correo }
        });
    }
}
