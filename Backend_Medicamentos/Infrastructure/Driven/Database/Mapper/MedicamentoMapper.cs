using Backend_Medicamentos.Domain.Model;
using Backend_Medicamentos.Infrastructure.Driven.Database.Entity;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Mapper;

public class MedicamentoMapper
{
    public static Medicamento ToDomain(MedicamentoEntity medicamentoEntity)
    {
        // Se infiere el día de la semana a partir del tipo concreto de la entidad
        var diaSemana = medicamentoEntity switch
        {
            LunesMedicamento     => Medicamento.DiaSemana.Lunes,
            MartesMedicamento    => Medicamento.DiaSemana.Martes,
            MiercolesMedicamento => Medicamento.DiaSemana.Miercoles,
            JuevesMedicamento    => Medicamento.DiaSemana.Jueves,
            ViernesMedicamento   => Medicamento.DiaSemana.Viernes,
            SabadoMedicamento    => Medicamento.DiaSemana.Sabado,
            DomingoMedicamento   => Medicamento.DiaSemana.Domingo,
            _ => throw new ArgumentException($"Tipo de entidad desconocido: {medicamentoEntity.GetType().Name}")
        };

        var medicamento = new Medicamento(
            nombre:     medicamentoEntity.Nombre,
            cantidad:   LeerCantidad(medicamentoEntity.Cantidad),
            descripcion: medicamentoEntity.Descripcion,
            diaSemana:  diaSemana,
            momentoDia: medicamentoEntity.MomentoDia
        );

        medicamento.Id = medicamentoEntity.Id;

        return medicamento;
    }

    /// <summary>
    /// En la BD la cantidad se guarda como texto. Lo normal es el nombre del enum ("Una"),
    /// pero hay filas antiguas con el texto descriptivo ("Una pastilla", "1 pastilla"...).
    /// Antes un Enum.Parse a secas lanzaba una excepción y una sola fila así rompía la lista del día entero.
    /// </summary>
    private static Medicamento.OpciónCantidad LeerCantidad(string texto)
    {
        // 1) Nombre del enum: "Una", "Media", "DiezMiligramos"... (ignoreCase por si acaso)
        if (Enum.TryParse(texto, ignoreCase: true, out Medicamento.OpciónCantidad cantidad)
            && Enum.IsDefined(cantidad))
        {
            return cantidad;
        }

        // 2) Texto descriptivo de las filas antiguas
        return texto.Trim().ToLowerInvariant() switch
        {
            "una pastilla" or "1 pastilla"                => Medicamento.OpciónCantidad.Una,
            "media pastilla" or "1/2 pastilla"            => Medicamento.OpciónCantidad.Media,
            "diez miligramos" or "10 mg" or "10mg"        => Medicamento.OpciónCantidad.DiezMiligramos,
            "veinte miligramos" or "20 mg" or "20mg"      => Medicamento.OpciónCantidad.VeinteMiligramos,
            _ => throw new InvalidOperationException($"Cantidad desconocida guardada en la base de datos: '{texto}'")
        };
    }

    public static MedicamentoEntity ToEntity(Medicamento medicamento)
    {
        // Se instancia la subclase concreta según el día de la semana
        MedicamentoEntity entity = medicamento.diaSemana switch
        {
            Medicamento.DiaSemana.Lunes     => new LunesMedicamento(),
            Medicamento.DiaSemana.Martes    => new MartesMedicamento(),
            Medicamento.DiaSemana.Miercoles => new MiercolesMedicamento(),
            Medicamento.DiaSemana.Jueves    => new JuevesMedicamento(),
            Medicamento.DiaSemana.Viernes   => new ViernesMedicamento(),
            Medicamento.DiaSemana.Sabado    => new SabadoMedicamento(),
            Medicamento.DiaSemana.Domingo   => new DomingoMedicamento(),
            _ => throw new ArgumentException($"DiaSemana desconocido: {medicamento.diaSemana}")
        };

        entity.Id          = medicamento.Id;
        entity.Nombre      = medicamento.Nombre;
        entity.Descripcion = medicamento.Descripcion;
        entity.Cantidad    = medicamento.Cantidad.ToString();
        entity.MomentoDia  = medicamento.momentoDia;

        return entity;
    }
}
