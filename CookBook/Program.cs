using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;
namespace CookBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = new WebHostBuilder();
#if DEBUG
            var env = host.GetSetting("environment");
#else
                var env = host.GetSetting(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine));
#endif
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .AddCommandLine(args);

            var configuration = builder.Build();
            return WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
                .UseConfiguration(configuration)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                });
        }
    }
}
