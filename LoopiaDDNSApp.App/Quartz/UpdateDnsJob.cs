using System;
using System.Threading.Tasks;
using LoopiaDDNSApp.Domain.Services.Interfaces;
using Quartz;

namespace LoopiaDDNSApp.Quartz
{
    [DisallowConcurrentExecution]
    public class UpdateDnsJob : IJob
    {
        private readonly IDNSService _dnsService;

        public UpdateDnsJob(IDNSService dnsService)
        {
            _dnsService = dnsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _dnsService.UpdateDNS();
        }
    }
}