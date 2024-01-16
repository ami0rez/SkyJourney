using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SkyJourney.Api.Controllers.Flights.Models.Queries
{
    public class ReservationQuery
    {
        [Required(ErrorMessage = "The FlightId is required.")]
        [DisplayName("FlightId")]
        public Guid FlightId { get; set; }
        public List<PassengerQuery> Passengers { get; set; }
    }
}
