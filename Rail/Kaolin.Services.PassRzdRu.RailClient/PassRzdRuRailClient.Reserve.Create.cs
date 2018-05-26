using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Parser;
    using Kaolin.Models.Rail;
    using Kaolin.Infrastructure.SessionStore;

    public partial class PassRzdRuRailClient
    {
        public async Task<QueryReserveCreate.Result> CreateReserveAsync(ISessionStore session, QueryReserveCreate.Request request)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var login = session.Retrieve<Session>("login");

            var trains = session.Retrieve<Internal.TrainOptions>("train_options");
            var train = trains.Options.First(x => x.OptionRef == request.Option.TrainOptionRef);

            var cars = session.Retrieve<Internal.CarOptions>("car_options");
            var car = cars.Options.First(x => x.OptionRef == request.Option.CarOptionRef);

            var requestData = new Parser.Structs.Layer5705.Request
            {
                Passengers = request.Passengers.Select(p => _passengerConverter.ToLayer5705(p, train.Depart.DateAndTime, cars.AgeLimits)).ToArray(),
                Orders = new Parser.Structs.Layer5705.RequestOrder[]
                {
                    new Parser.Structs.Layer5705.RequestOrder
                    {
                        Range0 = request.Option.Range.From,
                        Range1 = request.Option.Range.To,
                        PlUpdown = $"{request.Option.Range.LowerCount.ToString()}{request.Option.Range.UpperCount.ToString()}",
                        PlBedding = request.Option.Bedding,
                        PlComp = request.Option.Location,
                        Dir = 1,
                        Code0 = Int32.Parse(train.Depart.Station.Code),
                        Code1 = Int32.Parse(train.Arrive.Station.Code),
                        Route0 = train.RouteStart.Station.Name,
                        Route1 = train.RouteEndStation,
                        Number = train.Number,
                        Number2 = train.DisplayNumber,
                        Brand = train.Brand,
                        Letter = Char.IsLetter(train.DisplayNumber.Last()) ? train.DisplayNumber.Last().ToString() : "",
                        Ctype = car.Type.Id,
                        Cnumber = car.Number,
                        ClsType = car.ServiceClass,
                        ElReg = car.HasElectronicRegistration,
                        Ferry = false,
                        SeatType = null,
                        TicketPriceInPoints = 0,
                        TrainType = train.Type,
                        ConferenceRoomFlag = false,
                        CarrierGroupId = 1,
                        Datetime0 = train.Depart.GetDateTimeString(),
                        Teema = 0,
                        CarVipFlag = 0
                    }
                }
            };

            var result = await _parser.ReserveTicketAsync(login, requestData);

            session.Store("reserve", result);

            var price = _priceConverter.ToDecimal(result.TotalSum);
            var fixedExtraCharge = new FixedFeeType(200).ToCharge(price);
            var priceWithCharges = new Price(price, new Charges { Extra = new List<Charge> { fixedExtraCharge } });

            session.Store("price", priceWithCharges);

            return new QueryReserveCreate.Result
            {
                SaleOrderId = result.SaleOrderId,
                Price = priceWithCharges,
                Orders = from o in result.Orders
                         let durationParts = o.TimeInWay.Split(':').Select(x => int.Parse(x))
                         select new QueryReserveCreate.Result.ResultOrder
                         {
                             OrderId = o.OrderId,
                             Cost = o.Cost,
                             TotalCostPt = o.TotalCostPt,
                             Created = o.Created,
                             SeatNums = o.SeatNums,
                             DirName = o.DirName,
                             Agent = o.Agent,
                             //DeferredPayment = o.DeferredPayment,
                             Train = new TrainInfo
                             {
                                 Number = o.Number,
                                 DisplayNumber = o.Number2,
                                 Depart = new TripEvent(o.Date0, o.Time0, o.Msk0 ? TimeType.MOSCOW : TimeType.LOCAL, o.Station0, o.Code0),
                                 Arrive = new TripEvent(o.Date1, o.Time1, o.Msk1 ? TimeType.MOSCOW : TimeType.LOCAL, o.Station1, o.Code1),
                                 RouteStart = new TripEvent(o.TrDate0, o.TrTime0, o.Msk0 ? TimeType.MOSCOW : TimeType.LOCAL, o.Route0, null),
                                 ArriveLocal = new TripEvent(o.LocalDate1, o.LocalTime1, TimeType.LOCAL, o.Station1, o.Code1),
                                 RouteEndStation = o.Route1,
                                 TimezoneDifference = o.TimeDeltaString1,
                                 TripDuration = new TimeSpan(durationParts.ElementAt(0), durationParts.ElementAt(1), 00),
                                 //TripDistance = train.TripDistance,
                                 Carrier = o.Carrier,
                                 Brand = train.Brand,
                                 IsFirm = train.IsFirm,
                                 HasElectronicRegistration = train.HasElectronicRegistration,
                                 HasDynamicPricing = train.HasDynamicPricing
                             },
                             Car = new QueryReserveCreate.Result.Car
                             {
                                 Number = o.CNumber,
                                 Type = _carTypeConverter.ByCTypeI(o.Ctype), // WARNING: it's cTypeI here, not cType
                                 ServiceClass = o.ClsType,
                                 AdditionalInfo = o.TimeInfo
                             },
                             Tickets = o.Tickets.Select(t => new QueryReserveCreate.Result.Ticket
                             {
                                 TicketId = t.TicketId,
                                 Cost = t.Cost,
                                 Seats = t.Seats,
                                 SeatsType = t.SeatsType,
                                 Tariff = new QueryReserveCreate.Result.Tariff(t.Tariff, t.TariffName),
                                 Teema = t.Teema,
                                 Passengers = t.Pass.Select(p => _passengerConverter.ToPassenger(Array.IndexOf(t.Pass, p) + 1, p))
                             })
                         },
                PaymentSystems = result.PaymentSystems.Select(p => new QueryReserveCreate.Result.PaymentSystem
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Tip = p.Tip
                })
            };
        }
    }
}
