using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoopiaDDNSApp.Repository.Ipify.Interfaces;

namespace LoopiaDDNSApp.Repository.Ipify
{
    public class IpifyRepository : IIpifyRepository
    {
        public async Task<string> GetExternalIp()
        {
            Console.WriteLine("Contacting IPify to check our external IP address");
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://api.ipify.org");
        }
    }
}