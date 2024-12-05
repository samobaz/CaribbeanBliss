using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; } // Identificador único del usuario

        [Required(ErrorMessage = "Los nombres y apellidos son obligatorios.")]
        [StringLength(150, ErrorMessage = "El nombre completo no puede exceder los 150 caracteres.")]
        public string NombresApellidos { get; set; } // Nombres y apellidos

        [Required(ErrorMessage = "El tipo de identificación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de identificación no puede exceder los 50 caracteres.")]
        public string TipoIdentificacion { get; set; } // Tipo de identificación (Cédula, Pasaporte, etc.)

        [Required(ErrorMessage = "El número de identificación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de identificación no puede exceder los 50 caracteres.")]
        public string Identificacion { get; set; } // Número de identificación

        [Phone(ErrorMessage = "El número de teléfono no tiene un formato válido.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        public string Telefono { get; set; } // Teléfono del usuario

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Correo { get; set; } // Correo electrónico del usuario

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(15, ErrorMessage = "La contraseña no puede exceder los 255 caracteres.")]
        public string Contrasena { get; set; } // Contraseña (debe ser encriptada antes de insertar)

        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now; // Fecha de registro del usuario

        public bool Estado { get; set; } = true; // Estado del usuario (1 = Activo, 0 = Inactivo)

        public int IdRol { get; set; }  // Aquí guardamos el ID del rol (1 a 4)

        public Rol Rol { get; set; }  // Relación con la tabla de Roles

        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpiry { get; set; }
    }
}
