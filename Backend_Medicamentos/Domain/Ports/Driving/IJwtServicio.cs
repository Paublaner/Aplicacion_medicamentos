using Backend_Medicamentos.Domain.Model;

namespace Backend_Medicamentos.Domain.Ports.Driving;

public interface IJwtServicio
{

    public string generarToken(Usuario usuario);

}
