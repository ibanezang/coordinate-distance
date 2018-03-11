using CoordinateDistance.CustomerProblem.Log;
using Shouldly;
using Xunit;

namespace CoordinateDistance.CustomerProblem.Tests.Log
{
    public class InMemoryLoggerTest
    {
        private InMemoryLogger _inMemoryLogger;

        public InMemoryLoggerTest()
        {
            _inMemoryLogger = new InMemoryLogger();
        }

        [Fact]
        public void LogMessage_LogSeveralMessage_ShouldGetCorrectLogMessage()
        {
            _inMemoryLogger.LogMessage(LogLevel.Debug, "This is debug message!");
            _inMemoryLogger.LogMessage(LogLevel.Error, "This is error message!");
            _inMemoryLogger.LogItems.Count.ShouldBe(2);
            _inMemoryLogger.LogItems[0].GetLogMessage().ShouldBe("Debug : This is debug message!");
            _inMemoryLogger.LogItems[1].GetLogMessage().ShouldBe("Error : This is error message!");
        }
    }
}
