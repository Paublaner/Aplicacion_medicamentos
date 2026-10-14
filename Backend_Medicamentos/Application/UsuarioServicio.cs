using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Domain.Ports.Driving;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Backend_Medicamentos.Application
{
    public class UsuarioServicio(IUsuarioPersistencia usuarioPersistencia, ILogger<UsuarioServicio> log) : IUsuarioServicio
    {

        private readonly IUsuarioPersistencia _usuarioPersistencia = usuarioPersistencia;
        private readonly ILogger<UsuarioServicio> _log = log;
        private readonly PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

        public async Task<Result> AñadirUsuarioAsync(string nombre,string correo, string contrasena, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(nombre))
            {
                _log.LogError("El nombre no puede estar vacío");
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                _log.LogError("La contraseña no puede estar vacía.");
                throw new ArgumentException("La contraseña no puede estar vacía.");
            }

            if (string.IsNullOrEmpty(correo))
            {
                _log.LogError("El correo no puede estar vacío.");
                throw new ArgumentException("El correo no puede estar vacío.");
            }          

            Usuario usuario = new Usuario(nombre,correo, contrasena);

            Result<Usuario?> result = await _usuarioPersistencia.ObtenerUsuarioPorNombreAsync(correo, cancellationToken);

            if (result.IsSuccess && result.Value != null)
            {
                _log.LogError("El usuario ya existe.");
                return Result.Fail("El usuario ya existe.");
            }

            usuario.contrasena = passwordHasher.HashPassword(usuario, contrasena);

            Result addResult = await _usuarioPersistencia.AñadirUsuarioAsync(usuario, cancellationToken);

            if (!addResult.IsSuccess)
            {
                _log.LogError("Error al añadir el usuario.");
                // Se devuelven los errores del adaptador para no perder el motivo (p. ej. "El usuario ya existe")
                return Result.Fail(addResult.Errors);
            }

            return Result.Ok();

        }

        public async Task<Usuario?> ObtenerUsuarioPorNombreAsync(string correo, string contrasena, CancellationToken cancellationToken)
        {

            var emailValidator = new EmailAddressAttribute();

            if (string.IsNullOrEmpty(correo) || !emailValidator.IsValid(correo))
            {
                _log.LogError("El correo no es válido.");
                throw new ArgumentException("El correo no es válido.");
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                _log.LogError("La contraseña no puede estar vacía.");
                throw new ArgumentException("La contraseña no puede estar vacía.");
            }

            Result<Usuario?> result = await _usuarioPersistencia.ObtenerUsuarioPorNombreAsync(correo, cancellationToken);

            if (result.IsFailed || result.Value == null)
            {
                return null;
            }

            var usuario = result.Value;

            var verificacion = passwordHasher.VerifyHashedPassword(usuario, usuario.contrasena, contrasena);

            if (verificacion == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return usuario;
        }
    }
}
