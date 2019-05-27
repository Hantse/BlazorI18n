using BlazorI18n.Core.Services;
using BlazorI18n.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorI18n.Extensions
{
    public static class BlazorExtensions
    {
        public static IServiceCollection AddI18n(this IServiceCollection services, BlazorI18nConfiguration configuration)
        {
            return AddI18n<BlazorI18nConfiguration>(services, configuration);
        }

        public static IServiceCollection AddI18n<T>(this IServiceCollection services, T configuration)
            where T : BlazorI18nConfiguration
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrWhiteSpace(configuration.DefaultLocal)
                || string.IsNullOrEmpty(configuration.DefaultLocal))
            {
                throw new ArgumentException($"Default local must be provide.");
            }

            configuration.CurrentLocal = configuration.DefaultLocal;
            return AddI18n(services, configuration);
        }

        /// <summary>
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18JsonConfiguration"/>
        /// </summary>
        public static IServiceCollection AddI18n(this IServiceCollection services, Action<BlazorI18nConfiguration> configure)
        {
            return AddI18n<BlazorI18nConfiguration>(services, configure);
        }

        /// <summary>
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18JsonConfiguration"/>
        /// </summary>
        public static IServiceCollection AddI18n<T>(this IServiceCollection services, Action<T> configure)
            where T : BlazorI18nConfiguration
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            services.Configure(configure);
            services.AddSingleton<II18n, I18n>();

            return services;
        }
    }
}
