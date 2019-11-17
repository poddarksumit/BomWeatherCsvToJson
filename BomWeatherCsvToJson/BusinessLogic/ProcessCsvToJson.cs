using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using BomWeatherCsvToJson.Config;
using BomWeatherCsvToJson.Extensions;
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
        /// Configs
        /// </summary>
        private Settings Config { get; set; }

        /// <summary>
        /// Weather data json object.
        /// </summary>
        public WeatherDataJson WeatherDataJson { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePathValidator">Instance of path validator handler.</param>
        /// <param name="csvFileHandler">Instance of csv file reader handler.</param>
        /// <param name="fileWriterHandler">Instance of file writing handler.</param>
        /// <param name="settings">Instance of config settings.</param>
        public ProcessCsvToJson(IFilePathValidator filePathValidator, ICsvFileHandler csvFileHandler, IFileWriterHandler fileWriterHandler, Settings settings)
        {
            FilePathValidator = filePathValidator;
            CsvFileHandler = csvFileHandler;
            FileWriterHandler = fileWriterHandler;
            Config = settings;
        }

        /// <summary>
        /// Main method to process the file path.
        /// </summary>
        /// <param name="inputFilePath">Input path of CSV to be processed.</param>
        /// <returns>Bool, indicating wheather the operation is successful or not.</returns>
        public bool Process(string inputFilePath)
        {
            bool isProcessed = false;

            try
            {
                // Validate the path is valid before proceeding.
                if (FilePathValidator.IsFilePathValid(inputFilePath))
                {
                    var weatherRecords = CsvFileHandler.ReadCsvFile(inputFilePath);
                    if (weatherRecords.Any())
                    {
                        // Process CSV records and update WeatherDataJson object
                        ProcessWeatherRecords(weatherRecords);
                        // Extract json object to json file
                        OutputFilePath = string.Format(Config.OutputFilePath, DateTime.Now.ToString("yyyyMMddTHH:mm:ss"));
                        FileWriterHandler.ExportJsonInFile(OutputFilePath, WeatherDataJson);

                        isProcessed = true;
                    }
                    else
                    {
                        Console.WriteLine("No data available to process in the CSV file.");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Some exception occurred while processing the request: {e.Message} at {e.StackTrace}");
            }

            return isProcessed;
        }

        /// <summary>
        /// Method to process weather records.
        /// </summary>
        /// <param name="weatherRecords">List of weather data to process.</param>
        /// <returns>Processed data.</returns>
        public void ProcessWeatherRecords(List<Model.Input.WeatherData> weatherRecords)
        {
            int initialYear = weatherRecords.First().Year;

            WeatherDataJson = new WeatherDataJson
            {
                WeatherData = new WeatherData()
            };

            while (initialYear >= weatherRecords.Last().Year)
            {
                var recordsForYear = weatherRecords.Where(x => x.Year == initialYear)?.OrderBy(x => x.Month).ThenBy(x => x.Day).ToList();

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

        /// <summary>
        /// Method to build monthly data.
        /// </summary>
        /// <param name="recordsForYear">List of weather data to process.</param>
        /// <returns>Processed data.</returns>
        private List<MonthlyWeatherData> BuildMonthlyData(List<Model.Input.WeatherData> recordsForYear)
        {
            List<MonthlyWeatherData> monthlyWeatherDta = new List<MonthlyWeatherData>();

            int lastMonthIndex = recordsForYear.First().Year == DateTime.Now.Year ? DateTime.Now.Month - 1 : recordsForYear.Last().Month;
            for (int i = 0; i < lastMonthIndex; i++)
            {
                List<Model.Input.WeatherData> monthDataList = recordsForYear.Where(x => x.Month == i+1).ToList();
                if (monthDataList.Any())
                {
                    var monthlyWeatherData = new MonthlyWeatherData
                    {
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1)
                    };

                    SetBaseWeatherData(monthDataList, monthlyWeatherData);

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
            }

            return monthlyWeatherDta;
        }

        /// <summary>
        /// Method to build yearly data
        /// </summary>
        /// <param name="recordsForYear">List of weather data to process.</param>
        /// <returns>Processed data.</returns>
        public YearlyWeatherData BuildYearlyData(List<Model.Input.WeatherData> recordsForYear)
        {
            YearlyWeatherData yearlyWeatherData = new YearlyWeatherData
            {
                Year = recordsForYear.First().Year.ToString(),
                LongestNumberOfDaysRaining = recordsForYear.GetLongestRainfallDays().ToString()
            };

            SetBaseWeatherData(recordsForYear, yearlyWeatherData);

            return yearlyWeatherData;
        }

        /// <summary>
        /// Method to set the basic weather data across, yearly and monthly object.
        /// </summary>
        /// <param name="inputWeatherData">List of weather data.</param>
        /// <param name="outputWeatherData">Calculated weather data.</param>
        public void SetBaseWeatherData(List<Model.Input.WeatherData> inputWeatherData, BaseWeatherData outputWeatherData)
        {
            outputWeatherData.FirstRecordedDate = inputWeatherData.FirstOrDefault(x => x.RainfallAmount.HasValue)?.FormatDate();
            outputWeatherData.LastRecordedDate = inputWeatherData.LastOrDefault(x => x.RainfallAmount.HasValue)?.FormatDate();
            outputWeatherData.TotalRainfall = inputWeatherData.Sum(x => x.RainfallAmount).ToString();
            outputWeatherData.DaysWithRainfall = inputWeatherData.Count(x => x.RainfallAmount.HasValue && x.RainfallAmount > 0).ToString();
            outputWeatherData.DaysWithNoRainfall = inputWeatherData.Count(x => !x.RainfallAmount.HasValue || x.RainfallAmount == 0).ToString();

            if (outputWeatherData.TotalRainfall.AsDecimal() > 0 && outputWeatherData.DaysWithRainfall.AsInt() > 0)
            {
                outputWeatherData.AverageDailyRainfall = (outputWeatherData.TotalRainfall.AsDecimal() / outputWeatherData.DaysWithRainfall.AsInt()).ToString("0.#####");
            }
        }
    }
}
