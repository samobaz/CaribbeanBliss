using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Habitacion
{
    [Key]
    public int IdHabitacion { get; set; }

    [Required]
    [StringLength(20)]
    public string Nombre { get; set; }

    [StringLength(200)]
    public string Descripcion { get; set; }

    [Required]
    public int Capacidad { get; set; }

    [Required, Column(TypeName = "money")]
    public decimal PrecioHabitacion { get; set; }

    [DefaultValue(1)]
    public int IdEstado { get; set; } = 1;

    [ForeignKey("IdEstado")]
    public HabitacionEstado EstadoHabitacion { get; set; }
}
