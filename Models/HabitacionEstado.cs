using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class HabitacionEstado
{
    [Key]
    public int IdEstado { get; set; }

    [Required, StringLength(20)]
    public string Nombre { get; set; } 

    public ICollection<Habitacion> Habitaciones { get; set; }
}
