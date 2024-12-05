using System;
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models;

// Models/ResetPasswordModel.cs
public class ResetPasswordModel
{
    [Required(ErrorMessage = "El token es requerido")]
    public string Token { get; set; }

    [Required(ErrorMessage = "El correo electrónico es requerido")]
    [EmailAddress(ErrorMessage = "Por favor ingrese un correo electrónico válido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La nueva contraseña es requerida")]
    [StringLength(15, MinimumLength = 8, 
        ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9]{8,15}$",
        ErrorMessage = "La contraseña solo puede contener letras y números")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "La confirmación de contraseña es requerida")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; }
}
