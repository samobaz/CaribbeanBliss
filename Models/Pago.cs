using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models
{
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPago { get; set; }

        [Required(ErrorMessage = "El tipo de pago es obligatorio")]
        public string TipoPago { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El valor es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Debe especificar la reserva")]
        public int IdReserva { get; set; }

        [ForeignKey("IdReserva")]
        public virtual Reserva Reserva { get; set; }
    }
}
