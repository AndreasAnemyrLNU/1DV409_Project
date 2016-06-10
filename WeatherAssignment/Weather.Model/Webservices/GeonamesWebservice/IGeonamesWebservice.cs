using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Domain.Webservices
{
    public interface IGeonamesWebservice
    {
        IEnumerable<Geoname> GetGeonames(string startsWith, string country, int maxRows);
    }
}
