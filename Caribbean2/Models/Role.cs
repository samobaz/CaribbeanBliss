using System;
using System.Collections.Generic;

namespace Caribbean2.Models;

public partial class Role
{
    public int idRol { get; set; }

    public string nombre { get; set; } = null!;

    public int idPermiso { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Permiso idPermisoNavigation { get; set; } = null!;
}
