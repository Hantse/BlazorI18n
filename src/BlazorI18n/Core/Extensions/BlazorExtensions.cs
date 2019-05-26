using BlazorI18n.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using BlazorI18n.Services;
using Microsoft.AspNetCore.Components.Builder;
using System.Threading.Tasks;

namespace BlazorI18n
{
    public static class BlazorExtensions
    {

        /// <summary>
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18Configuration"/>
        /// </summary>
        public static IServiceCollection AddI18n(this IServiceCollection services, BlazorI18Configuration configuration)
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
        /// Adds a singleton <see cref="II18n"/> instance to the DI <see cref="IServiceCollection"/> with the specified <see cref="BlazorI18Configuration"/>
        /// </summary>
        public static IServiceCollection AddI18n(this IServiceCollection services, Action<BlazorI18Configuration> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            BlazorI18Configuration options = new BlazorI18Configuration();
            configure(options);

            return AddI18n(services, options);
        }
    }
}
