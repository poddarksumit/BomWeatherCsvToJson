using BomWeatherCsvToJson.BusinessLogic;
using System;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using BomWeatherCsvToJson.Config;

namespace BomWeatherCsvToJson
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Configure services
            ServiceProvider serviceProvider = ConfigureSerivces();
            #endregion

            #region Get CSV file path
            Console.WriteLine("--- Welcome to BOM weather CSV data to JSON ---");
            Console.Write("Please add the location of the file - ");
            string csvDataLocation = Console.ReadLine();
            #endregion

            #region Process CSV file to JSON.
            ProcessCsvToJson processCsvToJson = serviceProvider.GetService<ProcessCsvToJson>();
            bool processedStatus = processCsvToJson.Process(csvDataLocation);
            #endregion

            if (processedStatus)
            {
                Console.WriteLine($"Location of the weather JSON feed is {processCsvToJson.OutputFilePath}");
            }
            else
            {
                Console.WriteLine($"JSON feed could not be generated because of some error (mentioned above).");
            }
            Console.WriteLine($"--- Thanks for using the CSV-JSON converter. ---");
        }

        /// <summary>
        /// Method to configure serices, config files.
        /// </summary>
        /// <returns>Configured instance of <see cref="ServiceProvider"/></returns>
        private static ServiceProvider ConfigureSerivces()
        {
            var builder = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();

            Settings settings = new Settings();
            configuration.GetSection("Settings").Bind(settings);

            var serviceProvider = new ServiceCollection()
                .AddTransient<IFilePathValidator, FilePathValidator>()
                .AddTransient<ICsvFileHandler, CsvFileHandler>()
                .AddTransient<IFileWriterHandler, FileWriterHandler>()
                .AddTransient<ProcessCsvToJson>()
                .AddSingleton(settings)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
