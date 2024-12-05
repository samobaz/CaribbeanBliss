using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Suscripcion
    {
        [Key]  // Define la clave primaria
        public int IdSuscripcion { get; set; }  // Identity

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email no válido")]
        public string Email { get; set; }

        public DateTime FechaSuscripcion { get; set; }
    }
}

