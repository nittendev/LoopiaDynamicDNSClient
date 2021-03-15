using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LoopiaDDNSApp.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace LoopiaDDNSApp.Repository.Ipify
{
    public class IpifyRepository : IIpifyRepository
    {
        private readonly Config _config;

        public IpifyRepository(IOptions<Config> options)
        {
            _config = options.Value;
        }

        public async Task<string> GetExternalIp()
        {
            Console.WriteLine("Contacting IPify to check our external IP address");
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://api.ipify.org");
        }
    }
}