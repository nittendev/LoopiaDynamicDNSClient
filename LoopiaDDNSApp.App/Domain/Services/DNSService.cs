using System;
using System.Linq;
using System.Threading.Tasks;
using Horizon.XmlRpc.Client;
using LoopiaDDNSApp.Domain.Services.Interfaces;
using LoopiaDDNSApp.Repository.Interfaces;
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

                // Check if IP has changed compared to remote. If it has, early exit.
               /* if (ip == record.rdata)
                {
                    return true;
                }*/

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
            }

            return true;
        }
    }
}

/*
 * def update_record(Config, ip, record):
    """Update current A record"""

    # Does the record need updating?
    if record['rdata'] != new_ip:
        # Yes it does. Update it!
        new_record = {
            'priority': record['priority'],
            'record_id': record['record_id'],
            'rdata': new_ip,
            'type': record['type'],
            'ttl': record['ttl']
        }

        try:
            status = client.updateZoneRecord(
                    Config.username,
                    Config.password,
                    Config.domain,
                    Config.subdomain,
                    new_record)

            if Config.subdomain == '@':
                print('{domain}: {status}'.format(
                    domain=Config.domain,
                    status=status))
            else:
                print('{subdomain}.{domain}: {status}'.format(
                    subdomain=Config.subdomain,
                    domain=Config.domain,
                    status=status))

        except:
            api_error()

    else:
        # Record does not need updating
        if Config.subdomain == '@':
            print('{domain}: No change'.format(domain=Config.domain))
        else:
            print('{subdomain}.{domain}: No change'.format(
                subdomain=Config.subdomain,
                domain=Config.domain))


############
### Main ###
############

if __name__ == '__main__':
    # Build XML RPC client
    client = xmlrpc.client.ServerProxy(
        uri = 'https://api.loopia.se/RPCSERV',
        encoding = 'utf-8')

    # Get current A records and public IP address
    a_records = get_records(Config)
    new_ip = get_ip()

    # Do we currently have an A record? If not, create one!
    if len(a_records) == 0:
        add_record(Config, new_ip)

    else:
        # Remove all excess A records
        if len(a_records) > 1:
            del_excess(Config, a_records[1:])

        # Now let's update the record!
        update_record(Config, new_ip, a_records [0])*/