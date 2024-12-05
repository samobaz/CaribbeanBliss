using Caribbean2.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Caribbean2.Models;
public class Huesped
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(70, ErrorMessage = "El nombre completo no puede tener más de 70 caracteres.")]
    public string NombreCompleto { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [StringLength(50, ErrorMessage = "El correo electrónico no puede tener más de 50 caracteres.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string CorreoElectronico { get; set; }

    [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
    [StringLength(3, ErrorMessage = "El tipo de documento no puede tener más de 3 caracteres.")]
    public string TipoDocumento { get; set; } // 'CC', 'TI', 'CE', 'PAS', 'NIT'

    [Required(ErrorMessage = "El número de identificación es obligatorio.")]
    [StringLength(20, ErrorMessage = "El número de identificación no puede tener más de 20 caracteres.")]
    [UniqueNumeroIdentificacion(ErrorMessage = "El número de identificación ya está registrado.")]
    public string NumeroIdentificacion { get; set; }

    [StringLength(15, ErrorMessage = "El número de contacto no puede tener más de 15 caracteres.")]
    public string NumeroContacto { get; set; }

    [StringLength(50, ErrorMessage = "El lugar de residencia no puede tener más de 50 caracteres.")]
    public string LugarResidencia { get; set; }

    [Required(ErrorMessage = "La fecha de llegada es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "La fecha de llegada debe ser una fecha válida.")]
    public DateTime FechaLlegada { get; set; }

    [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "La fecha de salida debe ser una fecha válida.")]
    public DateTime FechaSalida { get; set; }

    public int? IdEstadoHuesped { get; set; } = 1;

    [ForeignKey("IdEstadoHuesped")]
    public HuespedEstado EstadoHuesped { get; set; }

    // Relación muchos a muchos con Reserva
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

}
