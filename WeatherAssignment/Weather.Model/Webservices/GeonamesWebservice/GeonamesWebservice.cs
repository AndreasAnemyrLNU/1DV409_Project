using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Weather.Model;

namespace Weather.Domain.Webservices
{
    public class GeonamesWebservice : GeonamesWebserviceBase
    {
        private readonly string username = "andreasanemyr";

        public override IEnumerable<Geoname> GetGeonames(string startsWith, string country = "SE", int maxRows = 10)
        {
            //Requested url Returns XML.
            var uri = $"http://api.geonames.org/search?name_startsWith={startsWith}&country={country}&maxRows={maxRows}&username={username}";
            string rawXML = string.Empty;

            // Create a request using a URL that can receive a post. 
            WebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            // Set the Method property of the request to GET.
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawXML = reader.ReadToEnd();
            }

            XDocument xml = XDocument.Parse(rawXML);

            return 
                        (
                            from geoname in xml.Root.Descendants("geoname")
                            select  new Geoname
                                    {
                                        toponymName = (string)geoname.Element("toponymName"),
                                        name        = (string)geoname.Element("name"),
                                        lat         = (string)geoname.Element("lat"),
                                        lng         = (string)geoname.Element("lng"),
                                        geonameId   = (string)geoname.Element("geonameId"),
                                        countryCode = (string)geoname.Element("countryCode"),
                                        countryName = (string)geoname.Element("countryName"),
                                        fcl         = (string)geoname.Element("countryName"),
                                        fcode       = (string)geoname.Element("fcode")
                                    }
                        ).ToList<Geoname>();
        }
    }
}
