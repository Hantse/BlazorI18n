using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorI18n.Samples
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddI18n((config) =>
            {
                config.DefaultLocal = "en";
                config.LocalsUri = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "en", "https://livetranslatestorage.blob.core.windows.net/cdn/en.json" },
                    { "fr", "https://livetranslatestorage.blob.core.windows.net/cdn/fr.json" }
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
