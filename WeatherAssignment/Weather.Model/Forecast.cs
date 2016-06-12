using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model
{
    [MetadataType(typeof(Forecast_Metadata))]
    public partial class Forecast
    {
        public Forecast()
        {
            //Empty   
        }

        public string GetSymbolFromWeatherMapSrc(Forecast forecast)
        {
            return $"http://openweathermap.org/img/w/{forecast.symbolVar}.png";
        }

        public class Forecast_Metadata
        {
            [Required]
            [DisplayName("Forecast Id")]
            public int forecastId { get; set; }
            [Required]
            [DisplayName("Geoname Id")]
            public string geonameId { get; set; }
            [Required]
            [DisplayName("Symbol Nr")]
            public string symbolNumer { get; set; }
            [Required]
            [DisplayName("Symbol Name")]
            public string symbolName { get; set; }
            [Required]
            [DisplayName("Symbol Var")]
            public string symbolVar { get; set; }
            [Required]
            [DisplayName("Wind Deg")]
            public string windDirectionDeg { get; set; }
            [Required]
            [DisplayName("Wind Code")]
            public string windDirectionCode { get; set; }
            [Required]
            [DisplayName("Wind Name")]
            public string windDirectionName { get; set; }
            [Required]
            [DisplayName("Wind Speed (Mps)")]
            public string windSpeedMps { get; set; }
            [Required]
            [DisplayName("Wind Speed Name")]
            public string windSpeedName { get; set; }
            [Required]
            [DisplayName("Temperature Unit")]
            public string temperatureUnit { get; set; }
            [Required]
            [DisplayName("Temperature Value")]
            public string temperatureValue { get; set; }
            [Required]
            [DisplayName("Temperature Min")]
            public string temperatureMin { get; set; }
            [Required]
            [DisplayName("Temperature Max")]
            public string temperatureMax { get; set; }
            [Required]
            [DisplayName("Pressure Unit")]
            public string pressureUnit { get; set; }
            [Required]
            [DisplayName("Pressure Value")]
            public string pressureValue { get; set; }
            [Required]
            [DisplayName("Humidity Value")]
            public string humidityValue { get; set; }
            [Required]
            [DisplayName("Humidity Unit")]
            public string humidityUnit { get; set; }
            [Required]
            [DisplayName("Clouds Value")]
            public string cloudsValue { get; set; }
            [Required]
            [DisplayName("Clouds All")]
            public string cloudsAll { get; set; }
            [Required]
            [DisplayName("Clouds Unit")]
            public string cloudsUnit { get; set; }
            [Required]
            [DisplayName("Time From")]
            [DataType(DataType.Date)]
            public System.DateTime timeFrom { get; set; }
            [Required]
            [DisplayName("Time To")]
            [DataType(DataType.Date)]
            public System.DateTime timeTo { get; set; }

        }

    }
}
