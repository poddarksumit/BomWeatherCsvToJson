using System;
using System.Collections.Generic;

namespace BomWeatherCsvToJson.Model.Output
{
    public class WeatherData
    {
        public WeatherData()
        {
            WeatherDataForYear = new List<YearlyWeatherData>();
        }

        public List<YearlyWeatherData> WeatherDataForYear { get; set; }
    }
}
