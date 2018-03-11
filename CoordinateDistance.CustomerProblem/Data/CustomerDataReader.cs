using System;
using System.Collections.Generic;
using CoordinateDistance.CustomerProblem.Log;
using CoordinateDistance.Library.Formula;
using Newtonsoft.Json;

namespace CoordinateDistance.CustomerProblem.Data
{
    public interface ICustomerDataReader
    {
        List<CustomerDistance> GetCustomerDistancesFromFile(Coordinate pointRef, string filePath);
    }

    public class CustomerDataReader : ICustomerDataReader
    {
        private readonly IGeoFormula _geoFormula;
        private readonly IFileReader _fileReader;
        private readonly ILogger _logger;
        public CustomerDataReader(IGeoFormula geoFormula, IFileReader fileReader, ILogger logger)
        {
            _geoFormula = geoFormula;
            _fileReader = fileReader;
            _logger = logger;
        }

        public List<CustomerDistance> GetCustomerDistancesFromFile(Coordinate pointRef, string filePath)
        {
            var customerDistances = new List<CustomerDistance>();
            var customerJsonList = _fileReader.ReadLines(filePath);

            if (customerJsonList == null || customerJsonList.Count == 0)
            {
                return customerDistances;
            }

            foreach (var json in customerJsonList)
            {
                try
                {
                    var customerRecord = JsonConvert.DeserializeObject<CustomerRecord>(json);
                    var distance = _geoFormula.CalculateEarthCoordinateDistance(
                        pointRef, new Coordinate(customerRecord.Latitude, customerRecord.Longitude));
                    customerDistances.Add(new CustomerDistance(customerRecord, distance));
                    _logger.LogMessage(LogLevel.Debug, $"{customerRecord.UserId}.{customerRecord.Name}.{distance}");
                }
                catch (Exception ex)
                {
                    _logger.LogMessage(LogLevel.Error, ex.Message);
                }
            }

            return customerDistances;
        }
    }
}
