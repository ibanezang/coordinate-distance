using CoordinateDistance.CustomerProblem.Data;
using CoordinateDistance.CustomerProblem.Log;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CoordinateDistance.CustomerProblem.Tests.Data
{
    public class FileReaderTest
    {
        private ILogger _logger;
        private FileReader _fileReader;

        public FileReaderTest()
        {
            _logger = Substitute.For<ILogger>();
            _fileReader = new FileReader(_logger);
        }

        [Fact]
        public void ReadLines_ReadCorrectFile_ShouldGetCorrectNumberOfLines()
        {
            var filePath = $"{System.IO.Directory.GetCurrentDirectory()}/../../../Data/testfile.txt";
            var result = _fileReader.ReadLines(filePath);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        [Fact]
        public void ReadLines_ReadInCorrectFilePath_ShouldGetNullAndLogError()
        {
            var nonExistenceFilePath = "xyz.txt";
            var result = _fileReader.ReadLines(nonExistenceFilePath);
            result.ShouldBeNull();
            _logger.Received().LogMessage(LogLevel.Error, Arg.Any<string>());
        }
    }
}
