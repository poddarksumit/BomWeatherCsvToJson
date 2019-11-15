using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using BomWeatherCsvToJson.Config;
using BomWeatherCsvToJson.Extensions;
using BomWeatherCsvToJson.Model.Input;
using BomWeatherCsvToJson.Model.Output;

namespace BomWeatherCsvToJson.BusinessLogic
{
    public class ProcessCsvToJson
    {
        /// <summary>
        /// Handler for file path validation
        /// </summary>
        private IFilePathValidator FilePathValidator { get; set; }

        /// <summary>
        /// Handler for CSV file handler
        /// </summary>
        private ICsvFileHandler CsvFileHandler { get; set; }

        /// <summary>
        /// Handler for file writing handler
        /// </summary>
        private IFileWriterHandler FileWriterHandler { get; set; }

        /// <summary>
        /// Path for output json feed.
        /// </summary>
        public string OutputFilePath { get; set; }

        /// <summary>
        /// JSON object of weather data
        /// </summary>
        private WeatherDataJson WeatherDataJson { get; set; }

        /// <summary>
        /// Configs
        /// </summary>
        private Settings Config { get; set; }

        public ProcessCsvToJson(IFilePathValidator filePathValidator, ICsvFileHandler csvFileHandler, IFileWriterHandler fileWriterHandler, Settings settings)
        {
            FilePathValidator = filePathValidator;
            CsvFileHandler = csvFileHandler;
            FileWriterHandler = fileWriterHandler;
            Config = settings;
        }

        public bool Process(string inputFilePath)
        {
            bool isProcessed = false;

            // Validate the path is valid before proceeding.
            if (FilePathValidator.IsFilePathValid(inputFilePath))
            {
                var weatherRecords = CsvFileHandler.ReadCsvFile(inputFilePath);
                if (weatherRecords.Any())
                {
                    // Process CSV records and update WeatherDataJson object
                    ProcessWeatherRecords(weatherRecords);
                    // Extract json object to json file
                    OutputFilePath = String.Format(Config.OutputFilePath, DateTime.Now.ToString("yyyyMMddTHH:mm:ss"));
                    FileWriterHandler.ExportJsonInFile(OutputFilePath, WeatherDataJson);
                    isProcessed = true;
                }
                else
                {
                    Console.WriteLine("No data available to process in the CSV file.");
                }
            }

            return isProcessed;
        }

        private void ProcessWeatherRecords(List<Model.Input.WeatherData> weatherRecords)
        {
            int initialYear = weatherRecords.First().Year;

            WeatherDataJson = new WeatherDataJson
            {
                WeatherData = new Model.Output.WeatherData()
            };

            while (initialYear >= weatherRecords.Last().Year)
            {
                var recordsForYear = weatherRecords.Where(x => x.Year == initialYear).OrderBy(x => x.Month).ThenBy(x => x.Day).ToList();

                if (recordsForYear.Any())
                {
                    YearlyWeatherData yearlyWeatherData = BuildYearlyData(recordsForYear);
                    yearlyWeatherData.MonthlyAggregates = new WeatherMonthlyAggregates()
                    {
                        WeatherDataForMonth = BuildMonthlyData(recordsForYear)
                    };
                    WeatherDataJson.WeatherData.WeatherDataForYear.Add(yearlyWeatherData);
                }
                initialYear--;
            }
        }

        private List<MonthlyWeatherData> BuildMonthlyData(List<Model.Input.WeatherData> recordsForYear)
        {
            List<MonthlyWeatherData> monthlyWeatherDta = new List<MonthlyWeatherData>();

            int lastMonthIndex = recordsForYear.First().Year == DateTime.Now.Year ? DateTime.Now.Month - 1 : recordsForYear.Last().Month;
            for (int i = 0; i < lastMonthIndex; i++)
            {
                List<Model.Input.WeatherData> monthDataList = recordsForYear.Where(x => x.Month == i+1).ToList();
                var monthlyWeatherData = new MonthlyWeatherData
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1)
                };

                SetBaseWeatherData(recordsForYear, monthlyWeatherData);

                // Set median

                var medianRainfallData = monthDataList.Where(x => x.RainfallAmount.HasValue).OrderBy(x => x.RainfallAmount).ToList();
                if (medianRainfallData.Any())
                {
                    monthlyWeatherData.MedianDailyRainfall = (medianRainfallData.Count % 2 != 0 ?
                                                                    // If odd, get middle
                                                                    medianRainfallData[medianRainfallData.Count / 2].RainfallAmount :
                                                                    // If even, (mid + (mid -1))/2
                                                                    (medianRainfallData[medianRainfallData.Count / 2].RainfallAmount +
                                                                        medianRainfallData[(medianRainfallData.Count / 2) - 1].RainfallAmount) / 2).ToString();
                }
                monthlyWeatherDta.Add(monthlyWeatherData);
            }


            return monthlyWeatherDta;
        }

        private static YearlyWeatherData BuildYearlyData(List<Model.Input.WeatherData> recordsForYear)
        {
            YearlyWeatherData yearlyWeatherData = new YearlyWeatherData
            {
                Year = recordsForYear.First().Year.ToString(),
                LongestNumberOfDaysRaining = recordsForYear.GetLongestRainfallDays().ToString()
            };

            SetBaseWeatherData(recordsForYear, yearlyWeatherData);

            return yearlyWeatherData;
        }

        public static void SetBaseWeatherData(List<Model.Input.WeatherData> inputWeatherData, BaseWeatherData outputWeatherData)
        {
            outputWeatherData.FirstRecordedDate = inputWeatherData.First(x => x.RainfallAmount.HasValue).FormatDate();
            outputWeatherData.LastRecordedDate = inputWeatherData.Last(x => x.RainfallAmount.HasValue).FormatDate();
            outputWeatherData.TotalRainfall = inputWeatherData.Sum(x => x.RainfallAmount).ToString();
            outputWeatherData.DaysWithRainfall = inputWeatherData.Where(x => x.RainfallAmount.HasValue && x.RainfallAmount > 0).Count().ToString();
            outputWeatherData.DaysWithNoRainfall = inputWeatherData.Where(x => !x.RainfallAmount.HasValue || x.RainfallAmount == 0).Count().ToString();

            if (outputWeatherData.TotalRainfall.AsDecimal() > 0 && outputWeatherData.DaysWithRainfall.AsInt() > 0)
            {
                outputWeatherData.AverageDailyRainfall = (outputWeatherData.TotalRainfall.AsDecimal() / outputWeatherData.DaysWithRainfall.AsInt()).ToString("0.#####");
            }
        }
    }
}
