using Amirez.Infrastructure.Data;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Reservations
{
    public class ReservationRepository : GenericRepository<ReservationEntity>, IReservationRepository
    {
        public ReservationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
