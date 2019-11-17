using BomWeatherCsvToJson.BusinessLogic;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using BomWeatherCsvToJson.Config;
using Microsoft.Extensions.DependencyInjection;

namespace BomWeatherCsvToJsonTests
{
    public class BaseTest
    {
        private ServiceProvider ServiceProvider { get; set; }

        public BaseTest()
        {
            Settings settings = new Settings
            {
                OutputFilePath = "../../../Model/Output/Json/BomJsonFeed-{0}.json"
            };

            ServiceProvider = new ServiceCollection()
                 .AddTransient<IFilePathValidator, FilePathValidator>()
                 .AddTransient<ICsvFileHandler, CsvFileHandler>()
                 .AddTransient<IFileWriterHandler, FileWriterHandlerMock>()
                 .AddTransient<ProcessCsvToJson>()
                 .AddSingleton(settings)
                 .BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}