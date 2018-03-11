using System.Collections.Generic;

namespace CoordinateDistance.CustomerProblem.Log
{
    public interface ILogger
    {
        void LogMessage(LogLevel logLevel, string message);
    }

    public enum LogLevel
    {
        Error,
        Debug
    }

    public class InMemoryLogger : ILogger
    {
        public List<LogItem> LogItems { get; }

        public InMemoryLogger()
        {
            LogItems = new List<LogItem>();
        }

        public void LogMessage(LogLevel logLevel, string message)
        {
            LogItems.Add(new LogItem(logLevel, message));
        }
    }

    public class LogItem
    {
        public LogLevel LogLevel { get; }
        public string Message { get; }
        public LogItem(LogLevel logLevel, string message)
        {
            LogLevel = logLevel;
            Message = message;
        }
        public string GetLogMessage()
        {
            return $"{LogLevel} : {Message}";
        }
    }
}
