using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models;

public partial class Boletin
{
    [Key]
    public int idEmail { get; set; }

    public string email { get; set; } = null!;
}
