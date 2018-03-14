using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using TraceIdentifierPoc.Service;
using Microsoft.Extensions.DependencyInjection;

namespace TraceIdentifierPoc.Logger
{
    public class CustomLogger : ILogger, IDisposable
    {
        // fields
        private Stack<IDictionary<string, object>> _scopedState = new Stack<IDictionary<string, object>>();

        // publics
        public IDisposable BeginScope<TState>(TState state)
        {
            var states = ParseState(state);

            if (states.Any())
            {
                _scopedState.Push(states);
            }

            return this;
        }
        
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // Gets previously scoped states
            var scopedStates = _scopedState.SelectMany(dictionary => dictionary).ToList();

            // Adds current states.
            scopedStates.AddRange(ParseState(state));

            //var traceIdentifier = ServiceLocator.ServiceProvider.GetService<ITraceIdentifierService>().Get();
            Console.WriteLine($"TraceIdentifier: {LoggerContext.GetTraceIdentifier()}");
        }

        public void Dispose()
        {
            if (_scopedState.Any())
            {
                _scopedState.Pop();
            }
        }

        // privates
        private IDictionary<string, object> ParseState<TState>(TState state)
        {
            var additionalData = new Dictionary<string, object>();

            // Get the arguments present in state
            var args = state as IEnumerable<KeyValuePair<string, object>>;

            // Create additional data for each argument informed
            // For more information about message templates, see: https://messagetemplates.org/
            // For more information about {OriginalFormat}, see: https://nblumhardt.com/2016/11/ilogger-beginscope/.
            if (args?.Any(arg => arg.Key.Equals("{OriginalFormat}")) == true)
            {
                additionalData = args
                    .Where(arg => !arg.Key.Equals("{OriginalFormat}"))
                    .ToDictionary(arg => arg.Key, arg => arg.Value);

                return additionalData;
            }

            return additionalData;
        }
    }
}
