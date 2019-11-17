namespace BomWeatherCsvToJson.Extensions
{
    /// <summary>
    /// Extension for string data.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Get string as decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal AsDecimal(this string value)
        {
            return decimal.Parse(value);
        }

        public static int AsInt(this string value)
        {
            return int.Parse(value);
        }

        public static double AsDouble(this string value)
        {
            return double.Parse(value);
        }
    }
}
