namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Model for Monthly data, extends <see cref="BaseWeatherData"/>
    /// </summary>
    public class MonthlyWeatherData : BaseWeatherData
    {
        /// <summary>
        /// Gets or sets month.
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Gets or sets median daily rainfall.
        /// </summary>
        public string MedianDailyRainfall { get; set; }
    }
}
