using System.Collections.Generic;
using BomWeatherCsvToJson.Model.Input;

namespace BomWeatherCsvToJson.BusinessLogic.Interface
{
    public interface ICsvFileHandler
    {
        List<WeatherData> ReadCsvFile(string filePath);
    }
}
