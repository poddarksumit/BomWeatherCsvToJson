namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Base model for JSON weather data.
    /// </summary>
    public class BaseWeatherData
    {
        /// <summary>
        /// Gets or sets the first recorded date.
        /// </summary>
        public string FirstRecordedDate { get; set; }

        /// <summary>
        /// Gets or sets the last recorded date.
        /// </summary>
        public string LastRecordedDate { get; set; }

        /// <summary>
        /// Gets or sets the total rainfall.
        /// </summary>
        public string TotalRainfall { get; set; }

        /// <summary>
        /// Gets or sets the average daily rainfall.
        /// </summary>
        public string AverageDailyRainfall { get; set; }

        /// <summary>
        /// Gets or sets the days with no rainfall.
        /// </summary>
        public string DaysWithNoRainfall { get; set; }

        /// <summary>
        /// Gets or sets the days with rainfall.
        /// </summary>
        public string DaysWithRainfall { get; set; }

    }
}
