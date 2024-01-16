namespace SkyJourney.Api.Controllers.Flights.Models.Responses
{
    public class FlightResponse
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public int NumberOfAvailableSeats { get; set; }
        public Guid PlanId { get; set; }
        public PlanResponse Plan { get; set; }
        public ICollection<ReservationResponse> Reservations { get; set; }
    }
}
