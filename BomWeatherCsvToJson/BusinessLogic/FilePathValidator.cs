using System;
using System.IO;
using BomWeatherCsvToJson.BusinessLogic.Interface;

namespace BomWeatherCsvToJson.BusinessLogic
{
    public class FilePathValidator : IFilePathValidator
    {
        public bool IsFilePathValid(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception("Input file path empty.");
            }

            if (!filePath.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Invalid file path extension, should be a CSV.");
            }

            if(!File.Exists(filePath)){
                throw new FileNotFoundException();
            }

            return true;
        }
    }
}
