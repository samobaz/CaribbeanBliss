using System;
using System.Collections.Generic;

namespace Caribbean2.Models;

public partial class ReservaEstado
{
    public int idEstado { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
