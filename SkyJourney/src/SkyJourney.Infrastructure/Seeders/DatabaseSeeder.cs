using Amirez.Infrastructure.Data;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Flights;

namespace SkyJourney.Infrastructure.Seeders
{
    public class DatabaseSeeder : IDatabaseSeeder
    {

        private readonly DatabaseContext _dbContext;
        private readonly ILogger<IDatabaseSeeder> _logger;
        private readonly IFlightRepository _flightRepository;
        private readonly ICityRepository _cityRepository;

        public DatabaseSeeder(
            ILoggerFactory loggerFactory,
            DatabaseContext context,
            IFlightRepository flightRepository,
            ICityRepository cityRepository
            )
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory.CreateLogger<IDatabaseSeeder>();
            _flightRepository = flightRepository;
            _cityRepository = cityRepository;
        }
        public async Task<List<PlanEntity>> CreatePlan()
        {
            try
            {
                _logger.LogInformation("save InitSeedRepository CreatePlan");
                var planFaker = new Faker<PlanEntity>()
                                .RuleFor(p => p.ModelName, f => f.Random.Word());

                var plans = planFaker.Generate(2); // Generate 50 fake plan entities
                await _dbContext.AddRangeAsync(plans);
                await _dbContext.SaveChangesAsync();
                // Retrieve the list of plans (including any database-generated IDs)
                var savedPlans = await _dbContext.Plans.ToListAsync();
                // Return the list of saved plans
                return savedPlans;
            }
            catch (Exception e)
            {
                _logger.LogError("error save InitSeedRepository", e);
                return Enumerable.Empty<PlanEntity>().ToList();
            }
        }

        public async Task CreateArrangement(FlightEntity flight)
        {
            try
            {
                _logger.LogInformation("save InitSeedRepository CreatePlan");
                var seatNumbers = new List<string>();
                var letters = new List<char> { 'A', 'B', 'C', 'D' }; // Add more letters if needed
                var numberOfSeats = 200; // Change this to the number of seats you want

                for (int i = 0; i < numberOfSeats; i++)
                {
                    var seatLetter = letters[i % letters.Count];
                    var seatNumber = (i / letters.Count) + 1;
                    var seat = $"{seatLetter}{seatNumber}";
                    seatNumbers.Add(seat);
                }
                int seatNumberIndex = 0;

                var seatArrangementFaker = new Faker<SeatArrangementEntity>()
                    .RuleFor(s => s.FlightId, (f, s) =>
                    {
                        return flight.Id;
                    }).RuleFor(s => s.SeatNumber, f =>
                    {
                        // Use seatNumbers in sequence
                        string seatNumber = seatNumbers[seatNumberIndex];
                        seatNumberIndex++;

                        return seatNumber;
                    })
                .RuleFor(s => s.Status, true);

                var seatArrangements = seatArrangementFaker.Generate(numberOfSeats); // Adjust the number as needed
                await _dbContext.AddRangeAsync(seatArrangements);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("error save InitSeedRepository", e);
            }
        }
        public async Task GenerateCitiesAsync()
        {
            try
            {
                var cityNames = new List<string> { "Paris", "Londres", "New York", "Carrollside", "Josuefurt" };
                var cityEntities = cityNames.Select(name => new CityEntity { Id = Guid.NewGuid(), Name = name }).ToList();
                await _cityRepository.CreateRangeAsync(cityEntities); 

            }
            catch (Exception e)
            {
                _logger.LogError("error while generating cities", e);
            }
        }
        public async Task<List<FlightEntity>> GenerateFlightsAsync(PlanEntity plan, int numberOfFlights)
        {
            try
            {
                var savedCities = await _cityRepository.FindAll().ToListAsync();

                var flightFaker = new Faker<FlightEntity>()
                    .StrictMode(true) // Enable strict mode to throw exceptions on rule violations
                    .RuleFor(f => f.Id, f => f.Random.Guid())
                    .RuleFor(f => f.Airline, f => f.Company.CompanyName())
                    .RuleFor(f => f.FlightNumber, f => f.Random.AlphaNumeric(6))
                    .RuleFor(f => f.PlanId, plan.Id)
                    .RuleFor(f => f.Price, f => f.Random.Double(100, 1000))
                    .RuleFor(f => f.NumberOfAvailableSeats, 200)
                    .RuleFor(f => f.DepartureCityId, f => f.PickRandom(savedCities).Id)
                    .RuleFor(f => f.ArrivalCityId, (f, flight) =>
                    {
                        CityEntity arrivalCity;
                        do
                        {
                            arrivalCity = f.PickRandom(savedCities);
                        } while (arrivalCity.Id == flight.DepartureCityId);
                        return arrivalCity.Id;
                    })
                    .RuleFor(f => f.DepartureDate, f => f.Date.Between(DateTime.Today.AddDays(10), DateTime.Today.AddDays(30)))
                    .RuleFor(f => f.ArrivalDate, (f, flight) => f.Date.Between(flight.DepartureDate.AddDays(6), flight.DepartureDate.AddDays(10)))
                    .Ignore(f => f.Reservations)
                    .Ignore(f => f.Plan)
                    .Ignore(f => f.DepartureCity) // Assuming these are loaded via navigation properties
                    .Ignore(f => f.ArrivalCity);
                var list = flightFaker.Generate(numberOfFlights);
                await _flightRepository.CreateRangeAsync(list);
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError("error save InitSeedRepository", e);
                return Enumerable.Empty<FlightEntity>().ToList();
            }
        }
    }
}