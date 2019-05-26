using BlazorI18n.Core.Models;
using BlazorI18n.Core.Services;
using BlazorI18n.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorI18n.Json
{
    public static class BlazorExtensions
    {

        /// <summary>
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18JsonConfiguration"/>
        /// </summary>
        public static IServiceCollection AddJsonI18n(this IServiceCollection services, BlazorI18JsonConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(configuration.DefaultLocal) || string.IsNullOrEmpty(configuration.DefaultLocal))
            {
                throw new ArgumentException($"Default local must be provide.");
            }

            if (configuration.LocalsUri == null || configuration.LocalsUri.Count == 0)
            {
                throw new ArgumentException($"Locals Uri can't be null and need 1 element.");
            }

            if (!configuration.LocalsUri.ContainsKey(configuration.DefaultLocal))
            {
                throw new ArgumentException($"Default local not found in provide uri.");
            }

            configuration.CurrentLocal = configuration.DefaultLocal;

            I18n.Configure(configuration);
            services.AddSingleton<II18n, I18n>();

            return services;
        }

        /// <summary>
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18JsonConfiguration"/>
        /// </summary>
        public static IServiceCollection AddJsonI18n(this IServiceCollection services, Action<BlazorI18JsonConfiguration> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            BlazorI18JsonConfiguration options = new BlazorI18JsonConfiguration();
            configure(options);

            return AddJsonI18n(services, options);
        }
    }
}
