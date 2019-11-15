namespace BomWeatherCsvToJson.Model.Output
{
    public class MonthlyWeatherData : BaseWeatherData
    {
        public string Month { get; set; }
        public string MedianDailyRainfall { get; set; }
    }
}
