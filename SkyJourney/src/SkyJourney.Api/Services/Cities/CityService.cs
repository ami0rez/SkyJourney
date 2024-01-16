using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkyJourney.Api.Controllers.Cities.Models;
using SkyJourney.Infrastructure.Repositories.Flights;

namespace SkyJourney.Api.Services.Cities
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(
            ICityRepository cityRepository,
             IMapper mapper
            )
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CityResponse>> GetAll()
        {
            var cities = _cityRepository.FindAll();

            var mappedCities = await _mapper.ProjectTo<CityResponse>(cities).ToListAsync();

            return mappedCities;
        }
    }
}
