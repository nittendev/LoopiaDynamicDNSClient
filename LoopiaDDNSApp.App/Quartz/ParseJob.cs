using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopiaDDNSApp.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using Quartz;

namespace LoopiaDDNSApp.Quartz
{
    [DisallowConcurrentExecution]
    public class ParseJob : IJob
    {
        private readonly IDNSService _dnsService;

        public ParseJob(IDNSService dnsService)
        {
            _dnsService = dnsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _dnsService.UpdateDNS();
        }
    }
}