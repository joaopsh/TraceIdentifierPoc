using System.Threading;

namespace TraceIdentifierPoc.Logger
{
    public static class LoggerContext
    {
        private static readonly AsyncLocal<string> TraceIdentifier = new AsyncLocal<string>();

        public static void AddTraceIdentifier(string traceIdentifier)
        {
            TraceIdentifier.Value = traceIdentifier;
        }

        public static string GetTraceIdentifier()
        {
            return TraceIdentifier.Value;
        }
    }
}
