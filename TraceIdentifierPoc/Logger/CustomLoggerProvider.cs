using System;
using Microsoft.Extensions.Logging;

namespace TraceIdentifierPoc.Logger
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
