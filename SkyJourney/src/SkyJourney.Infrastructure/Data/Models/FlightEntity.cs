namespace SkyJourney.Infrastructure.Data.Models
{
    public class FlightEntity : BaseEntity
    {
        // Empty constructor for EF
        public FlightEntity() { }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public Guid DepartureCityId { get; set; }
        public CityEntity DepartureCity { get; set; }
        public Guid ArrivalCityId { get; set; }
        public CityEntity ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public double Price { get; set; }
        public int NumberOfAvailableSeats { get; set; }

        // Navigation property to represent the associated aircraft
        public Guid PlanId { get; set; }
        public PlanEntity Plan { get; set; }
        public ICollection<ReservationEntity> Reservations { get; set; }
    }
}
