using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;

namespace EventStore.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Event store setting up for Event Sourcing");

            try
            {
                var config = new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                var builder = new WebHostBuilder()
                    .UseConfiguration(config)
                    .UseStartup<Startup>()
                    .UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 80);
                    });

                var host = builder.Build();
                host.Run();
            }
            catch (Exception)
            {
            }
            finally
            {
                Console.WriteLine("Event store is UP now!!");
            }
        }
    }
}
