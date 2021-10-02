using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherSPA.Configuration;
using WeatherSPA.Models;

namespace WeatherSPA.Service
{
    public class WeatherApiServise : IWeatherApi
    {


        private readonly Dictionary<string, string> directWindFormat = new Dictionary<string, string>()
        {
            {"nw", "СЗ"},
            {"n", "С"},
            {"ne", "СВ"},
            {"e", "В"},
            {"se", "ЮВ"},
            {"s", "Ю"},
            {"sw", "ЮЗ"},
            {"w", "З"},
            {"c", "Штиль"},
        };

        private readonly IOptions<YandexConfigure> _yandexConfig;

        public WeatherApiServise(IOptions<YandexConfigure> config)
        {
            this._yandexConfig = config;
        }

        private async Task<string> GetJson(float lat, float lon)
        {
            var CultInf = new CultureInfo("en-US");

            var person = $"https://api.weather.yandex.ru/v2/forecast?lat={lat.ToString(CultInf)}&lon={lon.ToString(CultInf)}&lang={_yandexConfig.Value.Lang}"
                .WithHeader("X-Yandex-API-Key", _yandexConfig.Value.KeyApi)
                .WithOAuthBearerToken("my_oauth_token")
                .GetAsync()
                .Result;

            var json = await person.ResponseMessage.Content.ReadAsStringAsync();

            return json;
        }

        private ForecastNow ForecastNow(Weather forecastYandex)
        {
            return new ForecastNow()
            {
                Date = forecastYandex.now_dt,
                NowTemp = forecastYandex.fact.temp,
                Icon = forecastYandex.fact.icon,
                ConditionSky = forecastYandex.fact.condition,
                City = forecastYandex.geo_object.province.name,
                WindSpeed = forecastYandex.fact.wind_speed,
                DirectWind = directWindFormat.GetValueOrDefault(forecastYandex.fact.wind_dir),
                Humidity = forecastYandex.fact.humidity,
                PrecProb = forecastYandex.fact.prec_prob
            };
        }

        private ForecastForDay ForecastForDay(Forecast ForecastDayYandex)
        {
            return new ForecastForDay()
            {
                MaxTemp = ForecastDayYandex.parts.day_short.temp,
                MinTemp = ForecastDayYandex.parts.day_short.temp_min,
                Date = ForecastDayYandex.date,
                Icon = ForecastDayYandex.parts.day_short.icon,
                ConditionSky = ForecastDayYandex.parts.day_short.condition,
                WindSpeed = ForecastDayYandex.parts.day_short.wind_speed,
                DirectWind = directWindFormat.GetValueOrDefault(ForecastDayYandex.parts.day_short.wind_dir),
                Humidity = ForecastDayYandex.parts.day_short.humidity,
                PrecProb = ForecastDayYandex.parts.day_short.prec_prob
            };
        }

        private ForecastForHour ForecastForHour(Hour ForecastHour, DateTime date)
        {
            return new ForecastForHour()
            {
                Date = date.AddHours(Convert.ToInt32(ForecastHour.hour)),
                NowTemp = ForecastHour.temp,
                Icon = ForecastHour.icon,
                ConditionSky = ForecastHour.condition,
                WindSpeed = ForecastHour.wind_speed,
                DirectWind = directWindFormat.GetValueOrDefault(ForecastHour.wind_dir),
                Humidity = ForecastHour.humidity,
                PrecProb = ForecastHour.prec_prob
            };
        }

        private List<ForecastForDay> ForecastForDays(Weather forecastYandex)
        {
            List<ForecastForDay> forecastForDays = new List<ForecastForDay>();

            foreach (var ForecastDayYandex in forecastYandex.forecasts)
            {
                var forecastDay = ForecastForDay(ForecastDayYandex);

                var ForecastsForHours = new List<ForecastForHour>();

                foreach (var ForecastHour in ForecastDayYandex.hours)
                {
                    ForecastsForHours.Add(ForecastForHour(ForecastHour, ForecastDayYandex.date));
                }

                forecastDay.ForecastForHours = ForecastsForHours;
                forecastForDays.Add(forecastDay);
            }
            return forecastForDays;
        }

        public async Task<Forecasts> GetForecast(float lat, float lon)
        {
            var json = await GetJson(lat, lon);

            Weather forecastYandex = JsonSerializer.Deserialize<Weather>(json);

            var forecastNow = ForecastNow(forecastYandex);

            var forecastForDays = ForecastForDays(forecastYandex);

            return new Forecasts() { ForecastNow = forecastNow, ForecastForDays = forecastForDays };
        }

        #region Serelize Classes 
        private class Weather
        {
            public DateTime now_dt { get; set; }
            public Geo_Object geo_object { get; set; }
            public Fact fact { get; set; }
            public Forecast[] forecasts { get; set; }
        }

        private class Geo_Object
        {
            public Locality locality { get; set; }
            public Province province { get; set; }
            public Country country { get; set; }
        }

        private class Locality
        {
            public string name { get; set; }
        }

        private class Province
        {
            public string name { get; set; }
        }

        private class Country
        {
            public string name { get; set; }
        }

        private class Fact
        {
            public int temp { get; set; }
            public int feels_like { get; set; }
            public string icon { get; set; }
            public string condition { get; set; }
            public float cloudness { get; set; }
            public double wind_speed { get; set; }
            public string wind_dir { get; set; }
            public int pressure_mm { get; set; }
            public int pressure_pa { get; set; }
            public double humidity { get; set; }
            public int uv_index { get; set; }
            public float wind_gust { get; set; }
            public double prec_prob { get; set; }
        }

        private class Forecast
        {
            public DateTime date { get; set; }
            public string sunrise { get; set; }
            public string sunset { get; set; }
            public int moon_code { get; set; }
            public parts parts { get; set; }
            public Hour[] hours { get; set; }
        }

        private class parts
        {
            public day_short day_short { get; set; }
        }

        private class day_short
        {
            public int temp { get; set; }
            public int temp_min { get; set; }
            public string icon { get; set; }
            public string condition { get; set; }
            public double wind_speed { get; set; }
            public string wind_dir { get; set; }
            public double humidity { get; set; }
            public double prec_prob { get; set; }
        }

        private class Hour
        {
            public string hour { get; set; }
            public int temp { get; set; }
            public string icon { get; set; }
            public string condition { get; set; }
            public float cloudness { get; set; }
            public string wind_dir { get; set; }
            public double wind_speed { get; set; }
            public double humidity { get; set; }
            public double prec_prob { get; set; }
        }

        #endregion
    }
}
