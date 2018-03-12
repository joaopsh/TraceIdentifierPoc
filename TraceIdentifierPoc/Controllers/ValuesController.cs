using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

        public ValuesController(ILogger<ValuesController> logger, IHttpContextAccessor httpContextAccessor, ISomeService someService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _traceIdentifierService = ServiceLocator.ServiceProvider.GetService<ITraceIdentifierService>();
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

        [HttpPost]
        public IActionResult Post()
        {
            using (_logger.BeginScope("My name is {name} {car}", "Joao", "Vw"))
            {
                using (_logger.BeginScope("My age is {age}", "23"))
                {
                    using (_logger.BeginScope("My profession is {profession}", "Programmer"))
                    {
                        _logger.LogWarning("Warning logging inside using third level statement. Must show NAME, AGE and PROFESSION.");
                    }

                    _logger.LogWarning("Warning logging inside using second level statement. Must show NAME and AGE.");
                }

                _logger.LogWarning("Warning logging inside using first level statement. Must show NAME.");
            }

            _logger.LogWarning("Logging out of using statement.");

            return Ok();
        }
    }
}
