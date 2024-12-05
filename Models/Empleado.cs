using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Empleado
    {
        [Key]  // Define la clave primaria
        public int IdEmpleado { get; set; }  // Identity

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string NombreEmpleado { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email no válido")]
        public string EmailEmpleado { get; set; }

        public bool EstadoEmpleado { get; set; }  // Activo/Inactivo

        // Relación 1 a N con Rol
        [Required]
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}

