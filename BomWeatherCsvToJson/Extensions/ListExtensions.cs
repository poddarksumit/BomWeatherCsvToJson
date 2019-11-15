using System.Collections.Generic;
using BomWeatherCsvToJson.Model.Input;

namespace BomWeatherCsvToJson.Extensions
{
    public static class ListExtensions
    {
        public static int GetLongestRainfallDays<T>(this List<T> currentList)
            where T: WeatherData
        {
            int interimDaysOfRainfall = 0, longestDaysOfRainfall = 0;
            foreach(T record in currentList)
            {
                if (!record.RainfallAmount.HasValue || record.RainfallAmount == 0)
                {
                    if (interimDaysOfRainfall > longestDaysOfRainfall)
                    {
                        longestDaysOfRainfall = interimDaysOfRainfall;
                    }
                    interimDaysOfRainfall = 0;
                }
                else
                {
                    interimDaysOfRainfall++;
                }
            }

            return interimDaysOfRainfall > longestDaysOfRainfall ? interimDaysOfRainfall : longestDaysOfRainfall;
        }
    }
}
