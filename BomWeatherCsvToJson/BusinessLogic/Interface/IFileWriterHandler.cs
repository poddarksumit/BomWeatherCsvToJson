namespace BomWeatherCsvToJson.BusinessLogic.Interface
{
    /// <summary>
    /// Interface for writing the data into JSON feed.
    /// </summary>
    public interface IFileWriterHandler
    {
        /// <summary>
        /// Method to export the data in the file specified.
        /// </summary>
        /// <param name="path">Path where data is to be exported.</param>
        /// <param name="feedData">The data to write in feed.</param>
        /// <returns>Bool, indicating the file writing operation is successful or not</returns>
        bool ExportData(string path, string feedData);

        /// <summary>
        /// Method to convert object into JSON and export it in the file specified.
        /// </summary>
        /// <typeparam name="T">Class to generize the type of object to be passed.</typeparam>
        /// <param name="path">Path where data is to be exported.</param>
        /// <param name="jsonFeedObject">The data to write in feed.</param>
        /// <returns>Bool, indicating the file writing operation is successful or not</returns>
        bool ExportJsonInFile<T>(string path, T jsonFeedObject)
            where T : class, new();
    }
}
