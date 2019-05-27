using BlazorI18n.Core.Helpers;
using BlazorI18n.Core.Models;
using BlazorI18n.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlazorI18n.Json.Services.Implements
{
    public class JsonValueProvider : IValueProvider
    {
        private HttpClient _httpClient;
        private BlazorI18nJsonConfiguration _configuration;

        public JsonValueProvider(HttpClient httpClient, IOptions<BlazorI18nJsonConfiguration> configuration)
        {
            _configuration = configuration?.Value ?? throw new ArgumentNullException(nameof(IOptions<BlazorI18nJsonConfiguration>));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(HttpClient));

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Dictionary<string, string>> FetchValues(string local)
        {
            string remoteValues = await _httpClient.GetStringAsync(_configuration.LocalsUri[local]);
            return JsonHelper.Flatten(remoteValues);
        }
    }
}
