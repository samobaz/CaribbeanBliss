using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Rol
    {
        [Key]  // Define la clave primaria
        public int IdRol { get; set; }  // Identity

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres")]
        public string NombreRol { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La descripción no debe exceder los 200 caracteres")]
        public string DescripcionRol { get; set; }  // Nueva propiedad para descripción del rol

        public bool EstadoRol { get; set; }  // Activo/Inactivo

        // Relación 1 a N con Empleados
        public ICollection<Empleado> Empleados { get; set; }

        // Relación N a N con Permisos
        public ICollection<Permiso> Permisos { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
