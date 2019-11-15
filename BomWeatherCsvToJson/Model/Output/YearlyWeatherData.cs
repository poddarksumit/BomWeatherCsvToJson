namespace BomWeatherCsvToJson.Model.Output
{
    public class YearlyWeatherData : BaseWeatherData
    {
        public string Year { get; set; }
        public WeatherMonthlyAggregates MonthlyAggregates { get; set; }
        public string LongestNumberOfDaysRaining {get; set; }
    }
}
