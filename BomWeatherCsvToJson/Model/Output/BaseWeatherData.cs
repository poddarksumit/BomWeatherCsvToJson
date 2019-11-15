using System;
namespace BomWeatherCsvToJson.Model.Output
{
    /// <summary>
    /// Model for JSON weather data.
    /// </summary>
    public class BaseWeatherData
    {
        public string FirstRecordedDate { get; set; }
        public string LastRecordedDate { get; set; }
        public string TotalRainfall { get; set; }
        public string AverageDailyRainfall { get; set; }
        public string DaysWithNoRainfall { get; set; }
        public string DaysWithRainfall { get; set; }

    }
}
