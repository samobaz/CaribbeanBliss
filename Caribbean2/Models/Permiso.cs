using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Permiso
    {
        [Key]  // Define la clave primaria
        public int IdPermiso { get; set; }  // Identity

        [Required(ErrorMessage = "El nombre del permiso es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre del permiso no debe exceder los 100 caracteres")]
        public string NombrePermiso { get; set; }

        public string DescripcionPermiso { get; set; }  // Opcional

        // Relación N a N con Roles
        public ICollection<Rol> Roles { get; set; }
    }
}
