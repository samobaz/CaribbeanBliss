using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models
{
    public partial class ReservaEstado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del estado no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; } = null!;

        // Relación con la tabla Reserva
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
