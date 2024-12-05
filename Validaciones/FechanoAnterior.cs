using System;
using System.ComponentModel.DataAnnotations;

public class FechaNoAnteriorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateOnly fecha)
        {
            // Compara la fecha de llegada con la fecha actual
            if (fecha < DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("La fecha de llegada no puede ser anterior a la fecha actual.");
            }
        }
        return ValidationResult.Success;
    }
}
