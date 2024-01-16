using Common.Models.Response;
using Microsoft.AspNetCore.Mvc;
using SkyJourney.Api.Controllers.Cities.Models;
using SkyJourney.Api.Controllers.Common;
using SkyJourney.Api.Controllers.Flights;
using SkyJourney.Api.Services.Cities;

namespace SkyJourney.Api.Controllers.Cities
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController<FlightController>
    {
        private readonly ICityService _cityService;
        public CitiesController(ILoggerFactory factory, ICityService cityService) : base(factory)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<MainResponse<IEnumerable<CityResponse>>>> GetAll()
        {
            var res = new MainResponse<IEnumerable<CityResponse>>();
            var message = "success";
            try
            {
                Logger.LogInformation("CitiesController try to get cities");
                res.Response = await _cityService.GetAll();
            }
            catch (Exception e)
            {
                message = "errorSavingadminstrative";
                Logger.LogError("error save getting cities ", e);
            }
            res.Message = message;
            return Ok(res);
        }
    }
}
