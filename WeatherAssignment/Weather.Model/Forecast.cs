using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model
{
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
    }
}
