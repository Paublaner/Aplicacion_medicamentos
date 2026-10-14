using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Infrastructure.Driven.Database.Entity;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Mapper;

public class UsuarioMapper
{

    public static Usuario ToDomain(UsuarioEntity usuarioEntity)
    {
        return new Usuario(
            nombre: usuarioEntity.Nombre,
            correo : usuarioEntity.Correo,
            contrasena: usuarioEntity.Contrasena
        )
        {
            id = usuarioEntity.Id
        };
    }

    public static UsuarioEntity ToEntity(Usuario usuario)
    {
        return new UsuarioEntity
        {
            Id = usuario.id,
            Nombre = usuario.nombre,
            Correo = usuario.correo,
            Contrasena = usuario.contrasena
        };
    }

}
