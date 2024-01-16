using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.Reservations
{
    public interface IReservationRepository : IGenericRepository<ReservationEntity>
    {
    }
}
