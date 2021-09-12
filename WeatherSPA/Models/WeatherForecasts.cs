using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherSPA.Models
{
    public class ForecastForHour : AbstForecastForHour
    { }

    public class ForecastNow : AbstForecastNow
    { }

    public class ForecastForDay : AbstForecastForDay<ForecastForHour>
    { }

    public class Forecasts : AbstForecasts<ForecastForDay, ForecastForHour>
    { }
}
