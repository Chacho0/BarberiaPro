namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class HistoriaCliente
    {
        [Key]
        public int IdHistory { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdCitaProcesada { get; set; }
        public CitaProcesada? CitaProcesada { get; set; }
    }

}
