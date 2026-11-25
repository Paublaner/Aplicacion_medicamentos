using Backend_Medicamentos.Domain.Ports.Driving;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.ComponentModel.DataAnnotations;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.UsuarioController;

[ApiController]
[EnableRateLimiting("limite-por-usuario")]
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

    [HttpPost("usuarios/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginUsuarios([FromBody] LoginUsuarioDto loginDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(loginDto.Correo))
        {
            _log.LogError("Error en el correo");
            return BadRequest("El correo no puede estar vacío.");
        }
        if (string.IsNullOrEmpty(loginDto.Contrasena))
        {
            _log.LogError("Error en la contraseña");
            return BadRequest("La contraseña no puede estar vacía.");
        }
        var usuario = await _usuarioServicio.ObtenerUsuarioPorNombreAsync(loginDto.Correo, loginDto.Contrasena, cancellationToken);
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

        //No devuelve usuario entero porque tiene el Hash
        return Ok(new
        {
            token,
            refreshToken = refreshToken.Value,
            usuario = new { usuario.id, usuario.nombre, usuario.correo }
        });
    }
}
