using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Model;

namespace WeatherApp.MVC.DataViewModels
{
    public class WeahterIndexViewModel
    {
        //For Querystring
        [DisplayName("Geoname ID")]
        public string GeonameId { get; set; }

        [Required]
        [DisplayName("Location")]
        public string GeonameSearch { get; set; }

        [Required]
        [DisplayName("Country")]
        public string CountryCode { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Min 1 and Max 10")]
        [DisplayName("Max Resultset")]
        public string MaxRow { get; set; }

        public readonly string[] countryCodes = { "SE", "DK", "FI", "NO" };

        public readonly string[] maxRows = { "1","2","3","4","5","6","7","8","9","10" };

        public bool HasForecasts => Forecasts?.Any() ?? false;

        public IEnumerable<Forecast> GetForecastsByDayZeroBased(int dayDifferanceFromToday)
        {
            return  Forecasts.
                    Where(m => m.timeFrom.DayOfWeek == DateTime.Now.AddDays(dayDifferanceFromToday).DayOfWeek).
                    ToList<Forecast>();
        }

        public Geoname Geoname { get; set; }

        public IEnumerable<Geoname> Geonames { get; set; }

        public IEnumerable<Forecast> Forecasts { get; set; }

    }
}
