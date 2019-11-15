using System;
namespace BomWeatherCsvToJson.Extensions
{
    public static class StringExtensions
    {
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
