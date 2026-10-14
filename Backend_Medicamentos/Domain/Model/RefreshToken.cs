namespace Backend_Medicamentos.Domain.Model;

public class RefreshToken
{

    public string Id { get; set; }
    public int UsuarioId { get; set; }
    public string Token {  get; set; }
    public DateTime ExpiraEn {  get; set; }
    public DateTime CreadoEn { get; set; }
    public bool Revocado { get; set; }


    public RefreshToken(string id, int usuarioId, string token, DateTime expiraEn, DateTime creadoEn, bool revocado)
    {
        Id = id;
        UsuarioId = usuarioId;
        Token = token;
        ExpiraEn = expiraEn;
        CreadoEn = creadoEn;
        Revocado = revocado;
    }



}
