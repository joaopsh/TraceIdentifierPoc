using System.Collections.Immutable;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace TraceIdentifierPoc.Logger
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        static readonly AsyncLocal<ImmutableStack<IImmutableDictionary<string, object>>> ScopeData = new AsyncLocal<ImmutableStack<IImmutableDictionary<string, object>>>();

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
