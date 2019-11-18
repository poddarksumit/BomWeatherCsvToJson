using System;
using System.IO;
using System.Linq;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using NUnit.Framework;

namespace BomWeatherCsvToJsonTests.UnitTests.BusinessLogic
{
    public class CsvFileHandlerTests : BaseTest
    {
        private ICsvFileHandler CsvFileHandler{ get; set; }

        [SetUp]
        public void Init()
        {
            CsvFileHandler = GetService<ICsvFileHandler>();
        }

        [Test]
        public void TestPathEmpty_ThrowExcpetion()
        {
            Assert.Throws<ArgumentException>(() => CsvFileHandler.ReadCsvFile(""));
        }

        [Test]
        public void TestPathNonCsv_ThrowExcpetion()
        {
            Assert.Throws<FileNotFoundException>(() => CsvFileHandler.ReadCsvFile("SampleInputFile"));
        }

        [Test]
        public void TestFileNotFound_ThrowExcpetion()
        {
            Assert.Throws<FileNotFoundException>(() => CsvFileHandler.ReadCsvFile("../SampleInputFile-fail.csv"));
        }

        [Test]
        public void TestCsvReader_Success()
        {
            var records = CsvFileHandler.ReadCsvFile("SampleInputFile.csv");
            Assert.NotNull(records);
            Assert.True(records.Any());
            Assert.AreEqual(25, records.Count());
        }

        [Test]
        public void TestOrderOfYear_Success()
        {
            var records = CsvFileHandler.ReadCsvFile("SampleInputFile.csv");
            Assert.AreEqual(2019, records.First().Year);
        }
    }
}
