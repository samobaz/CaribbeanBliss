namespace Caribbean2.Models
{
    public class HuespedReserva
    {
        public int HuespedId { get; set; }
        public Huesped Huesped { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
    }
}
