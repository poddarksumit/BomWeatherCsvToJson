using System.Collections.Generic;

namespace BomWeatherCsvToJson.Model.Output
{
    public class WeatherMonthlyAggregates
    {
        public WeatherMonthlyAggregates()
        {
            WeatherDataForMonth = new List<MonthlyWeatherData>();
        }

        public List<MonthlyWeatherData> WeatherDataForMonth { get; set; }
    }
}
