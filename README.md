# BlazorI18n
Blazor library for I18n translations

## How to use 
Add to startup in ConfigureServices(IServiceCollection services)
```csharp

            services.AddI18n((config) =>
            {
                config.DefaultLocal = "en";
                config.LocalsUri = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "en", "i18n/en.json" },
                    { "fr", "i18n/fr.json" }
                };
                config.CurrentLocal = "en";
            });

```