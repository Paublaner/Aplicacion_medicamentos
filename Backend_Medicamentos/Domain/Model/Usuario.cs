namespace Backend_Medicamentos.Domain.Model
{
    public class Usuario
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }


        public Usuario(string nombre,string correo, string contrasena)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.contrasena = contrasena;
        }
    }
}
