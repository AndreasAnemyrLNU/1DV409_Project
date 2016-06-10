using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Webservices;

namespace Weather.Model.Services
{
    public class WeatherService : WeatherServiceBase
    {
        public override IEnumerable<Geoname> GetGeonames(string startsWith, string country = "SE", int maxRows = 10)
        {
            return new GeonamesWebservice().GetGeonames(startsWith);
        }

        public override IEnumerable<Forecast> RefreshForecast(string cityId, string lat, string lon)
        {
            return new OpenWeatherMapWebservice().Get5DaysForecastByCityId(int.Parse(cityId), lat, lon);
        }
    }
}
