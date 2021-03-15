using System;
using System.Linq;
using System.Threading.Tasks;
using Horizon.XmlRpc.Client;
using LoopiaDDNSApp.Domain.Services.Interfaces;
using LoopiaDDNSApp.Models;
using LoopiaDDNSApp.Repository.Ipify.Interfaces;
using Microsoft.Extensions.Options;

namespace LoopiaDDNSApp.Domain.Services
{
    public class DNSService : IDNSService
    {
        private readonly IIpifyRepository _ipRepo;
        private readonly Config _config;

        public DNSService(IIpifyRepository ipRepo, IOptions<Config> options)
        {
            _config = options.Value;
            _ipRepo = ipRepo;
        }

        public async Task<bool> UpdateDNS()
        {

            // Get external IP
            var ip = await _ipRepo.GetExternalIp();

            // Grab settings from config.json
            var username = _config.Username;
            var password = _config.Password;
            var domain = _config.Domain;
            var subdomain = _config.Subdomain;
            var ttl = _config.ttl;

            // Build XML RPC Client
            var proxy = XmlRpcProxyGen.Create<ILoopiaServiceProxy>();
            proxy.Url = _config.LoopiaAPIUri;
            var record = new RecordDto();

            // Contact Loopia, grab a list of all your domainRecords
            try
            {
                var result = proxy.GetZoneRecords(username, password, domain, subdomain); 

                // We select the first one, which ought to be your IP. TODO: Make actual logic.
                record = result.First();

               if (ip == record.rdata)
                {
                    return true;
                }

                // Create domain record, new IP, append ID.
                var domainRecord = new RecordDto
                {
                    priority = _config.Priority,
                    rdata = ip,
                    record_id = record.record_id,
                    ttl = _config.ttl,
                    type = _config.Type
                };

                // Update
                var status = proxy.UpdateZoneRecord(username, password, domain, subdomain, domainRecord);

                // act on status
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("usually this means incorrect API username");
            }

            return true;
        }
    }
}
