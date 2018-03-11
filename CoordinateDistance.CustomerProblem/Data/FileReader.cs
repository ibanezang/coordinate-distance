using System;
using System.IO;
using System.Collections.Generic;
using CoordinateDistance.CustomerProblem.Log;
using System.Linq;

namespace CoordinateDistance.CustomerProblem.Data
{
    public interface IFileReader
    {
        List<string> ReadLines(string filePath);
    }
    public class FileReader : IFileReader
    {
        private ILogger _logger { get; }

        public FileReader(ILogger logger)
        {
            _logger = logger;
        }
        public List<string> ReadLines(string filePath)
        {
            try
            {
                return File.ReadLines(filePath).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogMessage(LogLevel.Error, ex.Message);
                return null;
            }
        }
    }
}
