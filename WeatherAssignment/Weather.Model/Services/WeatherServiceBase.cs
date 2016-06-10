using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model.Services
{
    public abstract class WeatherServiceBase : IWeatherService
    {
        public abstract IEnumerable<Geoname> GetGeonames(string startsWith, string country = "SE", int maxRows = 10);
        public abstract IEnumerable<Forecast> RefreshForecast(string cityId, string lat, string lon);
    }
}
