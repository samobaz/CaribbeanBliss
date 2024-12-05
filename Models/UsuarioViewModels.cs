using System.ComponentModel.DataAnnotations;

public class UsuarioViewModel
{
    /// <summary>
    /// Tipo de identificación del usuario (T.I, C.C, C.E).
    /// </summary>
    [Required(ErrorMessage = "El tipo de identificación es obligatorio.")]
    [RegularExpression("^(CC|TI|CE|PAS|NIT)$", ErrorMessage = "Tipo de identificación no válido.")]
    public string TipoIdentificacion { get; set; }

    /// <summary>
    /// Número de identificación, debe ser numérico y tener entre 6 y 15 dígitos.
    /// </summary>
    [Required(ErrorMessage = "El número de identificación es obligatorio.")]
    [RegularExpression("^[0-9]{6,15}$", ErrorMessage = "El número de identificación debe ser numérico entre 6 y 15 dígitos.")]
    public string Identificacion { get; set; }

    /// <summary>
    /// Nombre de usuario, debe ser alfanumérico y tener entre 6 y 30 caracteres.
    /// </summary>
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [RegularExpression("^[a-zA-Z0-9]{6,30}$", ErrorMessage = "El nombre de usuario debe ser alfanumérico y tener entre 6 y 30 caracteres.")]
    public string NombreUsuario { get; set; }

    /// <summary>
    /// Contraseña del usuario, debe tener entre 8 y 15 caracteres, incluyendo caracteres especiales permitidos.
    /// </summary>
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+=-]{8,15}$", 
        ErrorMessage = "La contraseña debe contener letras, números.")]
    public string Contrasena { get; set; }

    /// <summary>
    /// Confirmación de la contraseña, debe coincidir con la contraseña ingresada.
    /// </summary>
    [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmarContrasena { get; set; }

    /// <summary>
    /// Dirección de correo electrónico del usuario.
    /// </summary>
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z0-9]{5,}@[a-zA-Z0-9.-]+\.com$", ErrorMessage = "El correo debe ser válido, tener al menos 5 caracteres antes del @.")]
    public string Correo { get; set; }

    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe contener exactamente 10 dígitos.")]
    public string Telefono { get; set; }

    [Required]
    [Display(Name = "Rol")]
    public int Role { get; set; } = 1; // Default value
}
