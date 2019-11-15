using System.Collections.Generic;
using System.IO;
using System.Linq;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using BomWeatherCsvToJson.Model.Input;
using CsvHelper;

namespace BomWeatherCsvToJson.BusinessLogic
{
    public class CsvFileHandler : ICsvFileHandler
    {
        public List<WeatherData> ReadCsvFile(string filePath)
        {
            List<WeatherData> records = new List<WeatherData>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;

                records = csv.GetRecords<WeatherData>().OrderByDescending(x => x.Year).ToList();
            }

            return records;
        }
    }
}
