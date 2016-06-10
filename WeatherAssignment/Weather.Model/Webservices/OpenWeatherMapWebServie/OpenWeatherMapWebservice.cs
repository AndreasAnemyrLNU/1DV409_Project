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
    public class OpenWeatherMapWebservice : OpenWeatherMapWebserviceBase
    {
        private readonly string appId = "324f27d2163e532ea5d87ec035eff704";

        private readonly string mode = "xml";

        public override IEnumerable<Forecast> Get5DaysForecastByCityId(int cityId, string lat, string lon)
        {

            //Requested url Returns XML.
            //var uri = $"http://api.openweathermap.org/data/2.5/forecast/city?id={cityId}&APPID={appId}&mode={mode}";

            //Switch to lon & lat!
            var uri = $"http://api.openweathermap.org/data/2.5/forecast/city?lat={lat}&lon={lon}&APPID={appId}&mode={mode}";

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

            IEnumerable<XElement> forecasts = 
            from forecast in xml.Descendants("weatherdata").Elements("forecast").Elements("time")
            select forecast;

            return

                    (from forecast in forecasts
                    select new Forecast
                    {
                        timeFrom = DateTime.Parse(forecast.Attribute("from").Value),
                        timeTo = DateTime.Parse(forecast.Attribute("to").Value),
                        geonameId = cityId.ToString(),
                        symbolNumer = forecast.Element("symbol").Attribute("number").Value,
                        symbolName = forecast.Element("symbol").Attribute("name").Value,
                        symbolVar  = forecast.Element("symbol").Attribute("var").Value,
                        windDirectionDeg = forecast.Element("windDirection").Attribute("deg").Value,
                        windDirectionCode = forecast.Element("windDirection").Attribute("code").Value,
                        windDirectionName = forecast.Element("windDirection").Attribute("name").Value,
                        windSpeedMps = forecast.Element("windSpeed").Attribute("mps").Value,
                        windSpeedName = forecast.Element("windSpeed").Attribute("name").Value,
                        temperatureUnit = forecast.Element("temperature").Attribute("unit").Value,
                        temperatureValue = forecast.Element("temperature").Attribute("value").Value,
                        temperatureMin = forecast.Element("temperature").Attribute("min").Value,
                        temperatureMax = forecast.Element("temperature").Attribute("max").Value,
                        pressureUnit = forecast.Element("pressure").Attribute("unit").Value,
                        pressureValue = forecast.Element("pressure").Attribute("value").Value,
                        humidityValue = forecast.Element("humidity").Attribute("value").Value,
                        humidityUnit = forecast.Element("humidity").Attribute("unit").Value,
                        cloudsValue = forecast.Element("clouds").Attribute("value").Value,
                        cloudsAll = forecast.Element("clouds").Attribute("all").Value,
                        cloudsUnit = forecast.Element("clouds").Attribute("unit").Value
                    }).ToList<Forecast>();
        }
    }
}
