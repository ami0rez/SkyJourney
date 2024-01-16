namespace SkyJourney.Infrastructure.Data.Models
{ 
    public class SeatArrangementEntity : BaseEntity
    {
        public string SeatNumber { get; set; }
        public bool Status { get; set; } = true;
        public Guid FlightId { get; set; }
        public FlightEntity Flight { get; set; }
    }
}
