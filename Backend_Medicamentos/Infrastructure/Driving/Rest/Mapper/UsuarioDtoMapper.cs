using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Infrastructure.Driving.Rest.Controller.Dtos;

namespace Backend_Medicamentos.Infrastructure.Driving.Rest.Mapper;

public static class UsuarioDtoMapper
{
    public static Usuario ToDomain(UsuarioDto dto)
    {
        return new Usuario(dto.Nombre, dto.Correo, dto.Contrasena);
    }

    public static UsuarioDto ToDto(Usuario usuario)
    {
        return new UsuarioDto(usuario.nombre, usuario.correo, usuario.contrasena);
    }
}
