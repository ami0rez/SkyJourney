﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SkyJourney.Api.Controllers.Flights.Models.Queries
{
    public class FlightQuery
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The DepartureCity is Required")]
        [DisplayName("DepartureCity")]
        public Guid? DepartureCity { get; set; }

        [Required(ErrorMessage = "The ArrivalCity is Required")]
        [DisplayName("ArrivalCity")]
        public Guid? ArrivalCity { get; set; }

        [Required(ErrorMessage = "The DepartureDate is Required")]
        [DisplayName("DepartureDate")]
        public DateTime DepartureDate { get; set; }

        [DisplayName("ArrivalDate")]
        public DateTime? ArrivalDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than or equal to 1")]
        [DisplayName("PageNumber")]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than or equal to 1")]
        [DisplayName("PageSize")]
        public int PageSize { get; set; } = 10;

        [Range(1, 9, ErrorMessage = "CustomersCount must be greater than or equal to 1")]
        [DisplayName("CustomersCount")]
        public int CustomersCount { get; set; } = 10;
    }
}
