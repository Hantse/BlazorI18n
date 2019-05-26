using BlazorI18n.Core.Helpers;
using BlazorI18n.Core.Models;
using BlazorI18n.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorI18n.Services
{
    public class I18n : II18n
    {
        private static BlazorI18JsonConfiguration _configuration;

        private Dictionary<string, Dictionary<string, string>> _translations = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, string> _tmpTranslations = new Dictionary<string, string>();
        private HttpClient _httpClient;

        /// <summary>
        /// Event trig when UI must be update when local change
        /// </summary>
        public event Action OnLocalUpdateOrChange;

        /// <summary>
        /// Use to avoid multiple access to fetch 
        /// </summary>
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public I18n(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Try to fetch value from configurer Uris
        /// </summary>
        private async Task FetchLocal()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (!_translations.ContainsKey(_configuration.CurrentLocal) ||
                    _configuration.ForceReloadLocal)
                {
                    string remoteValues = await _httpClient.GetStringAsync(_configuration.LocalsUri[_configuration.CurrentLocal]);

                    Dictionary<string, string> currentLocalValues = JsonHelper.Flatten(remoteValues);


                    if (_translations.ContainsKey(_configuration.CurrentLocal))
                    {
                        _translations[_configuration.CurrentLocal] = currentLocalValues;
                    }
                    else
                    {
                        _translations.Add(_configuration.CurrentLocal, currentLocalValues);
                    }
                }

                _translations.TryGetValue(_configuration.CurrentLocal, out _tmpTranslations);

                OnLocalUpdateOrChange?.Invoke();
            }
            catch (HttpRequestException)
            {
                Debug.WriteLine("Unable to get values.");
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Change current local, fetch value and update UI
        /// </summary>
        /// <param name="local">Local must be exist in LocalsUri or available over OnBaseUri</param>
        public async Task ChangeLocal(string local)
        {
            _configuration.CurrentLocal = local;
            await FetchLocal();
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
            if (!_translations.Any()
                    && semaphoreSlim.CurrentCount == 1)
            {
                await FetchLocal();
            }

            _tmpTranslations.TryGetValue(key, out string value);
            return value?.ToString() ?? $"Key not found {key}:{_configuration.CurrentLocal}";
        }

        public bool IsCurrentLocal(string local)
        {
            return _configuration.CurrentLocal.Equals(local);
        }

        #region Configure Static 
        public static void Configure(BlazorI18JsonConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
    }
}
