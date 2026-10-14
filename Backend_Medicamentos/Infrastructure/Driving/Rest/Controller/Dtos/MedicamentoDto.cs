using Backend_Medicamentos.Domain.Model;
using System.ComponentModel.DataAnnotations;
using static Backend_Medicamentos.Domain.Model.Medicamento;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;

public class MedicamentoDto
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    [Required(ErrorMessage = "La cantidad es requerida.")]
    public OpciónCantidad Cantidad { get; set; }

    [Required(ErrorMessage = "El dia de la semana es requerido.")]
    public DiaSemana diaSemana { get; set; }

    [Required(ErrorMessage = "El momento del dia es requerido")]
    public MomentoDia momentoDia { get; set; }

    public MedicamentoDto(string nombre, string descripcion, OpciónCantidad cantidad, DiaSemana diaSemana, MomentoDia momentoDia)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Cantidad = cantidad;
        this.diaSemana = diaSemana;
        this.momentoDia = momentoDia;
    }
}
