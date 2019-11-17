namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Gets or sets the yearly weather data.
    /// </summary>
    public class YearlyWeatherData : BaseWeatherData
    {
        /// <summary>
        /// Gets or sets year.
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets monthly aggregates.
        /// </summary>
        public WeatherMonthlyAggregates MonthlyAggregates { get; set; }

        /// <summary>
        /// Gets or sets longest number of rain days.
        /// </summary>
        public string LongestNumberOfDaysRaining {get; set; }
    }
}
