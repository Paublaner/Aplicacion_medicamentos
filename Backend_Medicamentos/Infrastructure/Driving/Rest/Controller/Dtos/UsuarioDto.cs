using System.ComponentModel.DataAnnotations;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;

public class UsuarioDto
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    public string Nombre { get; set; }
    [EmailAddress]
    public string Correo { get; set; }
    [Required(ErrorMessage = "La contraseña es requerida.")]

    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "La contraseña no puede exceder 100 caracteres.")]
    public string Contrasena { get; set; }

    public UsuarioDto()
    {
    }

    public UsuarioDto(string nombre, string correo, string contrasena)
    {
        Nombre = nombre;
        Correo = correo;
        Contrasena = contrasena;
    }
}
