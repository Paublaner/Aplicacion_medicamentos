using Backend_Medicamentos.Domain.Ports.Driving;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using static Backend_Medicamentos.Domain.Model.Medicamento;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.MedicamentoController;

[ApiController]
[Authorize]
[EnableRateLimiting("Fixed")]
public class MedicamentosController(IMedicamentoServicio medicamentoServicio, ILogger<MedicamentosController> log) : ControllerBase
{

    private readonly IMedicamentoServicio _medicamentoServicio = medicamentoServicio;
    private readonly ILogger<MedicamentosController> _log = log;

    [HttpPost("medicamentos")]
    public async Task<IActionResult> AñadirMedicamento([FromBody] MedicamentoDto medicamentoDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(medicamentoDto.Nombre))
        {
            _log.LogError("Error en el nombre");
            return BadRequest("El nombre del medicamento no puede estar vacío.");
        }

        if (string.IsNullOrWhiteSpace(medicamentoDto.Descripcion))
        {
            _log.LogError("Error en la descripcion");
            return BadRequest("La descripción del medicamento no puede estar vacía.");
        }

        if (!Enum.IsDefined(medicamentoDto.Cantidad))
        {
            _log.LogError("Error en la cantidad");
            return BadRequest("La cantidad no es válida.");
        }

        var errorDiaMomento = ValidarDiaYMomento(medicamentoDto.diaSemana, medicamentoDto.momentoDia);
        if (errorDiaMomento != null)
        {
            return errorDiaMomento;
        }

        _log.LogInformation(
            "nombre={Nombre} cantidad={Cantidad} dia={Dia} momento={MomentoDia}",
            medicamentoDto.Nombre, medicamentoDto.Cantidad, medicamentoDto.diaSemana, medicamentoDto.momentoDia);

        var medicamento = MedicamentoDtoMapper.ToDomain(medicamentoDto);

        var resultado = await _medicamentoServicio.AñadirMedicamentoAsync(
            medicamento.Nombre,
            medicamento.Cantidad,
            medicamento.Descripcion,
            medicamento.diaSemana,
            medicamento.momentoDia,
            cancellationToken);

        if (resultado.IsFailed)
        {
            return BadRequest(new { mensaje = resultado.Errors.First().Message });
        }

        return StatusCode(200, new
        {
            mensaje = "Medicamento guardado correctamente"
        });
    }

    [HttpGet("medicamentos/dia/{dia}")]
    public async Task<IActionResult> ObtenerMedicamentosPorDia(DiaSemana dia, MomentoDia momentoDia, CancellationToken cancellationToken)
    {
        var errorDiaMomento = ValidarDiaYMomento(dia, momentoDia);
        if (errorDiaMomento != null)
        {
            return errorDiaMomento;
        }

        var resultado =
            await _medicamentoServicio.ObtenerMedicamentosPorDiaAsync(dia, momentoDia, cancellationToken);

        if (resultado.IsFailed)
        {
            return BadRequest(new { mensaje = resultado.Errors.First().Message });
        }

        return Ok(new
        {
            medicamentos = resultado.Value
        });
    }


    [HttpDelete("medicamentos")]
    public async Task<IActionResult> EliminarMedicamento(string nombre, DiaSemana diaSemana, MomentoDia momentoDia, CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(nombre))
        {
            _log.LogError("Error en el nombre");
            return BadRequest("El nombre del medicamento no puede estar vacío.");
        }

        var errorDiaMomento = ValidarDiaYMomento(diaSemana, momentoDia);
        if (errorDiaMomento != null)
        {
            return errorDiaMomento;
        }

        var resultado = await _medicamentoServicio.EliminarMedicamentoAsync(nombre, diaSemana, momentoDia, cancellationToken);

        if (resultado.IsFailed)
        {
            return NotFound(new { mensaje = resultado.Errors.First().Message });
        }

        _log.LogInformation("Medicamento eliminado correctamente: {Nombre}", nombre);
        return Ok();
    }

    // Los enums aceptan cualquier número (p. ej. diaSemana=99), así que hay que comprobar que el valor existe
    private BadRequestObjectResult? ValidarDiaYMomento(DiaSemana dia, MomentoDia momentoDia)
    {
        if (!Enum.IsDefined(dia))
        {
            _log.LogError("Error en el dia de la semana");
            return BadRequest("El día de la semana no es válido.");
        }

        if (!Enum.IsDefined(momentoDia))
        {
            _log.LogError("Error en el momento del dia");
            return BadRequest("El momento del día no es válido.");
        }

        return null;
    }

}
