using System.Net.Http;

namespace LoopiaDDNSApp.InfraStructure.Managers.Interfaces
{
    public interface IHttpClientManager
    {
        HttpClient GetHttpClient(string httpClientName);
    }
}