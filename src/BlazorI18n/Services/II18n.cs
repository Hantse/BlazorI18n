using BlazorI18n.Models;
using BlazorI18n.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorI18n.Core.Services
{
    public interface II18n
    {
        /// <summary>
        /// Get a value from key base on current local set in I18nService
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// Value if found or key passed in parameter
        /// </returns>
        Task<string> GetValue(string key);


        /// <summary>
        /// Change current local, fetch value and update UI
        /// </summary>
        /// <param name="local">Local must be exist in LocalsUri or available over OnBaseUri</param>
        Task ChangeLocal(string local);

        /// <summary>
        /// Event trig when UI must be update when local change
        /// </summary>
        event Action OnLocalUpdateOrChange;

        bool IsCurrentLocal(string local);
    }

    public class I18n : II18n
    {
        private BlazorI18nConfiguration _configuration;

        private Dictionary<string, Dictionary<string, string>> _values = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, string> _currentValues = new Dictionary<string, string>();
        private IValueProvider _valueProvider;
        private ILogger<I18n> _logger;

        /// <summary>
        /// Event trig when UI must be update when local change
        /// </summary>
        public event Action OnLocalUpdateOrChange;

        /// <summary>
        /// Use to avoid multiple access to fetch 
        /// </summary>
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public I18n(ILogger<I18n> logger, IValueProvider valueProvider, IOptions<BlazorI18nConfiguration> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger));
            _valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(IValueProvider));
            _configuration = options?.Value ?? throw new ArgumentNullException(nameof(IOptions<BlazorI18nConfiguration>));
        }

        public async Task ChangeLocal(string local)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                _configuration.CurrentLocal = local;

                if (_configuration.ForceReloadLocal || !_values.ContainsKey(local))
                {
                    _currentValues = await _valueProvider.FetchValues(local);
                    _values[local] = _currentValues;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Unable to get values.");
            }
            finally
            {
                semaphoreSlim.Release();
            }

            OnLocalUpdateOrChange.Invoke();
        }

        /// <summary>
        /// Get a value from key base on current local set in I18nService
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// Value if found or key passed in parameter
        /// </returns>
        public async Task<string> GetValue(string key)
        {
            if (!_values.Any()
                    && semaphoreSlim.CurrentCount == 1)
            {
                await ChangeLocal(_configuration.CurrentLocal);
            }

            _currentValues.TryGetValue(key, out string value);
            return value?.ToString() ?? $"Key not found {key}:{_configuration.CurrentLocal}";
        }

        public bool IsCurrentLocal(string local)
        {
            return _configuration.CurrentLocal.Equals(local);
        }
    }
}
