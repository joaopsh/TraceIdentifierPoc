using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TraceIdentifierPoc.Logger;

namespace TraceIdentifierPoc.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IHttpContextAccessor contextAccessor)
        {
            LoggerContext.AddTraceIdentifier(contextAccessor.HttpContext.TraceIdentifier);

            await _next(httpContext);
        }
    }
}
