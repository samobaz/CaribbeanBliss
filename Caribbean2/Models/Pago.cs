using System;
using System.Collections.Generic;

namespace Caribbean2.Models;

public partial class Pago
{
    public int idPago { get; set; }

    public string tipo_pago { get; set; } = null!;

    public DateOnly fecha { get; set; }

    public int valor { get; set; }

    public int idReserva { get; set; }

    public virtual Reserva idReservaNavigation { get; set; } = null!;
}
