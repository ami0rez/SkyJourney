namespace SkyJourney.Api.Controllers.Flights.Models.Responses
{
    public class PlanResponse
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public ICollection<FlightResponse> Flights { get; set; }
    }
}
