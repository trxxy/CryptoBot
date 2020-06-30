using System;
using System.Windows.Forms;
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
using UserInterface.Configuration;
using System.Threading.Tasks;

namespace UserInterface
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthForm(host.Services));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services)
                    => services.ConfigurateServices());
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            var config = GetConfiguration();

            services.AddLogging();
            services.AddViewServices();
            services.AddLogicServices(config);
            return services;
        }
        public static IConfiguration GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return configBuilder.Build();
        }
        public static ILogger ConfigurateLogger()
        {
            var logger = new LoggerFactory().CreateLogger("");
            return logger;
        }
    }
}