using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model.DataModels;

namespace Weather.Model.Repositories
{
    public class WeatherRepository : WeatherRepositoryBase
    {
        private Entities _context = new Entities();

        // Geoname
        protected override IQueryable<Geoname> QueryGeonames()
        {
            return _context.Geonames.AsQueryable();
        }

        public override void AddGeoname(Geoname geoname)
        {
            _context.Geonames.Add(geoname);
        }

        public override void UpdateGeoname(Geoname geoname)
        {
            _context.Entry(geoname).State = EntityState.Modified;
        }

        public override void RemoveGeoname(int id)
        {
            Geoname geoname = _context.Geonames.Find(id);
            _context.Geonames.Remove(geoname);
        }

        // Forecast
        protected override IQueryable<Forecast> QueryForecasts()
        {
            return _context.Forecasts.AsQueryable();
        }

        public override void AddForecast(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);
        }

        public override void UpdateForecast(Forecast forecast)
        {
            _context.Entry(forecast).State = EntityState.Modified;
        }

        public override void RemoveForecast(Forecast forecast)
        {
            forecast = _context.Forecasts.Find(forecast.forecastId);
            _context.Forecasts.Remove(forecast);
        }

        // Context
        public override void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;

            base.Dispose(disposing);
        }

        #endregion
    }
}
