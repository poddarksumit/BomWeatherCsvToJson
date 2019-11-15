using System;
using CsvHelper.Configuration.Attributes;

namespace BomWeatherCsvToJson.Model.Input
{
    /// <summary>
    /// Model to hold the data from CSV.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Gets and sets the product code.
        /// </summary>
        [Name("Product code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or Sets the BOM station number
        /// </summary>
        [Name("Bureau of Meteorology station number")]
        public string BomStationNumber { get; set; }

        /// <summary>
        /// Gets or Sets the year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or Sets the month.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Gets or Sets the day.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Gets or Sets the rainfall amount.
        /// </summary>
        [Name("Rainfall amount (millimetres)")]
        public decimal? RainfallAmount { get; set; }

        /// <summary>
        /// Gets or Sets the day.
        /// </summary>
        [Name("Period over which rainfall was measured (days)")]
        public int? PeriodOfRainfallMeasured { get; set; }

        /// <summary>
        /// Gets or Sets the quality.
        /// </summary>
        public string Quality { get; set; }

        public string FormatDate()
        {
            return $"{Year}-{Month}-{Day}";
        }
    }
}
