using Common.Models.Response;
using SkyJourney.Api.Controllers.Flights.Models.Queries;
using SkyJourney.Api.Controllers.Flights.Models.Responses;

namespace SkyJourney.Api.Services.Flights
{
    public interface IFlightService
    {
        Task<PaginationResponse<FlightResponse>> GetAll(FlightQuery query);
        Task AddReservation(ReservationQuery request);

    }
}
