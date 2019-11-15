using System;
using System.IO;
using BomWeatherCsvToJson.BusinessLogic.Interface;
using NUnit.Framework;

namespace BomWeatherCsvToJsonTests.UnitTests.BusinessLogic
{
    public class FilePathValidatorTests : BaseTest
    {
        private IFilePathValidator FilePathValidator { get; set; }

        [SetUp]
        public void Init()
        {
            FilePathValidator = GetService<IFilePathValidator>();
        }

        [Test]
        public void TestPathEmpty_ThrowExcpetion()
        {
            Assert.Throws<Exception>(() => FilePathValidator.IsFilePathValid(""));
        }

        [Test]
        public void TestPathNull_ThrowExcpetion()
        {
            Assert.Throws<Exception>(() => FilePathValidator.IsFilePathValid(null));
        }

        [Test]
        public void TestPathWhiteSpace_ThrowExcpetion()
        {
            Assert.Throws<Exception>(() => FilePathValidator.IsFilePathValid("  "));
        }

        [Test]
        public void TestPathNonCsv_ThrowExcpetion()
        {
            Assert.Throws<Exception>(() => FilePathValidator.IsFilePathValid("abc/sda/tegs"));
        }

        [Test]
        public void TestPathPathNotFound_ThrowExcpetion()
        {
            Assert.Throws<FileNotFoundException>(() => FilePathValidator.IsFilePathValid("abc/sda/tegs.csv"));
        }

        [Test]
        public void TestPathPath_Success()
        {
            Assert.True(FilePathValidator.IsFilePathValid("SampleInputFile.csv"));
        }
    }
}
