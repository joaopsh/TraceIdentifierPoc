using System;

namespace TraceIdentifierPoc.Service
{
    public class TraceIdentifierService : ITraceIdentifierService
    {
        private readonly string _traceIdentifier;

        public TraceIdentifierService(IServiceProvider serviceProvider, Func<IServiceProvider, string> traceIdentifierResolverFunc)
        {
            _traceIdentifier = traceIdentifierResolverFunc(serviceProvider);
        }

        public string Get()
        {
            return _traceIdentifier;
        }
    }
}
