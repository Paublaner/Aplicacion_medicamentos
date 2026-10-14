namespace Backend_Medicamentos.Infrastructure.Driven.Database.Entity;

public class UsuarioEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;
    public string Contrasena { get; set; } = null!;
}
