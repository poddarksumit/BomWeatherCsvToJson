using System.Collections.Generic;

namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Model for monthly aggregates.
    /// </summary>
    public class WeatherMonthlyAggregates
    {
        public WeatherMonthlyAggregates()
        {
            WeatherDataForMonth = new List<MonthlyWeatherData>();
        }

        /// <summary>
        /// Gets or sets the list of monthly weather data.
        /// </summary>
        public List<MonthlyWeatherData> WeatherDataForMonth { get; set; }
    }
}
