using Microsoft.AspNet.Http;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.Framework.DependencyInjection;
using Farticus;
using System;

namespace Farticus.App
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }

            loggerFactory.AddConsole((category, logLevel) => logLevel >= LogLevel.Verbose);
            _logger = loggerFactory.Create<Startup>();
            _logger.WriteVerbose("Constructed the instance.");
        }

        public void Configure(IApplicationBuilder app)
        {
            _logger.WriteVerbose("Configure is called.");

            app.UseServices(services =>
            {
                services.AddScoped<IFarticusRepository, InMemoryFarticusRepository>();
                services.AddTransient<IConfigureOptions<FarticusOptions>, FarticusOptionsSetup>();
            });

            app.UseMiddleware<FarticusMiddleware>();
        }
    }
}