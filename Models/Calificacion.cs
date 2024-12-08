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

        [Required(ErrorMessage = "La puntuación es obligatoria")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5")]
        [Display(Name = "Puntuación")]
        public int Puntuacion { get; set; }

        [StringLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres")]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "La reserva es obligatoria")]
        [Display(Name = "Reserva")]
        public int IdReserva { get; set; }

        [ForeignKey("IdReserva")]
        public virtual Reserva Reserva { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [Display(Name = "Fecha de Calificación")]
        public DateTime FechaCalificacion { get; set; } = DateTime.Now;

        [Display(Name = "Estado")]
        public bool EstadoCalificacion { get; set; } = true;
    }
}