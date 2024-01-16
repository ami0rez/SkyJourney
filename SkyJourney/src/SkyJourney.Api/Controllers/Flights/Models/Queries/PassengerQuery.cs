using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SkyJourney.Api.Controllers.Flights.Models.Queries
{
    public class PassengerQuery
    {
        [Required(ErrorMessage = "The LastName is required.")]
        [MinLength(2, ErrorMessage = "The LastName must be at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "The LastName cannot exceed 100 characters.")]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The FirstName is required.")]
        [MinLength(2, ErrorMessage = "The FirstName must be at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "The FirstName cannot exceed 100 characters.")]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        public string SeatNumber { get; set; }
        public Guid ReservationId { get; set; }
    }
}
