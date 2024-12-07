using System;
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Consulta
    {
        [Key]
        public int IdConsulta { get; set; }

        [Required(ErrorMessage = "El campo Nombres y Apellidos es obligatorio")]
        [Display(Name = "Nombres y Apellidos")]
        [StringLength(100)]
        public string NombresApellidos { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un email válido")]
        [Display(Name = "Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Asunto es obligatorio")]
        [StringLength(200)]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "El campo Mensaje es obligatorio")]
        [StringLength(1000)]
        public string Mensaje { get; set; }

        // Optional: Add timestamp for when the consultation was created
        [Display(Name = "Fecha de Consulta")]
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
