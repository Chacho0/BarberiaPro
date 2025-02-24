namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Estado { get; set; }  
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdEmpleado { get; set; }
        public Empleado? Empleado { get; set; }
        public int IdServicio { get; set; }
        public Servicio? Servicio { get; set; }
    }

}
