using System.Collections.Generic;

namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Model for weather data.
    /// </summary>
    public class WeatherData
    {
        public WeatherData()
        {
            WeatherDataForYear = new List<YearlyWeatherData>();
        }

        /// <summary>
        /// Gets or sets the yearly weather data.
        /// </summary>
        public List<YearlyWeatherData> WeatherDataForYear { get; set; }
    }
}
