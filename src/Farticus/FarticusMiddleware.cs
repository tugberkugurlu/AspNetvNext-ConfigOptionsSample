using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;

namespace Farticus
{
    public class FarticusMiddleware
    {
        // This shouldn't be static as this middleware is constructed per pipeline lifetime.
        private readonly ILogger _logger;
        private readonly IFarticusRepository _repository;
        private readonly FarticusOptions _options;

        // Get ILoggerFactory injected into this middleware as it was put inside the DI system
        // by Microsoft.AspNet.Hosting at early points during the bootstrapping process.
        public FarticusMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory,
            IFarticusRepository repository,
            IOptions<FarticusOptions> options)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }

            if(options == null)
            {
                throw new ArgumentNullException("options");
            }

            if(repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _logger = loggerFactory.Create(typeof(FarticusMiddleware).FullName);
            _repository = repository;
            _options = options.Options;
        }

        public async Task Invoke(HttpContext context)
        {
            var builder = new StringBuilder();
            builder.Append("<div><strong>Farting...</strong></div>");
            for(int i = 0; i < _options.NumberOfMessages; i++)
            {
                string message = await _repository.GetFartMessageAsync();
                builder.AppendFormat("<div>{0}</div>", message);
            }

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(builder.ToString());
        }
    }
}