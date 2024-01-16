using SkyJourney.Api.Controllers.Flights.Models.Queries;

namespace SkyJourney.Api.Controllers.Flights.Models.Responses
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public int NumberOfPassengers { get; set; }
        public string PassengerName { get; set; }
        public string SeatNumber { get; set; }
        public DateTime DateReservation { get; set; }
        public ICollection<CustomerQuery> Passengers { get; set; }
    }
}
