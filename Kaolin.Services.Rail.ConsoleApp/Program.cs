using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kaolin.Services.Rail.ConsoleApp
{
    using Kaolin.Models.Rail;

    class Program
    {
        static async Task Run(Models.Rail.Abstractions.IRailClient client, Infrastructure.SessionStore.ISessionProvider ssm)
        {
            try
            {
                var session = ssm.Create();

                var trainsResult = await client.SearchTrainsAsync(session, new SearchTrains.Request { From = "2000000", To = "2030000", DepartDate = DateTime.Now.AddDays(30) });
                var trains = trainsResult.Trains.ToArray();
                await ssm.SaveAsync(session);

                //var session = await ssm.LoadAsync("98336db4-3ee5-4af7-84a8-69906aa16b86");

                var carsResult = await client.GetCarsAsync(session, new GetCars.Request { OptionRef = 1 });
                var cars = carsResult.Cars.ToArray();
                await ssm.SaveAsync(session);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddOptions()
                .AddInMemorySessionProvider()
                .Configure<PassRzdRu.Parser.Config>(config =>
                {
                    config.Polling = new PassRzdRu.Parser.Config.PollingConfig(60, 1000);
                })
                .Configure<PassRzdRu.RailClient.Config>(config =>
                {
                    config.Username = "your_login_here";
                    config.Password = "your_password_here";
                })
                .AddSingleton<PassRzdRu.Parser.PassRzdRuClient>()
                .AddTransient<Models.Rail.Abstractions.IRailClient, PassRzdRu.RailClient.PassRzdRuRailClient>()
                .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            Run(serviceProvider.GetService<Models.Rail.Abstractions.IRailClient>(),
                serviceProvider.GetService<Infrastructure.SessionStore.ISessionProvider>()
            ).Wait();
        }
    }
}
