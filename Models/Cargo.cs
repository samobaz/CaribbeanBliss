using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models;

public partial class Cargo
{
    [Key]
    public int idCargo { get; set; }

    [Required]
    [StringLength(50)]
    public string nombre { get; set; } = null!;

    public string descripcion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
