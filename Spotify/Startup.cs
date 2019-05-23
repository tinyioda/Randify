using Blazor.Extensions.Logging;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spotify.Services;

namespace Spotify
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<ConfigurationService>();
            services.AddSingleton<SpotifyService>();
            services.AddStorage();
            services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
