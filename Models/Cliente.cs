using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caribbean2.Models;

public partial class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idCliente { get; set; }

    public string nombre { get; set; } = null!;

    public string email { get; set; }

    public string telefono { get; set; }

    public bool ClienteEstado { get; set; } = true;

    public int idRol { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public Rol idRolNavigation { get; set; }
}
