using System;
using System.Collections.Generic;
using System.Linq;
using BomWeatherCsvToJson.BusinessLogic;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using BomWeatherCsvToJson.Config;
using BomWeatherCsvToJson.Model.Input;
using NUnit.Framework;

namespace BomWeatherCsvToJsonTests.UnitTests.BusinessLogic
{
    public class ProcessCsvToJsonTests : BaseTest
    {
        private ProcessCsvToJson ProcessCsvToJson { get; set; }

        [Test]
        public void TestNoData()
        {
            ProcessCsvToJson = GetService<ProcessCsvToJson>();
            bool isProcessed = ProcessCsvToJson.Process("Test_NoData.csv");
            Assert.IsFalse(isProcessed);
        }

        [Test]
        public void TestInvalidCsvData()
        {
            ProcessCsvToJson = GetService<ProcessCsvToJson>();
            bool isProcessed = ProcessCsvToJson.Process("Test_InvalidData.csv");
            Assert.IsFalse(isProcessed);
        }

        [Test]
        public void TestValidCsvData()
        {
            ProcessCsvToJson = GetService<ProcessCsvToJson>();
            bool isProcessed = ProcessCsvToJson.Process("SampleInputFile.csv");
            Assert.IsTrue(isProcessed);
            var result = ProcessCsvToJson.WeatherDataJson;
            Assert.AreEqual(2, result.WeatherData.WeatherDataForYear.Count);
            Assert.AreEqual(1, result.WeatherData.WeatherDataForYear.FirstOrDefault(x => x.Year == "1858").MonthlyAggregates.WeatherDataForMonth.Count);
            Assert.IsNull(result.WeatherData.WeatherDataForYear.FirstOrDefault(x => x.Year == "1858").FirstRecordedDate);
            Assert.IsNull(result.WeatherData.WeatherDataForYear.FirstOrDefault(x => x.Year == "1858").LastRecordedDate);
            Assert.AreEqual(7, result.WeatherData.WeatherDataForYear.FirstOrDefault(x => x.Year == "2019").MonthlyAggregates.WeatherDataForMonth.Count);
            Assert.AreEqual("7", result.WeatherData.WeatherDataForYear.FirstOrDefault(x => x.Year == "2019").DaysWithRainfall);

        }
    }
}
