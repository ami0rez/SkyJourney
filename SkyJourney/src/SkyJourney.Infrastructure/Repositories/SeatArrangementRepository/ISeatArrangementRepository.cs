using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Generic;

namespace SkyJourney.Infrastructure.Repositories.SeatArrangement
{
    public interface ISeatArrangementRepository : IGenericRepository<SeatArrangementEntity>
    {
        Task<List<SeatArrangementEntity>> GetSeatArrangement(Guid flightId, IEnumerable<string> seats);
        int GetRange(string seatNumber);
        Task<IList<string>> GetAvailableSeats(Guid FlightId, int numberOfFamilyMembers);
        char GetPosition(string seatNumber);
        List<string> GenerateFamilySeats(int familySize, List<string> availableSeats);
    }
}
