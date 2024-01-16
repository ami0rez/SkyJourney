using SkyJourney.Api.Controllers.Cities.Models;

namespace SkyJourney.Api.Services.Cities
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponse>> GetAll();
    }
}
