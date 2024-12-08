using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models
{
    public class Calificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCalificacion { get; set; }

        [Required(ErrorMessage = "La calificación es obligatoria.")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int Puntuacion { get; set; }

        [StringLength(500, ErrorMessage = "El comentario no puede tener más de 500 caracteres.")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "Debe asignar una reserva a la calificación.")]
        public int IdReserva { get; set; }
        [ForeignKey("IdReserva")]
        public virtual Reserva Reserva { get; set; }

        [Required(ErrorMessage = "Debe asignar un cliente a la calificación.")]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        public DateTime FechaCalificacion { get; set; } = DateTime.Now;
    }
}