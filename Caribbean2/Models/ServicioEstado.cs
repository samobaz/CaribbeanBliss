using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ServicioEstado
{
    [Key]
    public int IdEstadoServicio { get; set; }

    [Required, StringLength(20)]
    public string NombreEstado { get; set; }

    public ICollection<Servicio> Servicios { get; set; }
}
