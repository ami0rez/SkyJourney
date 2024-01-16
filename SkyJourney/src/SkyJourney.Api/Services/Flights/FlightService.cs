using AutoMapper;
using Common.Models.Response;
using Microsoft.EntityFrameworkCore;
using SkyJourney.Api.Controllers.Flights.Models.Queries;
using SkyJourney.Api.Controllers.Flights.Models.Responses;
using SkyJourney.Infrastructure.Data.Models;
using SkyJourney.Infrastructure.Repositories.Customers;
using SkyJourney.Infrastructure.Repositories.Flights;
using SkyJourney.Infrastructure.Repositories.Reservations;
using SkyJourney.Infrastructure.Repositories.SeatArrangement;

namespace SkyJourney.Api.Services.Flights
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ISeatArrangementRepository _seatArrangementRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<IFlightService> _logger;
        private readonly IMapper _mapper;

        public FlightService(ILoggerFactory loggerFactory, IMapper mapper,
            IFlightRepository flightRepository,
            ISeatArrangementRepository seatArrangementRepository,
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository
            )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
            _seatArrangementRepository = seatArrangementRepository ?? throw new ArgumentNullException(nameof(seatArrangementRepository));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = loggerFactory?.CreateLogger<IFlightService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task<PaginationResponse<FlightResponse>> GetAll(FlightQuery query)
        {
            ValidateFlightQuery(query);

            var flights = _flightRepository.FindAll(
                flight => flight.NumberOfAvailableSeats >= query.CustomersCount
                && flight.DepartureCity.Id == query.DepartureCity
                && flight.ArrivalCity.Id == query.ArrivalCity
                && flight.DepartureDate.Date == query.DepartureDate.Date
                );

            if (query.ArrivalDate.HasValue)
            {
                flights = FilterByArrivalDate(flights, query.ArrivalDate.Value);
            }

            return await PaginateAndMapFlightsAsync(flights, query);
        }
        private void ValidateFlightQuery(FlightQuery query)
        {
            if (!query.DepartureCity.HasValue || !query.ArrivalCity.HasValue)
            {
                throw new ArgumentException("Departure and arrival cities are required.");
            }
        }
        private IQueryable<FlightEntity> FilterByArrivalDate(IQueryable<FlightEntity> flights, DateTime arrivalDate)
        {
            return flights.Where(f => f.ArrivalDate.HasValue && f.ArrivalDate.Value.Date == arrivalDate.Date);
        }
        private async Task<PaginationResponse<FlightResponse>> PaginateAndMapFlightsAsync(IQueryable<FlightEntity> flights, FlightQuery query)
        {
            var count = await flights.CountAsync();

            int skipCount = (query.PageNumber - 1) * query.PageSize;
            var pagedFlights = flights
                .OrderBy(f => f.Price)
                .Skip(skipCount)
                .Take(query.PageSize);
            var results = await _mapper.ProjectTo<FlightResponse>(pagedFlights).ToListAsync();

            return new PaginationResponse<FlightResponse>
            {
                Count = count,
                Rows = results,
            };
        }


        public async Task AddReservation(ReservationQuery request)
        {
            ValidateReservationRequest(request);

            var flight = await _flightRepository.FindById(request.FlightId);
            ValidateFlightForReservation(flight);

            await MakeReservation(request, flight);
        }
        private void ValidateReservationRequest(ReservationQuery request)
        {
            if (!request.Passengers.Any())
            {
                throw new ArgumentException("At least one passenger is required.");
            }

            foreach (var passenger in request.Passengers)
            {
                if (passenger == null)
                {
                    throw new ArgumentException("Invalid passenger details.");
                }
            }
        }

        private void ValidateFlightForReservation(FlightEntity flight)
        {
            if (flight == null)
            {
                throw new InvalidOperationException("Flight not found.");
            }
        }

        private async Task MakeReservation(ReservationQuery request, FlightEntity flight)
        {
            var availableSeats = await _seatArrangementRepository.GetAvailableSeats(request.FlightId, request.Passengers.Count);
            ValidateSeatAvailability(request, availableSeats);

            var reservations = await CreateReservationsAsync(request, availableSeats);
            await UpdateFlightAndSeatsAsync(flight, availableSeats);
        }

        private void ValidateSeatAvailability(ReservationQuery request, IEnumerable<string> availableSeats)
        {
            if (availableSeats.Count() < request.Passengers.Count)
            {
                throw new InvalidOperationException("Not enough available seats.");
            }
        }

        private async Task<List<ReservationEntity>> CreateReservationsAsync(ReservationQuery request, IEnumerable<string> bestChoiceSeats)
        {
            var reservations = new List<ReservationEntity>();
            foreach (var seat in bestChoiceSeats)
            {
                var reservation = new ReservationEntity
                {
                    Id = Guid.NewGuid(),
                    NumberOfPassengers = request.Passengers.Count,
                    DateReservation = DateTime.Now,
                    SeatNumber = seat,
                    FlightId = request.FlightId,
                    Passengers = new List<CustomerEntity>()
                };
                reservations.Add(reservation);

                foreach (var passenger in request.Passengers)
                {
                    var customerEntity = new CustomerEntity
                    {
                        Id = Guid.NewGuid(),
                        FirstName = passenger.FirstName,
                        LastName = passenger.LastName,
                        Email = passenger.Email,
                        SeatNumber = seat,
                        ReservationId = reservation.Id
                    };
                    reservation.Passengers.Add(customerEntity);
                    //await _customerRepository.CreateAsync(customerEntity);
                }
            }
            await _reservationRepository.CreateRangeAsync(reservations);
            return reservations;
        }
        private async Task UpdateFlightAndSeatsAsync(FlightEntity flight, IEnumerable<string> seats)
        {
            flight.NumberOfAvailableSeats -= seats.Count();
            await _flightRepository.UpdateAsync(flight.Id, flight);

            var seatArrangementsToUpdate = await _seatArrangementRepository.GetSeatArrangement(flight.Id, seats);
            foreach (var seatArrangement in seatArrangementsToUpdate)
            {
                seatArrangement.Status = false; // Assuming 'false' indicates the seat is booked
            }
            await _seatArrangementRepository.UpdateRangeAsync(seatArrangementsToUpdate);
        }

        private async Task<CustomerEntity> RegisterNewPassengers(PassengerQuery passenger)
        {
            var customer = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Email = passenger.Email,
                SeatNumber = passenger.SeatNumber,
                ReservationId = passenger.ReservationId
            };

            await _customerRepository.CreateAsync(customer);
            return customer;
        }
    }
}
