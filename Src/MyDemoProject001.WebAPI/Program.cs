using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyDemoProject001
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Zubair
        // Clear providers and Add Console provider
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging((context,logging) =>
             {
                 logging.ClearProviders(); // Clear default providers              
                 logging.AddConfiguration(context.Configuration.GetSection("Logging")); //Logging section of appsettings              
                 logging.AddConsole(); // Add console provider
                 //logging.AddEventSourceLogger();
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
