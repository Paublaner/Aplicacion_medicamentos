namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;

public class LoginUsuarioDto
{

    public string Correo { get; set; }

    public string Contrasena { get; set; }

    public LoginUsuarioDto(string correo, string contrasena)
    {
        Correo = correo;
        Contrasena = contrasena;
    }

}
