namespace SkyJourney.Infrastructure.Data.Models
{
    public class PlanEntity : BaseEntity
    {
        public string ModelName { get; set; }
        public ICollection<FlightEntity> Flights { get; set; }
    }
}
