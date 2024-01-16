using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SkyJourney.Api.Controllers.Flights.Models.Queries
{
    public class CustomerQuery
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The FirstName is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
