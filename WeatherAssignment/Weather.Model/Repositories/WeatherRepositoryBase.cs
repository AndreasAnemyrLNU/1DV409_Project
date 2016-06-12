using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model.Repositories
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {
        // Geoname
        protected abstract IQueryable<Geoname> QueryGeonames();

        public IEnumerable<Geoname> FindAllGeonames()
        {
            return QueryGeonames().ToList();
        }

        public Geoname FindGeonameById(int id)
        {
            return QueryGeonames().SingleOrDefault(t => int.Parse(t.geonameId) == id);
        }

        public abstract void AddGeoname(Geoname geoname);
        public abstract void UpdateGeoname(Geoname geoname);
        public abstract void RemoveGeoname(int id);

        // Forecast
        protected abstract IQueryable<Forecast> QueryForecasts();

        public IEnumerable<Forecast> FindAllForecasts()
        {
            return QueryForecasts().ToList();
        }

        public Forecast FindForecastById(int id)
        {
            return QueryForecasts().SingleOrDefault(u => u.forecastId == id);
        }

        public IEnumerable<Forecast> FindForecastsByGeonameID(string id)
        {
            return QueryForecasts().Where(u => u.geonameId == id);
        }

        public abstract void AddForecast(Forecast forecast);
        public abstract void UpdateForecast(Forecast forecast);
        public abstract void RemoveForecast(Forecast forecst);

        public abstract void Save();
       
        //Dispose. Read about this! AA20160611
        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
