using System;
using System.Collections.Generic;
using BomWeatherCsvToJson.BusinessLogic;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using BomWeatherCsvToJson.Config;
using BomWeatherCsvToJson.Model.Input;
using NUnit.Framework;

namespace BomWeatherCsvToJsonTests.UnitTests.BusinessLogic
{
    public class ProcessCsvToJsonTests : BaseTest
    {
        private ICsvFileHandler CsvFileHandler { get; set; }
        private ProcessCsvToJson ProcessCsvToJson { get; set; }

        [SetUp]
        public void Init()
        {
            ProcessCsvToJson = GetService<ProcessCsvToJson>();
        }

        [Test]
        public void TestSetBaseWeatherData()
        {
            List<WeatherData> inputWeatherData = new List<WeatherData>();
            inputWeatherData.Add(new WeatherData
            {

            });
        }
    }
}
