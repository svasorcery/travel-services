using System;
using System.Linq;
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
                var request = new Parser.Structs.Layer5827.Request("2000000", "2030000", DateTime.Now.AddDays(14));

                var trains = await client.GetTrainsAsync(loginResult, request);
                var train = trains.Tp[0]?.List[0];

                var cars = await client.GetCarsAsync(loginResult, new Parser.Structs.Layer5764.Request(0, request.From, request.To, request.DepartDate.ToString("dd.MM.yyyy"), train.Number, train.BEntire));
                var car = cars.Lst[0]?.Cars[0];
                
                var reserve = await client.ReserveTicketAsync(loginResult, GetReserveRequest(request, train, car));

                var cancel = await client.CancelReserveAsync(loginResult, new Parser.Structs.Layer5769.Request { OrderId = reserve.SaleOrderId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static Parser.Structs.Layer5705.Request GetReserveRequest(Parser.Structs.Layer5827.Request request, Parser.Structs.Layer5827.TpItem.TpTrain train, Parser.Structs.Layer5764.LstItem.Car car)
        {
            if (request == null || train == null || car == null)
            {
                throw new ArgumentNullException();
            }

            return new Parser.Structs.Layer5705.Request
            {
                Orders = new Parser.Structs.Layer5705.RequestOrder[]
                {
                    new Parser.Structs.Layer5705.RequestOrder
                    {
                        Range0 = 1,
                        Range1 = 30,
                        PlBedding = true,
                        PlUpdown = "01",
                        Dir = 1,
                        Code0 = int.Parse(request.From),
                        Code1 = int.Parse(request.To),
                        Route0 = train.Route0,
                        Route1 = train.Route1,
                        Number = train.Number,
                        Number2 = train.Number2,
                        Brand = train.Brand,
                        Letter = Char.IsLetter(train.Number2.Last()) ? train.Number2.Last().ToString() : "",
                        Ctype = car.CType,
                        Cnumber = car.CNumber,
                        ClsType = car.ClsType,
                        ElReg = car.ElReg,
                        Ferry = false,
                        SeatType = null,
                        TicketPriceInPoints = 0,
                        TrainType = "",
                        ConferenceRoomFlag = false,
                        CarrierGroupId = 1,
                        Datetime0 = $"{train.Date0} {train.Time0}",
                        Teema = 0,
                        CarVipFlag = 0
                    }
                },
                Passengers = new Parser.Structs.Layer5705.RequestPassenger[]
                {
                    new Parser.Structs.Layer5705.RequestPassenger
                    {
                        Id = 0,
                        FirstName = "Иван",
                        MidName = "Иванович",
                        LastName = "Иванов",
                        Birthdate = "31.07.1985",
                        Gender = 2, // MALE
                        DocType = 1, // PS
                        DocNumber = "1511151115",
                        Country = 114, // RU
                        Tariff = "Adult"
                    }
                }
            };
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
