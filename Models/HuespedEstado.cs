using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models;

public class HuespedEstado
{
    [Key]
    public int IdEstadoHuesped { get; set; }

    [Required, StringLength(20)]
    public string NombreEstado { get; set; }

    public ICollection<Huesped> Huespedes { get; set; }
}
