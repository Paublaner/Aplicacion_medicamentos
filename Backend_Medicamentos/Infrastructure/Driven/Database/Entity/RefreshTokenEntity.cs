namespace Backend_Medicamentos.Infrastructure.Driven.Database.Entity;

public class RefreshTokenEntity
{
    public string Id { get; set; }
    public int UsuarioId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiraEn { get; set; }
    public DateTime CreadoEn { get; set; }
    public bool Revocado { get; set; }

    public UsuarioEntity Usuario { get; set; }
}
