using System;
using LoopiaDDNSApp.Domain.Services;
using LoopiaDDNSApp.Domain.Services.Interfaces;
using LoopiaDDNSApp.InfraStructure.Managers;
using LoopiaDDNSApp.InfraStructure.Managers.Interfaces;
using LoopiaDDNSApp.Quartz;
using LoopiaDDNSApp.Repository.Interfaces;
using LoopiaDDNSApp.Repository.Ipify;
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
                var configuration = new ConfigurationBuilder().SetBasePath(hostContext.HostingEnvironment.ContentRootPath).AddJsonFile("config.json", false, true).AddEnvironmentVariables().Build();

                services.AddOptions();
                services.Configure<Config>(configuration.GetSection("Settings"));
                
                services.AddScoped<IHttpClientManager, HttpClientManager>();
                services.AddScoped<IIpifyRepository, IpifyRepository>();
                services.AddScoped<IDNSService, DNSService>();
                services.AddSingleton<IJobFactory, JobFactory>();
                services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                services.AddSingleton<QuartzJobRunner>();
                services.AddScoped<ParseJob>();
                services.AddSingleton(new JobSchedule(typeof(ParseJob), configuration.GetValue<string>("Settings:Schedule")));

                services.AddHostedService<QuartzHostedService>();

            }).UseWindowsService();
        }
    }
}