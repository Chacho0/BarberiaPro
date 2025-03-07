namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CitaProcesada
    {
        [Key]
        public int IdCitaProcesada { get; set; }
        public decimal TotalPago { get; set; }
        public int Point { get; set; }
        public string? EstadoPago { get; set; } = "Pendiente"; // Estado por defecto como string

        public bool Seleccionada { get; set; } = false;
        public int IdCita { get; set; }
        public Cita? Cita { get; set; }

        // Nuevos campos para el voucher y número de transferencia
        public string? NumeroTransferencia { get; set; }
        public string? VoucherPath { get; set; } // Ruta del voucher (imagen)
        public string? Observacion { get; set; }
    }

}
