namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Main model for weather data as json for response.
    /// </summary>
    public class WeatherDataJson
    {
        /// <summary>
        /// Gets or sets the weather data.
        /// </summary>
        public WeatherData WeatherData { get; set; }
    }
}
