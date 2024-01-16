using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Flights
{
    public interface IFlightRepository : IGenericRepository<FlightEntity>
    {
    }
}
