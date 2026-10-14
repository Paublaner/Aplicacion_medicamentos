using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Mapper;

public static class MedicamentoDtoMapper
{
    public static Medicamento ToDomain(MedicamentoDto dto)
    {
        return new Medicamento(
            dto.Nombre,
            dto.Cantidad,
            dto.Descripcion,
            dto.diaSemana,
            dto.momentoDia);
    }

    public static MedicamentoDto ToDto(Medicamento medicamento)
    {
        return new MedicamentoDto(
            medicamento.Nombre,
            medicamento.Descripcion,
            medicamento.Cantidad,
            medicamento.diaSemana,
            medicamento.momentoDia);
    }
}
