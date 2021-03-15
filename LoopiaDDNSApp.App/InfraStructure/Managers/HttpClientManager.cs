using System.Net.Http;
using LoopiaDDNSApp.InfraStructure.Managers.Interfaces;
using Microsoft.Extensions.Options;

namespace LoopiaDDNSApp.InfraStructure.Managers
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly Config _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientManager(IHttpClientFactory httpClientFactory, IOptions<Config> options)
        {
            _httpClientFactory = httpClientFactory;
            _config = options.Value;
        }

        public HttpClient GetHttpClient(string httpClientName)
        {
            var httpClient = _httpClientFactory.CreateClient(httpClientName);
            httpClient.DefaultRequestHeaders.Clear();

            return httpClient;
        }
    }
}