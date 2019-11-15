using System;
using System.IO;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using Newtonsoft.Json;

namespace BomWeatherCsvToJson.BusinessLogic
{
    /// <summary>
    /// Handler for file writing operation.
    /// </summary>
    public class FileWriterHandler : IFileWriterHandler
    {
        /// <summary>
        /// Method to export the data in the file specified.
        /// </summary>
        /// <param name="path">Path where data is to be exported.</param>
        /// <param name="feedData">The data to write in feed.</param>
        /// <returns>Bool, indicating the file writing operation is successful or not</returns>
        public bool ExportData(string path, string feedData)
        {
            File.WriteAllText(path, feedData);
            return true;
        }

        /// <summary>
        /// Method to convert object into JSON and export it in the file specified.
        /// </summary>
        /// <typeparam name="T">Class to generize the type of object to be passed.</typeparam>
        /// <param name="path">Path where data is to be exported.</param>
        /// <param name="jsonFeedObject">The data to write in feed.</param>
        /// <returns>Bool, indicating the file writing operation is successful or not</returns>
        public bool ExportJsonInFile<T>(string path, T jsonFeedObject)
            where T: class, new()
        {
            if (jsonFeedObject != null && path.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                return ExportData(path, JsonConvert.SerializeObject(jsonFeedObject, Formatting.Indented));
            }
            return false;
        }
    }
}
