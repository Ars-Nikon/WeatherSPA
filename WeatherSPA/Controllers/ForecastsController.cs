using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherSPA.Service;

namespace WeatherSPA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastsController :  Controller
    {
        private readonly ILogger<ForecastsController> _logger;
        private readonly IWeatherApi _weatherApi;

        public ForecastsController(
            IWeatherApi weatherApi,
            ILogger<ForecastsController> logger)
        {
            _weatherApi = weatherApi;
            _logger = logger;
        }

        [Route("Forecasts")]
        public IActionResult Forecasts()
        {
            return Json(_weatherApi.GetForecast(45.06231040444037f, 38.993170694533525f).Result);
        }
    }
}
