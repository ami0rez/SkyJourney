namespace SkyJourney.Infrastructure.Data.Models
{
    public class CustomerEntity : BaseEntity
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string SeatNumber { get; set; }
        public Guid ReservationId { get; set; }
        public ReservationEntity Reservation { get; set; }
    }
}