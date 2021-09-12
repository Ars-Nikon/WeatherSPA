using System.Threading.Tasks;
using WeatherSPA.Models;

namespace WeatherSPA.Service
{
    public interface IWeatherApi
    {
        public Task<Forecasts> GetForecast(float lat, float lon);
    }
}
