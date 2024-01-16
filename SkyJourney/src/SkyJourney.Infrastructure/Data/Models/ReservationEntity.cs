namespace SkyJourney.Infrastructure.Data.Models
{
    public class ReservationEntity : BaseEntity
    {
        public int NumberOfPassengers { get; set; }
        public string SeatNumber { get; set; }
        public DateTime DateReservation { get; set; }
        public Guid FlightId { get; set; }
        public FlightEntity Flight { get; set; }
        public ICollection<CustomerEntity> Passengers { get; set; }
    }
}
