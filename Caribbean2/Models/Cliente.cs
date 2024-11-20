using System;
using System.Collections.Generic;

namespace Caribbean2.Models;

public partial class Cliente
{
    public int idCliente { get; set; }

    public string nombre { get; set; } = null!;

    public string? email { get; set; }

    public string? telefono { get; set; }

    public int? idRol { get; set; }

    public int? idEstado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ClienteEstado? idEstadoNavigation { get; set; }

    public virtual Role? idRolNavigation { get; set; }
}
