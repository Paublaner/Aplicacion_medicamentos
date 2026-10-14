using Backend_Medicamentos.Application;
using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Domain.Ports.Driving;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Backend_Medicamentos.Test;

public class TestUsuariosUnitarios
{
    [Fact]
    public async Task AñadirUsuarioAsync_CamposValidos_LlamaPersistenciaAsync()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();


        mockPersistencia.Setup(p => p.AñadirUsuarioAsync(It.IsAny<Usuario>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result.Ok());

        mockPersistencia.Setup(p => p.ObtenerUsuarioPorNombreAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result.Fail<Usuario?>("Usuario no encontrado"));

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);



       await servicio.AñadirUsuarioAsync("Pablo", "usuario@ejemplo.com", "Clave1234", CancellationToken.None);

        mockPersistencia.Verify(p =>
            p.AñadirUsuarioAsync(
                It.Is<Usuario>(u => u.correo == "usuario@ejemplo.com"
                                 && u.nombre == "Pablo"
                                 && u.contrasena != "Clave1234"
                                 && u.contrasena.Length > 20), 
                It.IsAny<CancellationToken>()
            ), Times.Once);


    }

    [Fact]
    public async Task AñadirUsuarioAsync_NombreVacio_LanzaExcepcion()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => servicio.AñadirUsuarioAsync("", "usuario@ejemplo.com", "Clave1234", CancellationToken.None));

        Assert.Contains("nombre", exception.Message, StringComparison.OrdinalIgnoreCase);

    }


    [Fact]
    public async Task AñadirUsuarioAsync_ContrasenaVacia_LanzaExcepcion()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => servicio.AñadirUsuarioAsync("Pablo", "usuario@ejemplo.com", "", CancellationToken.None));

        Assert.Contains("contraseña", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ObtenerUsuarioPorNombreAsync_UsuarioExiste_RetornaUsuario()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();
        
        var hashService = new PasswordHasher<Usuario>();

        var usuarioEsperado = new Usuario("Pablo", "usuario@ejemplo.com", "Clave1234");

        usuarioEsperado.contrasena = hashService.HashPassword(usuarioEsperado, "Clave1234");

        mockPersistencia.Setup(p => p.ObtenerUsuarioPorNombreAsync(It.IsAny<String>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuarioEsperado);

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);

        var usuario = await servicio.ObtenerUsuarioPorNombreAsync("usuario@ejemplo.com", "Clave1234", CancellationToken.None);

        Assert.Equal(usuarioEsperado, usuario);

        mockPersistencia.Verify(p => p.ObtenerUsuarioPorNombreAsync("usuario@ejemplo.com", It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task ObtenerUsuarioPorNombreAsync_ContrasenaIncorrecta_RetornaNull()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();

        var hashService = new PasswordHasher<Usuario>();

        var usuarioEsperado = new Usuario("Pablo", "usuario@ejemplo.com", "Clave1234.");

        usuarioEsperado.contrasena = hashService.HashPassword(usuarioEsperado, "Clave1234");

        mockPersistencia.Setup(p => p.ObtenerUsuarioPorNombreAsync(It.IsAny<String>(), It.IsAny<CancellationToken>())).ReturnsAsync(usuarioEsperado);

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);

        var usuario = await servicio.ObtenerUsuarioPorNombreAsync("usuario@ejemplo.com", "contraseñaMala", CancellationToken.None);

        Assert.Null(usuario);

        mockPersistencia.Verify(p => p.ObtenerUsuarioPorNombreAsync("usuario@ejemplo.com", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AñadirUsuarioAsync_CorreoVacio_LanzaExcepcion()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => servicio.AñadirUsuarioAsync("Pablo", "", "Clave1234", CancellationToken.None));

        Assert.Contains("correo", exception.Message, StringComparison.OrdinalIgnoreCase);
        
    }

    [Fact]
    public async Task ObtenerUsuarioPorNombreAsync_CorreoInvalido_LanzaExcepcion()
    {
        var mockPersistencia = new Mock<IUsuarioPersistencia>();
        var mockLogger = new Mock<ILogger<UsuarioServicio>>();

        var servicio = new UsuarioServicio(mockPersistencia.Object, mockLogger.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => servicio.ObtenerUsuarioPorNombreAsync("Pablo", "Clave1234", CancellationToken.None));

        Assert.Contains("correo", exception.Message, StringComparison.OrdinalIgnoreCase);

    }

}
