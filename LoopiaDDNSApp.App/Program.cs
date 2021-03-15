using System;
using LoopiaDDNSApp.Domain.Services;
using LoopiaDDNSApp.Domain.Services.Interfaces;
using LoopiaDDNSApp.InfraStructure.Managers;
using LoopiaDDNSApp.InfraStructure.Managers.Interfaces;
using LoopiaDDNSApp.Quartz;
using LoopiaDDNSApp.Repository.Ipify;
using LoopiaDDNSApp.Repository.Ipify.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace LoopiaDDNSApp
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            return 0;
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                try
                { 
                   var configuration = new ConfigurationBuilder().SetBasePath(hostContext.HostingEnvironment.ContentRootPath).AddJsonFile("config.json", false, true).AddEnvironmentVariables().Build();
                   services.AddOptions();
                   services.Configure<Config>(configuration.GetSection("Settings"));

                   services.AddScoped<IHttpClientManager, HttpClientManager>();
                   services.AddScoped<IIpifyRepository, IpifyRepository>();
                   services.AddScoped<IDNSService, DNSService>();
                   services.AddSingleton<IJobFactory, JobFactory>();
                   services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                   services.AddSingleton<QuartzJobRunner>();
                   services.AddScoped<UpdateDnsJob>();
                   services.AddSingleton(new JobSchedule(typeof(UpdateDnsJob), configuration.GetValue<string>("Settings:Schedule")));

                   services.AddHostedService<QuartzHostedService>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Is there a config.json available?");
                    throw;
                }

            }).UseWindowsService();
        }
    }
}