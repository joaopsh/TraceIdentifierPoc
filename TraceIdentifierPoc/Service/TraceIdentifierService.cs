using System;

namespace TraceIdentifierPoc.Service
{
    public class TraceIdentifierService : ITraceIdentifierService
    {
        private readonly string _traceIdentifier;

        public TraceIdentifierService()
        {
            _traceIdentifier = Guid.NewGuid().ToString();
        }

        public string Get()
        {
            return _traceIdentifier;
        }
    }
}
