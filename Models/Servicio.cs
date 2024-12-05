using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        [StringLength(30, ErrorMessage = "El nombre no debe exceder los 30 caracteres")]
        public string Nombre { get; set; }

        [StringLength(300, ErrorMessage = "La descripción no debe exceder los 300 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio del servicio es obligatorio")]
        [Column(TypeName = "money")]
        public decimal PrecioServicio { get; set; }

        [DefaultValue(true)]
        public bool EstadoServicio { get; set; } = true;

        public virtual ICollection<Reserva> Reservas { get; set; }

    
    }
}