using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Servicio
{
    [Key]
    public int IdServicio { get; set; }

    [Required, StringLength(30)]
    public string Nombre { get; set; }

    [StringLength(300)]
    public string Descripcion { get; set; }

    [Required, Column(TypeName = "money")]
    public decimal PrecioServicio { get; set; }

    [DefaultValue(1)]
    public int? IdEstadoServicio { get; set; }

    [ForeignKey("IdEstadoServicio")]
    public ServicioEstado EstadoServicio { get; set; }
}
