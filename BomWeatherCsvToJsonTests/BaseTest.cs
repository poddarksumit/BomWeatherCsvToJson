using BomWeatherCsvToJson.BusinessLogic;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BomWeatherCsvToJsonTests
{
    public class BaseTest
    {
        private ServiceProvider ServiceProvider { get; set; }

        public BaseTest()
        {
            ServiceProvider = new ServiceCollection()
                 .AddTransient<IFilePathValidator, FilePathValidator>()
                 .AddTransient<ICsvFileHandler, CsvFileHandler>()
                 .AddTransient<IFileWriterHandler, FileWriterHandler>()
                 .AddTransient<ProcessCsvToJson>()
                 .BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}