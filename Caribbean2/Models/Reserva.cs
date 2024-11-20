using System;
using System.Collections.Generic;

namespace Caribbean2.Models;

public partial class Reserva
{
    public int idReserva { get; set; }

    public DateOnly fecha_inicio { get; set; }

    public DateOnly? fecha_fin { get; set; }

    public decimal precio_reserva { get; set; }

    public int idCliente { get; set; }

    public int idEstado { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Cliente idClienteNavigation { get; set; } = null!;

    public virtual ReservaEstado idEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Habitacion> idHabitacions { get; set; } = new List<Habitacion>();

    public virtual ICollection<Servicio> idServicios { get; set; } = new List<Servicio>();
}
