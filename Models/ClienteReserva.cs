using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models
{
    public class ClienteReserva : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClienteReserva { get; set; }

        [Required(ErrorMessage = "Los Nombres y Apellidos son obligatorios")]
        [MaxLength(50, ErrorMessage = "El nombre es muy largo")]
        [Display(Name = "Nombres y Apellidos")]
        [MinLength(5, ErrorMessage = "El nombre es muy corto")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "Debe ingresar al menos 1 huésped")]
        public string Huespedes { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [StringLength(3, ErrorMessage = "El tipo de documento no puede tener más de 3 caracteres.")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número del documento no puede tener más de 20 caracteres.")]
        public string NumeroDocumento { get; set; }

        public DateOnly fechaReserva { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "La fecha de llegada es obligatoria.")]
        [FechaNoAnterior]
        public DateOnly fechaLlegada { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        public DateOnly fechaSalida { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (fechaSalida < fechaLlegada)
            {
                yield return new ValidationResult(
                    "La fecha de salida no puede ser anterior a la fecha de llegada.",
                    new[] { nameof(fechaSalida) }
                );
            }
        }

        private static readonly Random random = new Random();
        public int IdHabitacion { get; set; } = random.Next(101, 410);
        public string TipodeHabitacion { get; set; }
    }
}
