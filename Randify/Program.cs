using Blazor.Extensions.Storage;
using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Randify.Services;
using System;

namespace Randify
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<AuthenticationService>();
                services.AddSingleton<ConfigurationService>();
                services.AddSingleton<SpotifyService>();
                services.AddStorage();
                services.AddLogging(builder => builder
                    .AddBrowserConsole()
                    .SetMinimumLevel(LogLevel.Trace));
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
