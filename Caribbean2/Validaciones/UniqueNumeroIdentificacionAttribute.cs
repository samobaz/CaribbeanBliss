using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models; // Asegúrate de usar tu namespace correcto

namespace Caribbean2.Validations
{
    public class UniqueNumeroIdentificacionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Obtiene el contexto de la base de datos
            var context = (CaribbeanContext)validationContext.GetService(typeof(CaribbeanContext));

            // Obtiene el valor del campo
            var numeroIdentificacion = value?.ToString();

            // Valida si existe otro huésped con el mismo número de identificación
            var existe = context.Huespedes.Any(h => h.NumeroIdentificacion == numeroIdentificacion);

            if (existe)
            {
                return new ValidationResult("Ya existe un huésped registrado con este número de identificación.");
            }

            return ValidationResult.Success;
        }
    }
}
