namespace BarberiaPro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cargo
    {
        [Key]
        public int IdCargo { get; set; }
        public string DescripcionCargo { get; set; }
    }

}
