using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TraceIdentifierPoc.Service
{
    public class SomeService : ISomeService
    {
        private readonly ILogger<SomeService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITraceIdentifierService _traceIdentifierService;

        public SomeService(ILogger<SomeService> logger, IHttpContextAccessor httpContextAccessor, ITraceIdentifierService traceIdentifierService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _traceIdentifierService = traceIdentifierService;
        }

        public Task LongRunningAsync(int myref)
        {
            return Task.Run(() =>
            {
                MyTaskMethod(myref);
            });
        }

        private void MyTaskMethod(int myref)
        {
            var random = new Random();
            var delay = random.Next(3000, 10000);

            //_logger.LogInformation($"{_httpContextAccessor.HttpContext.TraceIdentifier}:{myref} Random delay: {delay}ms");
            _logger.LogInformation($"{_traceIdentifierService.Get()}:{myref} Random delay: {delay}ms");

            var stopWatch = Stopwatch.StartNew();

            stopWatch.Start();
            Task.Delay(delay).Wait();
            stopWatch.Stop();

            //_logger.LogInformation($"{_httpContextAccessor.HttpContext.TraceIdentifier}:{myref} Finished delay in {stopWatch.Elapsed.TotalMilliseconds}ms");
            _logger.LogInformation($"{_traceIdentifierService.Get()}:{myref} Finished delay in {stopWatch.Elapsed.TotalMilliseconds}ms");
        }
    }
}
