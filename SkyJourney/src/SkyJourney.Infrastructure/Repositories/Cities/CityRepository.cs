using Amirez.Infrastructure.Data;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Flights
{
    public class CityRepository : GenericRepository<CityEntity>, ICityRepository
    {
        public CityRepository(DatabaseContext context) : base(context)
        {
                
        }
    }
}
