using BlazorI18n.Core.Models;
using BlazorI18n.Core.Services;
using BlazorI18n.Extensions;
using BlazorI18n.Json.Services.Implements;
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
        public static IServiceCollection AddI18nJsonProvider(this IServiceCollection services, BlazorI18nJsonConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            if (configuration.LocalsUri == null || configuration.LocalsUri.Count == 0)
            {
                throw new ArgumentException($"Locals Uri can't be null and need 1 element.");
            }

            return AddI18nJsonProvider(services, configuration);
        }

        /// <summary>
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18JsonConfiguration"/>
        /// </summary>
        public static IServiceCollection AddI18nJsonProvider(this IServiceCollection services, Action<BlazorI18nJsonConfiguration> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            services.AddI18n(configure)
                    .AddSingleton<IValueProvider, JsonValueProvider>();
            return services;
        }
    }
}
