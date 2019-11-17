namespace BomWeatherCsvToJson.BusinessLogic.Interface
{
    /// <summary>
    /// Interface for file path validation.
    /// </summary>
    public interface IFilePathValidator
    {
        /// <summary>
        /// Method to validate the file path.
        /// </summary>
        /// <param name="filePath">Path of the CSV file.</param>
        /// <returns>Bool, indicating wheather the path is valid or not.</returns>
        bool IsFilePathValid(string filePath);
    }
}
