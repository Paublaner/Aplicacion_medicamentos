using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Infrastructure.Driven.Database.Context;
using Backend_Medicamentos.Infrastructure.Driven.Database.Entity;
using Backend_Medicamentos.Infrastructure.Driven.Database.Mapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Adapter;

public class RefreshTokenAdapter(PostgresContext context) : IRefreshTokenPersistencia
{

    private readonly PostgresContext _context = context;

    public async Task<Result> GuardarRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct)
    {
        if (refreshToken == null)
        {
            return Result.Fail("Ha habido un  error al guardar el token");
        }

        var refreshTokenEntity = RefreshTokenMapper.ToEntity(refreshToken);
        await _context.RefreshToken.AddAsync(refreshTokenEntity, ct);
        await _context.SaveChangesAsync(ct);

        return Result.Ok();
    }

    public async Task<Result<RefreshToken?>> ObtenerPorTokenAsync(string token, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(token)){
            return Result.Fail("Ha habido un error al obtener el token");
        }

        var refreshTokenEntity = await _context.RefreshToken.FirstOrDefaultAsync(r => r.Token == token, ct);

        if (refreshTokenEntity == null)
        {
            return Result.Fail("Ha habido un error al encontrar el token");
        }

        var refreshToken = RefreshTokenMapper.ToDomain(refreshTokenEntity);

        return Result.Ok<RefreshToken?>(refreshToken);

    }

    public async Task<Result> RevocarRefreshTokenAsync(string token, CancellationToken ct)
    {
        var tokenFound = await _context.RefreshToken.FirstOrDefaultAsync(r => r.Token == token, ct);

        if (tokenFound == null)
        {
            return Result.Fail("Ha fallado algo al revocar el refresh token");
        }

        tokenFound.Revocado = true;
        tokenFound.ExpiraEn = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
