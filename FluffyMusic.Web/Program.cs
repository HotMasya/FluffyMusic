using FluffyMusic.Core;
using FluffyMusic.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FluffyMusic.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            //FluffyClient client = host.Services.GetRequiredService<FluffyClient>();
            //CommandHandler handler = host.Services.GetRequiredService<CommandHandler>();
            //await client.RunAsync();
            //await handler.LoadModulesAsync(typeof(BasicModule).Assembly);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
