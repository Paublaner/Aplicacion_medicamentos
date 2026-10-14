using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Infrastructure.Driven.Database.Context;
using Backend_Medicamentos.Infrastructure.Driven.Database.Mapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Adapter;

public class UsuarioAdapter (PostgresContext context) : IUsuarioPersistencia
{
    private readonly PostgresContext _context = context;

    public async Task<Result> AñadirUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        if (usuario == null)
        {
            return Result.Fail("El usuario no puede ser nulo");
        }

        var entity = UsuarioMapper.ToEntity(usuario);

        var existingEntity = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == usuario.nombre || u.Correo == usuario.correo, cancellationToken);

        if (existingEntity != null)
        {
            return Result.Fail("El usuario ya existe");
        }

        _context.Usuarios.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<Usuario?>> ObtenerUsuarioPorNombreAsync(string correo, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(correo) )
        {
            return Result.Fail<Usuario?>("El nombre de usuario y la contraseña no pueden estar vacíos");
        }

        var entity = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo, cancellationToken);

        if (entity == null)
        {
            return Result.Fail<Usuario?>("Usuario no encontrado");
        }

        var usuario = UsuarioMapper.ToDomain(entity);

        return Result.Ok<Usuario?>(usuario);
    }

    public async Task<Result<Usuario?>> ObtenerUsuarioPorIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (entity == null)
        {
            return Result.Fail<Usuario?>("Usuario no encontrado");
        }

        var usuario = UsuarioMapper.ToDomain(entity);

        return Result.Ok<Usuario?>(usuario);
    }
}
