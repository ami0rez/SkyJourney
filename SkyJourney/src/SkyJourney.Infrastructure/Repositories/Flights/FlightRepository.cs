using Amirez.Infrastructure.Data;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Flights
{
    public class FlightRepository : GenericRepository<FlightEntity>, IFlightRepository
    {
        public FlightRepository(DatabaseContext context) : base(context)
        {
                
        }
    }
}
