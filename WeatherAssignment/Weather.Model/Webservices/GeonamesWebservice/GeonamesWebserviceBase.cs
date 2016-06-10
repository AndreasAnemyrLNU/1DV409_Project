using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Domain.Webservices
{
    public abstract class GeonamesWebserviceBase : IGeonamesWebservice
    {
        public abstract IEnumerable<Geoname> GetGeonames(string startsWith, string country, int maxRows);
    }
}
