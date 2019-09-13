using System;

namespace JobLogger.LoggerApi.Infrastructure.Exceptions
{
    public class LoggerApiException : Exception
    {
        public LoggerApiException()
        { }

        public LoggerApiException(string message)
            : base(message)
        { }

        public LoggerApiException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
