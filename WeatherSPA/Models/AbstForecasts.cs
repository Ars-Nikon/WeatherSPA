using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherSPA.Models
{
    public abstract class AbstForecasts<T, U> where T : AbstForecastForDay<U> where U : AbstForecastForHour
    {
        public AbstForecastNow ForecastNow { get; set; }

        public List<T> ForecastForDays { get; set; }
    }



    public abstract class AbstForecastNow
    {
        public DateTime? Date { get; set; }

        public int? NowTemp { get; set; }

        public string Icon { get; set; }

        public string City { get; set; }

        public string ConditionSky { get; set; }

        public double? WindSpeed { get; set; }

        public string DirectWind { get; set; }

        public double? Humidity { get; set; }

        public double? PrecProb { get; set; }
    }

    public abstract class AbstForecastForDay<T> where T : AbstForecastForHour
    {
        public DateTime? Date { get; set; }

        public int? MaxTemp { get; set; }

        public int? MinTemp { get; set; }

        public string Icon { get; set; }

        public string ConditionSky { get; set; }

        public double? WindSpeed { get; set; }

        public string DirectWind { get; set; }

        public double? Humidity { get; set; }

        public double? PrecProb { get; set; }

        public List<T> ForecastForHours { get; set; }
    }


    public abstract class AbstForecastForHour
    {
        public DateTime? Date { get; set; }

        public int? NowTemp { get; set; }

        public string Icon { get; set; }

        public string ConditionSky { get; set; }

        public double? WindSpeed { get; set; }

        public string DirectWind { get; set; }

        public double? Humidity { get; set; }

        public double? PrecProb { get; set; }

    }
}