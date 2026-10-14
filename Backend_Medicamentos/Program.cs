using Backend_Medicamentos.Application;
using Backend_Medicamentos.Domain.Ports.Driven;
using Backend_Medicamentos.Domain.Ports.Driving;
using Backend_Medicamentos.Infrastructure.Driven.Database.Adapter;
using Backend_Medicamentos.Infrastructure.Driven.Database.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMedicamentoServicio, MedicamentoServicio>();
builder.Services.AddScoped<IMedicamentoPersistencia, MedicamentoAdapter>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IUsuarioPersistencia, UsuarioAdapter>();
builder.Services.AddScoped<IJwtServicio, JwtServicio>();
builder.Services.AddScoped<IRefreshTokenServicio, RefreshTokenServicio>();
builder.Services.AddScoped<IRefreshTokenPersistencia, RefreshTokenAdapter>();
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
        else
        {
            policy.WithOrigins("https://app.flutterflow.io")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    });
});


builder.Services.AddRateLimiter(options =>
{ 
    options.AddFixedWindowLimiter("Fixed", options =>
    {
        options.PermitLimit = 100; // Número máximo de solicitudes permitidas
        options.Window = TimeSpan.FromMinutes(1); // Ventana de tiempo
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0; // No permitir solicitudes en cola
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// El manejador de excepciones va primero para que capture los errores de todo lo que viene detrás
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        var (statusCode, mensaje) = exception switch
        {
            ArgumentException => (400, exception.Message),
            KeyNotFoundException => (404, "Recurso no encontrado"),
            _ => (500, "Ocurrió un error interno")
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            mensaje,
            detalle = app.Environment.IsDevelopment() ? exception?.Message : null
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// En desarrollo no se redirige a https: el móvil llama por http://IP-del-PC:5241 y el https
// solo escucha en localhost (y su certificado de desarrollo no es de confianza en el teléfono)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
// CORS antes de la autenticación: si no, las peticiones preflight (OPTIONS) del navegador
// llegan sin token, se rechazan con 401 y el front recibe un error de CORS
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();
app.Run();
