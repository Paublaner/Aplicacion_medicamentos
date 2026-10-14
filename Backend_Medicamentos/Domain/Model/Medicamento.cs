using System.ComponentModel;

namespace Backend_Medicamentos.Domain.Model;

public class Medicamento
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public OpciónCantidad Cantidad { get; set; }
    public DiaSemana diaSemana { get; set; }
    public MomentoDia momentoDia { get; set; }

    public enum OpciónCantidad
    {
        [Description("1 pastilla")]
        Una,
        [Description("Media pastilla")]
        Media,
        [Description("Diez miligramos")]
        DiezMiligramos = 10,
        [Description("Veinte miligramos")]
        VeinteMiligramos = 20
    }

    public enum DiaSemana
    {
        Lunes,
        Martes,
        Miercoles,
        Jueves,
        Viernes,
        Sabado,
        Domingo
    }

    public enum MomentoDia
    {
        Desayuno,
        Comida,
        Cena
    }

    public Medicamento(string nombre, OpciónCantidad cantidad, string descripcion, DiaSemana diaSemana, MomentoDia momentoDia)
    {
        Nombre = nombre;
        Cantidad = cantidad;
        Descripcion = descripcion;
        this.diaSemana = diaSemana;
        this.momentoDia = momentoDia;
    }
}
