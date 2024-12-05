using System.ComponentModel.DataAnnotations;

namespace Caribbean2.Models
{
    public class Metrica
    {
        [Key]  // Define la clave primaria
        public int IdMetrica { get; set; }  // Identity

        [Required]
        public DateTime Fecha { get; set; }  // Fecha asociada a las métricas

        [Range(0, double.MaxValue)]
        public decimal IngresosTotales { get; set; }

        [Range(0, 100)]
        public double TasaOcupacion { get; set; }

        [Range(0, int.MaxValue)]
        public int OcupacionDiaria { get; set; }

        [Range(0, int.MaxValue)]
        public int OcupacionSemanal { get; set; }

        [Range(0, int.MaxValue)]
        public int OcupacionMensual { get; set; }

        [Range(0, int.MaxValue)]
        public int ReservasNuevas { get; set; }

        [Range(0, int.MaxValue)]
        public int Cancelaciones { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ImpactoFinancieroCancelaciones { get; set; }

        [Range(0, double.MaxValue)]
        public double PromedioDiasAnticipacionReserva { get; set; }

        [Range(0, int.MaxValue)]
        public int NumeroHuespedes { get; set; }

        [Range(0, int.MaxValue)]
        public int SuscritosBoletin { get; set; }

        [Range(0, double.MaxValue)]
        public double DuracionPromedioEstadia { get; set; }  // En días
    }
}