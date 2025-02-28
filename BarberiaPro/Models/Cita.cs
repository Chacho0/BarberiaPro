namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;
    using BarberiaPro.Services;

    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Estado { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdEmpleado { get; set; }
        public Empleado? Empleado { get; set; }

        // Relación muchos a muchos con Servicio
        public ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();
    }

}
