using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Webservices;
using Weather.Model.Repositories;

namespace Weather.Model.Services
{
    public class WeatherService : WeatherServiceBase
    {
        private IWeatherRepository _weatherRepository;

        public WeatherService()
            :this(new WeatherRepository())
        {
            // Empty
        }

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }





        public override IEnumerable<Geoname> GetGeonames(string startsWith, string country, int maxRows)
        {
            return new GeonamesWebservice().GetGeonames(startsWith, country, maxRows);
        }

        public override IEnumerable<Forecast> RefreshForecast(string cityId, string lat, string lon)
        {
            IEnumerable<Forecast> forcasts;

            try
            {
                // Try to find forcast if geonameid exists!
                // READ in qrud. KRAV!
                forcasts = _weatherRepository.FindForecastsByGeonameID(cityId);

                //No hit in db. Exception is thrown and Get forcst from openweather api instead.
                forcasts.First();
            }
            catch
            {
                //No existing forecasts in db for geonamId.
                //Instead we request from OpenWeatherMap.
                //We save this into db.
                forcasts = new OpenWeatherMapWebservice().Get5DaysForecastByCityId(int.Parse(cityId), lat, lon);

                foreach (var forcast in forcasts)
                {
                    try
                    {
                        _weatherRepository.AddForecast(forcast);
                        _weatherRepository.Save();
                    }
                    catch (DbEntityValidationException)
                    {
                        //Ignore adding...
                        //Forecast data not valid.
                        _weatherRepository.RemoveForecast(forcast);
                    }
                }         
            }
            try
            {
                _weatherRepository.FindForecastsByGeonameID(cityId).First();
                return _weatherRepository.FindForecastsByGeonameID(cityId);
            }
            catch
            {
                //No hit!
                //For user we presents response from openweathermap directly. 
                //But Forcasts is not saved into db!
                return forcasts;
            }           
        }
    }
}
