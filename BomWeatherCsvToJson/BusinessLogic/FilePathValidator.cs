using System;
using System.IO;
using BomWeatherCsvToJson.BusinessLogic.Interface;

namespace BomWeatherCsvToJson.BusinessLogic
{
    /// <summary>
    /// Handler for file path validator.
    /// </summary>
    public class FilePathValidator : IFilePathValidator
    {
        /// <summary>
        /// Method to validate the file path.
        /// </summary>
        /// <param name="filePath">Path of the CSV file.</param>
        /// <returns>Bool, indicating wheather the path is valid or not.</returns>
        public bool IsFilePathValid(string filePath)
        {
            // Null/Empty check.
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception("Input file path empty.");
            }

            // Extension check.
            if (!filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Invalid file path extension, should be a CSV.");
            }

            // File exists check.
            if(!File.Exists(filePath)){
                throw new FileNotFoundException();
            }

            return true;
        }
    }
}
