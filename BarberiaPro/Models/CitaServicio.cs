namespace BarberiaPro.Models
{
    public class CitaServicio
    {
        public int IdCita { get; set; }
        public Cita Cita { get; set; }

        public int IdServicio { get; set; }
        public Servicio Servicio { get; set; }
    }

}
