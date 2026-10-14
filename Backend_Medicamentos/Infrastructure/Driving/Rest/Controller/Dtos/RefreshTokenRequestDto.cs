namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; }
    public int UsuarioId { get; set; }
}
