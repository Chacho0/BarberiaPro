namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }
        public string TipoServicio { get; set; }
        public decimal Precio { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        // Relación muchos a muchos con Cita
        public ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();
    }

}
