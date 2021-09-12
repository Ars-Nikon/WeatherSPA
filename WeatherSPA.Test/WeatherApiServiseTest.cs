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
            KeyApi = "54d9c111-9cd4-4ac4-bd33-057491d6a1c1",
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
