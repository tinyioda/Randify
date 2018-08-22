using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using Randify.App.Services;

namespace Randify.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<ConfigurationService>();
            services.AddSingleton<SpotifyService>();
            services.AddStorage();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
