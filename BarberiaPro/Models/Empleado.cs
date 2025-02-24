namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; } = false;
        public int IdCargo { get; set; }
        public Cargo? Cargo { get; set; }

        // Clave foránea hacia Usuario
        public int IdUsuario { get; set; }

        // Relación uno a uno con Usuario
        public Usuario? Usuario { get; set; }
    }

}
