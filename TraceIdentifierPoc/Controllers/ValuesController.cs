using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TraceIdentifierPoc.Service;

namespace TraceIdentifierPoc.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITraceIdentifierService _traceIdentifierService;
        private readonly ISomeService _someService;

        public ValuesController(ILogger<ValuesController> logger, IHttpContextAccessor httpContextAccessor, ITraceIdentifierService traceIdentifierService, ISomeService someService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _traceIdentifierService = traceIdentifierService;
            _someService = someService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            int myref = new Random().Next(0, 1000);
            //_logger.LogInformation($"{_httpContextAccessor.HttpContext.TraceIdentifier}:{myref} Starting request...");
            _logger.LogInformation($"{_traceIdentifierService.Get()}:{myref} Starting request...");

            var stopWatch = Stopwatch.StartNew();

            stopWatch.Start();
            _someService.LongRunningAsync(myref);
            stopWatch.Stop();

            //_logger.LogInformation($"{_httpContextAccessor.HttpContext.TraceIdentifier}:{myref} LongRunningAsync finished in {stopWatch.Elapsed.TotalMilliseconds}ms.");
            _logger.LogInformation($"{_traceIdentifierService.Get()}:{myref} LongRunningAsync finished in {stopWatch.Elapsed.TotalMilliseconds}ms.");

            //_logger.LogInformation($"{_httpContextAccessor.HttpContext.TraceIdentifier}:{myref} Finishing request...");
            _logger.LogInformation($"{_traceIdentifierService.Get()}:{myref} Finishing request...");
            return new string[] { "value1", "value2" };
        }
    }
}
