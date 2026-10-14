using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Infrastructure.Driven.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Context;

public class PostgresContext : DbContext
{

    public DbSet<UsuarioEntity> Usuarios { get; set; }
    public DbSet<LunesMedicamento> Lunes { get; set; }
    public DbSet<MartesMedicamento> Martes { get; set; }
    public DbSet<MiercolesMedicamento> Miercoles { get; set; }
    public DbSet<JuevesMedicamento> Jueves { get; set; }
    public DbSet<ViernesMedicamento> Viernes { get; set; }
    public DbSet<SabadoMedicamento> Sabado { get; set; }
    public DbSet<DomingoMedicamento> Domingo { get; set; }

    public DbSet<RefreshTokenEntity> RefreshToken { get; set; }

    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<UsuarioEntity>()
         .ToTable("Usuarios");
        // Tabla independiente por cada día — sin herencia TPH/TPT
        modelBuilder.Entity<LunesMedicamento>().ToTable("Lunes");
        modelBuilder.Entity<MartesMedicamento>().ToTable("Martes");
        modelBuilder.Entity<MiercolesMedicamento>().ToTable("Miercoles");
        modelBuilder.Entity<JuevesMedicamento>().ToTable("Jueves");
        modelBuilder.Entity<ViernesMedicamento>().ToTable("Viernes");
        modelBuilder.Entity<SabadoMedicamento>().ToTable("Sabado");
        modelBuilder.Entity<DomingoMedicamento>().ToTable("Domingo");
        modelBuilder.Entity<RefreshTokenEntity>(entity =>
        {
            entity.ToTable("RefreshToken");
            entity.HasOne(r => r.Usuario)
                  .WithMany()
                  .HasForeignKey(r => r.UsuarioId);
        });

        // MomentoDia se guarda como texto en PostgreSQL
        var tiposHijos = new[]
        {
            typeof(LunesMedicamento), typeof(MartesMedicamento), typeof(MiercolesMedicamento),
            typeof(JuevesMedicamento), typeof(ViernesMedicamento), typeof(SabadoMedicamento),
            typeof(DomingoMedicamento)
        };

        foreach (var tipo in tiposHijos)
        {
            modelBuilder.Entity(tipo)
                .Property(nameof(MedicamentoEntity.MomentoDia))
                .HasConversion<string>();
        }


        modelBuilder.Entity<LunesMedicamento>().HasData(
            new LunesMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new LunesMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new LunesMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new LunesMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new LunesMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new LunesMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new LunesMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new LunesMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new LunesMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new LunesMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new LunesMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new LunesMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new LunesMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new LunesMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

        modelBuilder.Entity<MartesMedicamento>().HasData(
            new MartesMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MartesMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MartesMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MartesMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MartesMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MartesMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new MartesMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new MartesMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new MartesMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MartesMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MartesMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MartesMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MartesMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MartesMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

        modelBuilder.Entity<MiercolesMedicamento>().HasData(
            new MiercolesMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MiercolesMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MiercolesMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MiercolesMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MiercolesMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new MiercolesMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new MiercolesMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new MiercolesMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new MiercolesMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MiercolesMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MiercolesMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MiercolesMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MiercolesMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new MiercolesMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

        modelBuilder.Entity<JuevesMedicamento>().HasData(
            new JuevesMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new JuevesMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new JuevesMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new JuevesMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new JuevesMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new JuevesMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new JuevesMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new JuevesMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new JuevesMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new JuevesMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new JuevesMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new JuevesMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new JuevesMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new JuevesMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

        modelBuilder.Entity<ViernesMedicamento>().HasData(
            new ViernesMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new ViernesMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new ViernesMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new ViernesMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new ViernesMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new ViernesMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new ViernesMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new ViernesMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new ViernesMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new ViernesMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new ViernesMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new ViernesMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new ViernesMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new ViernesMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

        modelBuilder.Entity<SabadoMedicamento>().HasData(
            new SabadoMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new SabadoMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new SabadoMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new SabadoMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new SabadoMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new SabadoMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new SabadoMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new SabadoMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new SabadoMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new SabadoMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new SabadoMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new SabadoMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new SabadoMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new SabadoMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

        modelBuilder.Entity<DomingoMedicamento>().HasData(
            new DomingoMedicamento { Id = 1, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new DomingoMedicamento { Id = 2, Nombre = "Potasion", Descripcion = "Suplemento de potasio", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new DomingoMedicamento { Id = 3, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new DomingoMedicamento { Id = 4, Nombre = "Jardiance", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new DomingoMedicamento { Id = 5, Nombre = "Omeprazol", Descripcion = "Protección del estómago", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Desayuno },
            new DomingoMedicamento { Id = 6, Nombre = "Furosemida", Descripcion = "Diurético para eliminar líquidos", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new DomingoMedicamento { Id = 7, Nombre = "Clopidogrel", Descripcion = "Prevención de coágulos", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Comida },
            new DomingoMedicamento { Id = 8, Nombre = "Acitrecina", Descripcion = "Tratamiento de problemas de piel", Cantidad = "Diez miligramos", MomentoDia = Medicamento.MomentoDia.Comida },
            new DomingoMedicamento { Id = 9, Nombre = "Metformina", Descripcion = "Control de la glucosa", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new DomingoMedicamento { Id = 10, Nombre = "Amlodipino", Descripcion = "Control de la presión arterial", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new DomingoMedicamento { Id = 11, Nombre = "Atorvastatina", Descripcion = "Control del colesterol", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new DomingoMedicamento { Id = 12, Nombre = "Zebinix", Descripcion = "Control de las crisis epilépticas", Cantidad = "1 pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new DomingoMedicamento { Id = 13, Nombre = "Pregabalina", Descripcion = "Tratamiento del dolor nervioso", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena },
            new DomingoMedicamento { Id = 14, Nombre = "Mirtazapina", Descripcion = "Tratamiento de la depresión", Cantidad = "Media pastilla", MomentoDia = Medicamento.MomentoDia.Cena }
        );

    }
}
