using System;
using System.Collections.Generic;

namespace Caribbean2.Models;

public partial class ClienteEstado
{
    public int idEstado { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
