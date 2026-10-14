using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Infrastructure.Driven.Database.Entity;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Mapper;

public class RefreshTokenMapper
{

    public static RefreshToken ToDomain(RefreshTokenEntity refreshTokenEntity)
    {
        return new RefreshToken(refreshTokenEntity.Id, 
            refreshTokenEntity.UsuarioId, 
            refreshTokenEntity.Token, 
            refreshTokenEntity.ExpiraEn, 
            refreshTokenEntity.CreadoEn, 
            refreshTokenEntity.Revocado);
    }

    public static RefreshTokenEntity ToEntity(RefreshToken refreshToken)
    {
        return new RefreshTokenEntity
        {
            Id = refreshToken.Id,
            UsuarioId = refreshToken.UsuarioId,
            Token = refreshToken.Token,
            ExpiraEn = refreshToken.ExpiraEn,
            CreadoEn = refreshToken.CreadoEn,
            Revocado = refreshToken.Revocado
        };
    }

}
