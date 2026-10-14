using Backend_Medicamentos.Application;
using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using FluentResults;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Backend_Medicamentos.Test;

public class MedicamentoServicioTests
{

    [Fact]
    public async Task AñadirMedicamentoAsync_Valido_LlamaPersistencia()
    {

        var mockPersistencia = new Mock<IMedicamentoPersistencia>();
        var mockLogger = new Mock<ILogger<MedicamentoServicio>>();

        mockPersistencia.Setup(p => p.AñadirMedicamento(It.IsAny<Medicamento>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Ok());

        var servicio = new MedicamentoServicio(mockPersistencia.Object, mockLogger.Object);

        Medicamento medicamento = new Medicamento("Paracetamol", Medicamento.OpciónCantidad.Una, "Descripcion", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno);

        CancellationToken ct = new CancellationToken();

        await servicio.AñadirMedicamentoAsync("Paracetamol", Medicamento.OpciónCantidad.Una, "Descripcion", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno, ct);

        mockPersistencia.Verify(p => p.AñadirMedicamento(
            It.Is<Medicamento>(u => u.Nombre == "Paracetamol" 
                                    && u.Cantidad == Medicamento.OpciónCantidad.Una
                                    && u.Descripcion == "Descripcion"
                                    && u.diaSemana == Medicamento.DiaSemana.Sabado
                                    && u.momentoDia == Medicamento.MomentoDia.Desayuno
                                    ), 
            It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task ObtenerMedicamentosPorDiaAsync_RetornaArray()
    {
        var mockPersistencia = new Mock<IMedicamentoPersistencia>();
        var mockLogger = new Mock<ILogger<MedicamentoServicio>>();

        Medicamento[] medicamentos = new Medicamento[] { };

        mockPersistencia.Setup(m => m.ObtenerMedicamentosPorDia(It.IsAny<Medicamento.DiaSemana>(),It.IsAny<Medicamento.MomentoDia>(), It.IsAny<CancellationToken>())).ReturnsAsync(medicamentos);

        var servicio = new MedicamentoServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.ObtenerMedicamentosPorDiaAsync(Medicamento.DiaSemana.Lunes, Medicamento.MomentoDia.Desayuno, CancellationToken.None);

        Assert.True(resultado.IsSuccess);
        Assert.Equal(medicamentos, resultado.Value);

        mockPersistencia.Verify(m => m.ObtenerMedicamentosPorDia(Medicamento.DiaSemana.Lunes, Medicamento.MomentoDia.Desayuno, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ObtenerMedicamentosPorDiaAsync_DiaSinMedicamentos_RetornaArrayVacio()
    {
        var mockPersistencia = new Mock<IMedicamentoPersistencia>();
        var mockLogger = new Mock<ILogger<MedicamentoServicio>>();

        mockPersistencia.Setup(m => m.ObtenerMedicamentosPorDia(Medicamento.DiaSemana.Sabado,
                Medicamento.MomentoDia.Desayuno,
                It.IsAny<CancellationToken>())).ReturnsAsync(Result.Ok(Array.Empty<Medicamento>()));

        var servicio = new MedicamentoServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado =  await servicio.ObtenerMedicamentosPorDiaAsync(Medicamento.DiaSemana.Sabado,
                Medicamento.MomentoDia.Desayuno, CancellationToken.None);

        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Value);
    }

    [Fact]
    public async Task ObtenerMedicamentosPorDiaAsync_PersistenciaFalla_DevuelveFallo()
    {
        var mockPersistencia = new Mock<IMedicamentoPersistencia>();
        var mockLogger = new Mock<ILogger<MedicamentoServicio>>();

        mockPersistencia.Setup(m => m.ObtenerMedicamentosPorDia(Medicamento.DiaSemana.Sabado,
                Medicamento.MomentoDia.Desayuno,
                It.IsAny<CancellationToken>())).ReturnsAsync(Result.Fail<Medicamento[]>("Error en persistencia"));

        var servicio = new MedicamentoServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.ObtenerMedicamentosPorDiaAsync(Medicamento.DiaSemana.Sabado,
                Medicamento.MomentoDia.Desayuno, CancellationToken.None);

        Assert.True(resultado.IsFailed);
    }

    [Fact]
    public async Task EliminarMedicamentoAsync_Encontrado_LlamaEliminar()
    {
        var mockPersistencia = new Mock<IMedicamentoPersistencia>();
        var mockLogger = new Mock<ILogger<MedicamentoServicio>>();

        mockPersistencia
            .Setup(m => m.EliminarMedicamento("Paracetamol", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok());

        var servicio = new MedicamentoServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarMedicamentoAsync("Paracetamol", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno, CancellationToken.None);

        Assert.True(resultado.IsSuccess);
        mockPersistencia.Verify(m => m.EliminarMedicamento("Paracetamol", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno, CancellationToken.None), Times.Once);

    }

    [Fact]
    public async Task EliminarMedicamentoAsync_NoEncontrado_DevuelveFallo()
    {
        var mockPersistencia = new Mock<IMedicamentoPersistencia>();
        var mockLogger = new Mock<ILogger<MedicamentoServicio>>();

        mockPersistencia
            .Setup(m => m.EliminarMedicamento("Paracetamol", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("No se encontró el medicamento con el nombre, día y momento especificados."));

        var servicio = new MedicamentoServicio(mockPersistencia.Object, mockLogger.Object);

        var resultado = await servicio.EliminarMedicamentoAsync("Paracetamol", Medicamento.DiaSemana.Sabado, Medicamento.MomentoDia.Desayuno, CancellationToken.None);

        Assert.True(resultado.IsFailed);

    }


}
