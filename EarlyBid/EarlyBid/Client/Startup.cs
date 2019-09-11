using Blazor.Extensions;
using EarlyBid.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EarlyBid.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
              services.AddTransient<HubConnectionBuilder>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
