using Backend_Medicamentos.Domain.Model;

namespace Backend_Medicamentos.Infrastructure.Driven.Database.Entity;

public abstract class MedicamentoEntity
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Cantidad { get; set; }
    public Medicamento.MomentoDia MomentoDia { get; set; }
}

public class LunesMedicamento : MedicamentoEntity { }
public class MartesMedicamento : MedicamentoEntity { }
public class MiercolesMedicamento : MedicamentoEntity { }
public class JuevesMedicamento : MedicamentoEntity { }
public class ViernesMedicamento : MedicamentoEntity { }
public class SabadoMedicamento : MedicamentoEntity { }
public class DomingoMedicamento : MedicamentoEntity { }
