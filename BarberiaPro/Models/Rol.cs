using System.ComponentModel.DataAnnotations;

namespace BarberiaPro.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string TipoRol { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
    public enum TipoRolEnum
    {
        Admin,
        Cliente,
        Empleado
    }
}
