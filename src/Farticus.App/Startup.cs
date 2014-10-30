using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.DependencyInjection;
using Farticus;

namespace Farticus.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseServices(services =>
            {
                services.AddScoped<IFarticusRepository, InMemoryFarticusRepository>();
                services.AddTransient<IConfigureOptions<FarticusOptions>, FarticusOptionsSetup>();
            });

            app.UseMiddleware<FarticusMiddleware>();
        }
    }
}