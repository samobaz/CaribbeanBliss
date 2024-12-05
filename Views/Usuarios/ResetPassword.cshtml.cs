

// Models/ForgotPasswordViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo válido")]
        public string Email { get; set; }
    }
}
