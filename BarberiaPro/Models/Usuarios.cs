using Microsoft.AspNetCore.Identity;

namespace BarberiaPro.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string PassWord { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        // Relación uno a uno con Empleado
        public Empleado? Empleado { get; set; }

        // Relación uno a uno con PerfilCliente
        public Perfil? PerfilCliente { get; set; }
    }

}
