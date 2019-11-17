using System.Collections.Generic;
using BomWeatherCsvToJson.Model.Input;

namespace BomWeatherCsvToJson.BusinessLogic.Interface
{
    /// <summary>
    /// Interface for CSV Handler.
    /// </summary>
    public interface ICsvFileHandler
    {
        /// <summary>
        /// Method to read CSV file and return data in List.
        /// </summary>
        /// <param name="filePath">Path of the CSV file.</param>
        /// <returns>List of data in CSV file.</returns>
        List<WeatherData> ReadCsvFile(string filePath);
    }
}
