namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Perfil
    {
        [Key]
        public int IdPerfil { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Foto { get; set; } // Ruta de la foto

        // Relación uno a uno con Usuario
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }

}
