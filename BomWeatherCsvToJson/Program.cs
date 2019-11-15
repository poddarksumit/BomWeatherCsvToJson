using BomWeatherCsvToJson.Extensions;
using InputData = BomWeatherCsvToJson.Model.Input;
using BomWeatherCsvToJson.Model.Output;
using BomWeatherCsvToJson.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ServiceProvider serviceProvider = ConfigureSerivces();

            Console.WriteLine("--- Welcome to BOM weather CSV data to JSON ---");
            Console.Write("Please add the location of the file - ");
            string csvDataLocation = Console.ReadLine();

            // /Users/sumitpoddar/Desktop/IDCJAC0009_066062_1800/IDCJAC0009_066062_1800_Data.csv

            ProcessCsvToJson processCsvToJson = serviceProvider.GetService<ProcessCsvToJson>();
            processCsvToJson.Process("/Users/sumitpoddar/Desktop/IDCJAC0009_066062_1800/IDCJAC0009_066062_1800_Data.csv");

            Console.WriteLine($"Location of the file is {processCsvToJson.OutputFilePath}");
        }

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
