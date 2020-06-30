using AutoMapper;
using DataAccess;
using DataAccess.Configuration;
using Logic.Configuration.MappingProfiles;
using Logic.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using SushiHouseParser;
using System;
using Logic.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using DataAccess;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Infrastructure.Internal;
using Microsoft.Extensions.Logging;
using Logic.Services;

namespace Logic.Infrastructure
{
    public static class LogicConfiguration
    {
        /// <summary>
        /// Provide logic layer services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns></returns>
        public static IServiceCollection AddLogicServices(this IServiceCollection services, IConfiguration config)
        => services.ConfigurateSettings(config)
            .AddDataAccessServices()            
            .AddAutoMapper(MappingAssembly.GetMappingProfileTypes())
            .AddScoped<IParser, Parser>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddMailKit(optionBuilder => 
            {
                var options = services.BuildServiceProvider().GetRequiredService<IOptions<MailKitOptions>>().Value;
                optionBuilder.UseMailKit(options);
            });

        /// <summary>
        /// Configurate IOptions service from IConfiguration sections of appsetting.json
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="config">IConfiguraion</param>
        /// <returns></returns>
        private static IServiceCollection ConfigurateSettings(this IServiceCollection services, IConfiguration config)
        => services.Configure<DatabaseSettings>(config.GetSection("DatabaseOptions"))
            .Configure<MailKitOptions>(config.GetSection("SmtpConnection"))
            .Configure<OrderSettings>(config.GetSection("OrderSettings"));
    }
}