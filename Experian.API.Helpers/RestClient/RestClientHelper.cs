using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Experian.API.Helpers.RestClient
{
    [ExcludeFromCodeCoverage]
    public class RestClientHelper : IRestClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RestClientHelper(IHttpClientFactory factory)
        {
            this._httpClientFactory = factory;
        }

        public async Task<T> GetAsync<T>(string requestUri) where T : class
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(requestUri);
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResult);
        }
    }
}
