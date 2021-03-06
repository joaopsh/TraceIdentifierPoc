﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TraceIdentifierPoc.Logger;
using TraceIdentifierPoc.Middleware;
using TraceIdentifierPoc.Service;

namespace TraceIdentifierPoc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISomeService, SomeService>();
            
            services.AddScoped<ITraceIdentifierService>(serviceProvider =>
            {
                return new TraceIdentifierService(
                    serviceProvider,
                    (srv) => 
                    {
                        return srv.GetService<IHttpContextAccessor>()?.HttpContext?.TraceIdentifier;
                    });
            });

            services.AddMvc();

            ServiceLocator.ServiceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerProvider)
        {
            loggerProvider.AddProvider(new CustomLoggerProvider());

            app.UseMiddleware<LoggerMiddleware>();

            app.UseMvc();
        }
    }
}
