//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weather.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Forecast
    {
        public int forecastId { get; set; }
        public string geonameId { get; set; }
        public string symbolNumer { get; set; }
        public string symbolName { get; set; }
        public string symbolVar { get; set; }
        public string windDirectionDeg { get; set; }
        public string windDirectionCode { get; set; }
        public string windDirectionName { get; set; }
        public string windSpeedMps { get; set; }
        public string windSpeedName { get; set; }
        public string temperatureUnit { get; set; }
        public string temperatureValue { get; set; }
        public string temperatureMin { get; set; }
        public string temperatureMax { get; set; }
        public string pressureUnit { get; set; }
        public string pressureValue { get; set; }
        public string humidityValue { get; set; }
        public string humidityUnit { get; set; }
        public string cloudsValue { get; set; }
        public string cloudsAll { get; set; }
        public string cloudsUnit { get; set; }
        public System.DateTime timeFrom { get; set; }
        public System.DateTime timeTo { get; set; }
    
        public virtual Geoname Geoname { get; set; }
    }
}
