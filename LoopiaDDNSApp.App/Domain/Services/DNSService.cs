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
        private readonly Config _config;
        private readonly IIpifyRepository _ipRepo;

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
            var subDomain = _config.Subdomain;
            var ttl = _config.ttl;

            // Build XML RPC Client
            var proxy = XmlRpcProxyGen.Create<ILoopiaServiceProxy>();
            proxy.Url = _config.LoopiaAPIUri;

            // Contact LoopiaAPI, grab a list of all your domainRecords for this specific domain.
            try
            {
                var result = proxy.GetZoneRecords(username, password, domain, subDomain);

                // We select the first one, which ought to be your IP. TODO: Make actual logic?
                var record = result.First();

                if (ip == record.rdata)
                {
                    var currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    Console.WriteLine(currentTime + "\nNo need to update\n Your current external ip: " + ip + "\n Your current Loopia DNS IP: " + record.rdata);
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
                var status = proxy.UpdateZoneRecord(username, password, domain, subDomain, domainRecord);
                var timeNow = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                // act on status
                switch (status)
                {
                    case "OK":
                        Console.WriteLine("Updated" + timeNow);
                    break;
                    case "AUTH_ERROR":
                        Console.WriteLine(timeNow + "\nAUTH_ERROR, Wrong username or password");
                    break;
                    case "BAD_INDATA":
                        Console.WriteLine(timeNow + "\nBAD_INDATA, Incorrect configuration in config.json");
                    break;
                    default:
                        Console.WriteLine(timeNow + "\nUnknown status reply from Loopia...");
                    break;
                }
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