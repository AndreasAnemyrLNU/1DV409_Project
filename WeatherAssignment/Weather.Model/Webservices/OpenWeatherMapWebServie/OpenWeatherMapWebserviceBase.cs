using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Domain.Webservices
{
    public abstract class OpenWeatherMapWebserviceBase : IOpenWeatherMapWebservice
    {
        public abstract IEnumerable<Forecast> Get5DaysForecastByCityId(int cityId, string lat, string lon);
    }
}
