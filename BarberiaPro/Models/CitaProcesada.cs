namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CitaProcesada
    {
        [Key]
        public int IdCitaProcesada { get; set; }
        public decimal TotalPago { get; set; }
        public int Point { get; set; }
        public int IdCita { get; set; }
        public Cita? Cita { get; set; }
    }

}
