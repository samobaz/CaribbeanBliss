using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Caribbean2.Models;

public class Habitacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdHabitacion { get; set; }

    [Required(ErrorMessage = "El nombre de la habitación es obligatorio")]
    [StringLength(20, ErrorMessage = "El nombre no debe exceder los 20 caracteres")]
    public string Nombre { get; set; }

    [StringLength(200, ErrorMessage = "La descripción no debe exceder los 200 caracteres")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "La capacidad es obligatoria")]
    public int Capacidad { get; set; }

    [Required(ErrorMessage = "El número de la habitación es obligatorio")]
    public int NumeroHabitacion { get; set; }

    [Required(ErrorMessage = "El precio de la habitación es obligatorio")]
    [Column(TypeName = "money")]
    public decimal PrecioHabitacion { get; set; }

    [DefaultValue(1)]
    public int IdEstado { get; set; } = 1;

    [ForeignKey("IdEstado")]
    public HabitacionEstado EstadoHabitacion { get; set; }
    public virtual ICollection<Reserva> Reservas { get; set; }
}
