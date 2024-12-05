using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "Debe asignar un cliente a la reserva.")]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Debe asignar una habitación a la reserva.")]
        public int IdHabitacion { get; set; }
        [ForeignKey("IdHabitacion")]
        public virtual Habitacion Habitacion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El número de personas es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de personas debe ser mayor a 0")]
        public int NumeroPersonas { get; set; }

        [Required(ErrorMessage = "El precio total es obligatorio.")]
        public decimal PrecioTotal { get; set; }

        [Required(ErrorMessage = "El anticipo es obligatorio.")]
        public decimal Anticipo { get; set; }

        public string Notas { get; set; }

        // Relación muchos a muchos con Huesped
        [Required(ErrorMessage = "Debe seleccionar al menos un huésped")]
        [MinLength(1, ErrorMessage = "Debe seleccionar al menos un huésped")]
        public virtual ICollection<Huesped> Huespedes { get; set; } = new List<Huesped>();

        // Relación muchos a muchos con Servicio
        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

        // Relación con ReservaEstado
        [Required(ErrorMessage = "Debe asignar un estado a la reserva.")]
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public virtual ReservaEstado Estado { get; set; }

        // Relación uno a muchos con Pago
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
