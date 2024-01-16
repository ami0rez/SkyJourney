using SkyJourney.Infrastructure.Data.Models;

namespace SkyJourney.Infrastructure.Seeders
{
    public interface IDatabaseSeeder
    {
        Task<List<PlanEntity>> CreatePlan();
        Task GenerateCitiesAsync();
        Task CreateArrangement(FlightEntity flight);
        Task<List<FlightEntity>> GenerateFlightsAsync(PlanEntity plan, int numberOfFlights);
    }
}
