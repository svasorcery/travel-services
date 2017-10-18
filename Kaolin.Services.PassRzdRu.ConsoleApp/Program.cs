using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kaolin.Services.PassRzdRu.ConsoleApp
{
    class Program
    {
        static async Task Run(Parser.PassRzdRuClient client)
        {
            try
            {
                var loginResult = await client.CreateSession("your_login_here", "your_password_here");

                var trains = await client.GetTrainsAsync(loginResult, new Parser.Structs.Layer5827.Request("2000000", "2030000", DateTime.Now.AddDays(14), null));

                var cars = await client.GetCarsAsync(loginResult, new Parser.Structs.Layer5764.Request(0, trains.Tp[0].FromCode, trains.Tp[0].WhereCode, trains.Tp[0].List[0].Number, trains.Tp[0].Date, trains.Tp[0].List[0].BEntire));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddOptions()
                .Configure<Parser.Config>(_ => _.Polling = new Parser.Config.PollingConfig(60, 1000))
                .AddSingleton<Parser.PassRzdRuClient>()
                .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            Run(serviceProvider.GetService<Parser.PassRzdRuClient>()).Wait();
        }
    }
}
