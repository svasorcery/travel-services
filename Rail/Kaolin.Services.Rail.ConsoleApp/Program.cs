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
                var session = ssm.Create(TimeSpan.FromMinutes(15));

                var trainsResult = await client.QueryTrainsAsync(session, new QueryTrains.Request { From = "2000000", To = "2030000", DepartDate = DateTime.Now.AddDays(30) });
                var trains = trainsResult.Trains.ToArray();
                await ssm.SaveAsync(session);

                //var session = await ssm.LoadAsync("98336db4-3ee5-4af7-84a8-69906aa16b86");

                var carsResult = await client.QueryCarsAsync(session, new QueryCars.Request { OptionRef = 1 });
                var cars = carsResult.Cars.ToArray();
                await ssm.SaveAsync(session);

                var optionRef = session.Retrieve<PassRzdRu.RailClient.Internal.CarOptions>("car_options").Options.First().OptionRef;
                var car = await client.QueryCarAsync(session, new QueryCar.Request { TrainRef = 1, OptionRef = optionRef });

                var reserveResult = await client.CreateReserveAsync(session, GetReserveRequest(session));
                var totalCost = reserveResult.Price.Total;
                await ssm.SaveAsync(session);

                var cancelResult = await client.CancelReserveAsync(session);
                var status = cancelResult.Status;
                await ssm.SaveAsync(session);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        private static QueryReserveCreate.Request GetReserveRequest(Infrastructure.SessionStore.ISessionStore session)
        {
            var person = new Person(
                Gender.MALE, 
                "Иван", "Иванович", "Иванов", 
                new DateTime(1985, 7, 31), 
                new Passport(
                    PassportType.RussianPassport,
                    "1511", "151115", 
                    new Country { RzdId = 114 }, 
                    new DateTime(2020, 7, 31)
                )
            );

            return new QueryReserveCreate.Request
            {
                Option = new QueryReserveCreate.Request.OptionParams
                {
                    SessionId = session.Id,
                    TrainOptionRef = session.Retrieve<PassRzdRu.RailClient.Internal.TrainOptions>("train_options").Options.First().OptionRef,
                    CarOptionRef = session.Retrieve<PassRzdRu.RailClient.Internal.CarOptions>("car_options").Options.First().OptionRef,
                    Range = new QueryReserveCreate.Request.PlacesRange(1, 30, top: 1),
                    Bedding = true
                },
                Passengers = new PassengerRequest[]
                {
                    new PassengerRequest(1, person
                        //insurance: 10, 
                        //policy: session.Retrieve<PassRzdRu.RailClient.Internal.TrainOptions>("train_options").Request.DepartDate.AddDays(5).ToString("dd.MM.yyyy")
                    )
                }
            };
        }

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddOptions()
                //.AddInMemorySessionProvider()
                .AddMongoDbSessionProvider()
                .Configure<Infrastructure.SessionStore.Config>(config =>
                {
                    config.ConnectionString = "mongodb://localhost:27017";
                    config.Database = "ssp";
                    config.Collection = "sessions";
                })
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
