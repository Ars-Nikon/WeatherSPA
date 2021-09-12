using Microsoft.Extensions.Options;
using WeatherSPA.Configuration;
using WeatherSPA.Service;
using Xunit;

namespace WeatherSPA.Test
{
    public class WeatherApiServiseTest
    {
        private readonly OptionsWrapper<YandexConfigure> myConfiguration = new OptionsWrapper<YandexConfigure>(new YandexConfigure
        {
            KeyApi = "18b8d15c-0d6c-4849-8ee3-0ab08cdf5876",
            Lang = "ru_RU"
        });


        [Fact]
        public void GetForecasts()
        {
            // Arrange
            WeatherApiServise weather = new WeatherApiServise(myConfiguration);

            // Act
            var result = weather.GetForecast(45.06231040444037f, 38.993170694533525f).Result;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ForecastForDays);
            Assert.NotEmpty(result.ForecastForDays);
            Assert.NotNull(result.ForecastForDays[0].ForecastForHours);
            Assert.NotEmpty(result.ForecastForDays[0].ForecastForHours);
        }
    }
}
