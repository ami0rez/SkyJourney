using Common.Models.Response;
using Microsoft.AspNetCore.Mvc;
using SkyJourney.Api.Controllers.Common;
using SkyJourney.Api.Controllers.Flights.Models.Queries;
using SkyJourney.Api.Controllers.Flights.Models.Responses;
using SkyJourney.Api.Services.Flights;

namespace SkyJourney.Api.Controllers.Flights
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : BaseController<FlightController>
    {
        private readonly IFlightService _flightService;
        public FlightController(ILoggerFactory factory, IFlightService flightService) : base(factory)
        {
            _flightService = flightService;
        }

        [HttpPost("save")]
        public async Task<ActionResult<MainResponse<bool>>> AddReservation([FromBody] ReservationQuery model)
        {
            var res = new MainResponse<bool>();
            var message = "success";
            try
            {
                Logger.LogInformation("FlightController try to save");
                await _flightService.AddReservation(model);
                res.Response = true;
            }
            catch (Exception e)
            {
                message = "errorSavingadminstrative";
                Logger.LogError("error save adminstrative ", e);
            }
            res.Message = message;
            return Ok(res);
        }

        [HttpPost("findAll")]
        public async Task<ActionResult<MainResponse<PaginationResponse<FlightResponse>>>> GetAll([FromBody] FlightQuery model)
        {
            var res = new MainResponse<PaginationResponse<FlightResponse>>();
            var message = "success";
            try
            {
                Logger.LogInformation("FlightController try to searching by id" + model.Id.ToString());
                var response = await _flightService.GetAll(model);
                if (response == null) message = "emptyData";
                res.Response = response;
            }
            catch (Exception e)
            {
                message = "errorgetFilght";
                res.Error = true;
                Logger.LogError("error while searching into Filght " + model.Id.ToString(), e);
            }
            res.Message = message;
            return Ok(res);
        }
    }
}
