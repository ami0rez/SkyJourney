using AutoMapper;
using SkyJourney.Api.Controllers.Cities.Models;
using SkyJourney.Api.Controllers.Flights.Models.Queries;
using SkyJourney.Api.Controllers.Flights.Models.Responses;
using SkyJourney.Infrastructure.Data.Models;

namespace SkyJourney.Api.Utils.Mapping
{
    public class SkyJourneyMappingProfile : Profile
    {
        public SkyJourneyMappingProfile()
        {
            CreateMap<CustomerEntity, CustomerQuery>();
            CreateMap<FlightEntity, FlightResponse>();
            CreateMap<PlanEntity, PlanResponse>();
            CreateMap<ReservationEntity, ReservationResponse>()
                .ForMember(dest => dest.PassengerName, opt => opt.Ignore());

            CreateMap<CityEntity, CityResponse>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        }
    }
}
