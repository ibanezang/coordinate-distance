using System.Collections.Generic;
using CoordinateDistance.CustomerProblem.Data;
using CoordinateDistance.CustomerProblem.Log;
using CoordinateDistance.Library.Formula;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CoordinateDistance.CustomerProblem.Tests.Data
{
    public class CustomerDataReaderTest
    {
        private IGeoFormula _geoFormula;
        private ILogger _logger;
        private IFileReader _fileReader;
        private CustomerDataReader _customerDataReader;

        private Coordinate _testCoordinate = new Coordinate(53.339428, -6.257664);
        private const double TestDistance = 54;
        public CustomerDataReaderTest()
        {
            _logger = Substitute.For<ILogger>();
            _fileReader = Substitute.For<IFileReader>();
            _geoFormula = Substitute.For<IGeoFormula>();
            _geoFormula.CalculateEarthCoordinateDistance(Arg.Is(_testCoordinate), Arg.Any<Coordinate>())
                       .Returns(TestDistance);
            _customerDataReader = new CustomerDataReader(_geoFormula, _fileReader, _logger);
        }

        [Fact]
        public void GetCustomerRecordFromFile_GetNullFromFileReader_ShouldReturnEmptyCustomerRecords()
        {
            _fileReader.ReadLines("xyz.txt").Returns(x => null);
            var result = _customerDataReader.GetCustomerDistancesFromFile(_testCoordinate, "xyz.txt");
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetCustomerRecordFromFile_GetEmptyFromFileReader_ShouldReturnEmptyCustomerRecords()
        {
            _fileReader.ReadLines("xyz.txt").Returns(new List<string>());
            var result = _customerDataReader.GetCustomerDistancesFromFile(_testCoordinate, "xyz.txt");
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetCustomerRecordFromFile_GetAllValidJsonData_ShouldReturnCorrectCustomerRecords()
        {
            _fileReader.ReadLines("xyz.txt").Returns(
                new List<string>
                {
                    "{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}",
                    "{\"latitude\": \"51.92893\", \"user_id\": 1, \"name\": \"Alice Cahill\", \"longitude\": \"-10.27699\"}"
                });
            var result = _customerDataReader.GetCustomerDistancesFromFile(_testCoordinate, "xyz.txt");
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result[0].Customer.UserId.ShouldBe(12);
            result[0].Customer.Name.ShouldBe("Christina McArdle");
            result[0].Customer.Latitude.ShouldBe(52.986375);
            result[0].Customer.Longitude.ShouldBe(-6.043701);
            result[0].DistanceInKm.ShouldBe(TestDistance);
            result[1].Customer.UserId.ShouldBe(1);
            result[1].Customer.Name.ShouldBe("Alice Cahill");
            result[1].Customer.Latitude.ShouldBe(51.92893);
            result[1].Customer.Longitude.ShouldBe(-10.27699);
            result[1].DistanceInKm.ShouldBe(TestDistance);
        }

        [Fact]
        public void GetCustomerRecordFromFile_GetOneInValidJsonData_ShouldReturnOnlyCorrectCustomerRecords()
        {
            _fileReader.ReadLines("xyz.txt").Returns(
                new List<string>
                {
                    "{latitude: 52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \" -6.043701\"}",
                    "{\"latitude\": \"51.92893\", \"user_id\": 1, \"name\": \"Alice Cahill\", \"longitude\": \"-10.27699\"}"
            });
            var result = _customerDataReader.GetCustomerDistancesFromFile(_testCoordinate, "xyz.txt");
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].Customer.UserId.ShouldBe(1);
            result[0].Customer.Name.ShouldBe("Alice Cahill");
            result[0].Customer.Latitude.ShouldBe(51.92893);
            result[0].Customer.Longitude.ShouldBe(-10.27699);
            result[0].DistanceInKm.ShouldBe(TestDistance);
            _logger.Received().LogMessage(LogLevel.Error, Arg.Any<string>());
        }
    }
}
