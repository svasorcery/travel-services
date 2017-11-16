using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Kaolin.Services.PassRzdRu.ConsoleApp
{
    using Kaolin.Services.PassRzdRu.Parser.Structs;

    class Program
    {
        static async Task Run(Parser.PassRzdRuClient client)
        {
            try
            {
                var loginResult = await client.CreateSession("your_login_here", "your_password_here");
                var request = new Layer5827.Request("2000000", "2030000", DateTime.Now.AddDays(14));

                var trains = await client.GetTrainsAsync(loginResult, request);
                var train = trains.Tp[0]?.List[0];

                var cars = await client.GetCarsAsync(loginResult, new Layer5764.Request(0, request.From, request.To, request.DepartDate.ToString("dd.MM.yyyy"), train.Number, train.BEntire));
                var car = cars.Lst[0]?.Cars[0];
                
                var reserve = await client.ReserveTicketAsync(loginResult, GetReserveRequest(request, train, car));

                var docs = await client.GetDocumentTypesAsync(); // "ru" / "en"

                var cancel = await client.CancelReserveAsync(loginResult, new Layer5769.Request { OrderId = reserve.SaleOrderId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static Layer5705.Request GetReserveRequest(Layer5827.Request request, Layer5827.TpItem.TpTrain train, Layer5764.LstItem.Car car)
        {
            if (request == null || train == null || car == null)
            {
                throw new ArgumentNullException();
            }

            return new Layer5705.Request
            {
                Orders = new Layer5705.RequestOrder[]
                {
                    new Layer5705.RequestOrder
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
                Passengers = new Layer5705.RequestPassenger[]
                {
                    new Layer5705.RequestPassenger
                    {
                        Id = 0,
                        FirstName = "Иван",
                        MidName = "Иванович",
                        LastName = "Иванов",
                        Birthdate = new DateTime(1985, 7, 31).ToString("dd.MM.yyyy"),
                        Gender = Layer5705.Gender.MALE,
                        DocType = Layer5705.DocumentTypes.PN,
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
