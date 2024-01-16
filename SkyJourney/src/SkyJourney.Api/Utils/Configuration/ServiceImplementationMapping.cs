using SkyJourney.Api.Services.Cities;
using SkyJourney.Api.Services.Flights;
using SkyJourney.Infrastructure.Repositories.Customers;
using SkyJourney.Infrastructure.Repositories.Flights;
using SkyJourney.Infrastructure.Repositories.Reservations;
using SkyJourney.Infrastructure.Repositories.SeatArrangement;
using SkyJourney.Infrastructure.Seeders;

namespace SkyJourney.Api.Utils.Configuration
{
    public static class ServiceImplementationMapping
    {
        public static void MapServicesToImplementations(this IServiceCollection services)
        {
            //Flight
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IFlightRepository, FlightRepository>();

            //Customer
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            //Reservation
            services.AddTransient<IReservationRepository, ReservationRepository>();

            //SeatArrangement
            services.AddTransient<ISeatArrangementRepository, SeatArrangementRepository>();

            //City
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICityRepository, CityRepository>();

            //DatabaseSeeder
            services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();

        }
    }
}
