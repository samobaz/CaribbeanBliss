using System.ComponentModel.DataAnnotations;

public class Comodidad
{
    [Key]
    public int IdComodidad { get; set; }

    [Required, StringLength(30)]
    public string Nombre { get; set; }

    [StringLength(100)]
    public string Descripcion { get; set; }
}
