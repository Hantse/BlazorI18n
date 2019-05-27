using Blazor.Extensions.Logging;
using BlazorI18n.Json;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorI18n.Samples
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
                .SetMinimumLevel(LogLevel.Trace)
            );

            services.AddI18nJsonProvider((config) =>
            {
                config.DefaultLocal = "en";
                config.LocalsUri = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "en", "i18n/en.json" },
                    { "fr", "i18n/fr.json" }
                };
                config.CurrentLocal = "en";
            });
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
