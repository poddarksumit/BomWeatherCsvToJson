using BomWeatherCsvToJson.BusinessLogic.Interface;

namespace BomWeatherCsvToJsonTests
{
    /// <summary>
    /// Mock for file writing handler.
    /// </summary>
    public class FileWriterHandlerMock : IFileWriterHandler
    {

        public bool ExportData(string path, string feedData)
        {
            return true;
        }

        public bool ExportJsonInFile<T>(string path, T jsonFeedObject) where T : class, new()
        {
            return true;
        }
    }
}
