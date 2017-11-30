using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Parser;
    using Kaolin.Models.Rail;
    using Kaolin.Models.Rail.Abstractions;
    using Kaolin.Infrastructure.SessionStore;

    public class PassRzdRuRailClient : IRailClient
    {
        private readonly Config _config;
        private readonly PassRzdRuClient _parser;
        private readonly Internal.Converters.PriceConverter _priceConverter;
        private readonly Internal.Converters.PassengerToLayer5705 _passengerConverter;
        private readonly Internal.Converters.CarTypeConverter _carTypeConverter;

        public PassRzdRuRailClient(IOptions<Config> optionsAccessor, PassRzdRuClient parser)
        {
            _config = optionsAccessor.Value;
            _parser = parser;
            _priceConverter = new Internal.Converters.PriceConverter();
            _passengerConverter = new Internal.Converters.PassengerToLayer5705();
            _carTypeConverter = new Internal.Converters.CarTypeConverter();
        }


        public async Task<SearchTrains.Result> SearchTrainsAsync(ISessionStore session, SearchTrains.Request request)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var credentials = _config; // TODO: credentials provider, issue ref #42

            var parserSession = await _parser.CreateSession(credentials.Username, credentials.Password);
            session.Store("login", parserSession);

            var result = await _parser.GetTrainsAsync(
                parserSession, 
                new Parser.Structs.Layer5827.Request(request.From, request.To, request.DepartDate)
                );

            int optionRef = 0;
            if (result.Tp != null && result.Tp.Length == 1)
            {
                var options = new Internal.TrainOptions
                {
                    Request = request,
                    Options = (from t in result.Tp[0].List
                               let tt = t.FlMsk == 3 || t.FlMsk == 2 ? TimeType.MOSCOW : TimeType.LOCAL
                               let durationParts = t.TimeInWay.Split(':').Select(x => int.Parse(x))
                               select new Internal.TrainOptions.Option
                               {
                                   OptionRef = ++optionRef,
                                   Number = t.Number,
                                   DisplayNumber = t.Number2,
                                   //Type = t.Type,
                                   Brand = t.Brand,
                                   BEntire = t.BEntire,
                                   IsFirm = t.BFirm,
                                   HasElectronicRegistration = t.ElReg,
                                   HasDynamicPricing = t.VarPrice,
                                   Depart = new TripEvent(t.Date0, t.Time0, tt, t.Station0, result.Tp[0].FromCode),
                                   Arrive = new TripEvent(t.Date1, t.Time1, tt, t.Station1, result.Tp[0].WhereCode),
                                   TripDuration = new TimeSpan(durationParts.ElementAt(0), durationParts.ElementAt(1), 00),
                                   RouteStart = new TripEvent(t.TrDate0, t.TrTime0, tt, t.Route0, null),
                                   RouteEndStation = t.Route1
                               })
                };

                session.Store("train_options", options);
            }

            optionRef = 0;
            return new SearchTrains.Result
            {
                Origin = result.Tp[0].From,
                OriginCode = result.Tp[0].FromCode,
                Destination = result.Tp[0].Where,
                DestinationCode = result.Tp[0].WhereCode,
                DepartureDate = DateTime.Parse(result.Tp[0].Date),
                TimeType = result.Tp[0].DefShowTime.Equals("msk") ? TimeType.MOSCOW : TimeType.GMT,
                NoFreeTickets = result.Tp[0].NoSeats,
                Trains = from t in result.Tp[0].List
                         let tt = t.FlMsk == 3 || t.FlMsk == 2 ? TimeType.MOSCOW : TimeType.LOCAL
                         let durationParts = t.TimeInWay.Split(':').Select(x => int.Parse(x))
                         select new SearchTrains.Result.Train
                         {
                             OptionRef = ++optionRef,
                             Name = t.TrainName,
                             Number = t.Number,
                             DisplayNumber = t.Number2,
                             Depart = new TripEvent(t.Date0, t.Time0, tt, t.Station0, result.Tp[0].FromCode),
                             Arrive = new TripEvent(t.Date1, t.Time1, tt, t.Station1, result.Tp[0].WhereCode),
                             ArriveLocal = new TripEvent(t.LocalDate1, t.LocalTime1, TimeType.LOCAL, t.Station1, result.Tp[0].WhereCode),
                             RouteStart = new TripEvent(t.TrDate0, t.TrTime0, tt, t.Route0, null),
                             RouteEndStation = t.Route1,
                             TimezoneDifference = t.TimeDeltaString1,
                             TripDuration = new TimeSpan(durationParts.ElementAt(0), durationParts.ElementAt(1), 00),
                             //TripDistance = null,
                             Carrier = t.Carrier,
                             Brand = t.Brand,
                             IsFirm = t.BFirm,
                             HasElectronicRegistration = t.ElReg,
                             HasDynamicPricing = t.VarPrice,
                             IsComponent = t.CarMods,
                             Cars = from c in t.Cars
                                    select new SearchTrains.Result.Car
                                    {
                                        Type = c.TypeLoc,
                                        ServiceClass = c.ServCls,
                                        FreeSeats = c.FreeSeats,
                                        MinPrice = c.Tariff,
                                        BonusPoints = c.Pt
                                    }
                         }
            };
        }

        public Task<GetTrain.Result> GetTrainAsync(ISessionStore session, GetTrain.Request request)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var trains = session.Retrieve<Internal.TrainOptions>("train_options");
            var train = trains.Options.First(x => x.OptionRef == request.OptionRef);

            return Task.FromResult(new GetTrain.Result
            {
                Train = new GetTrain.Result.TrainOption
                {
                    OptionRef = train.OptionRef,
                    DisplayNumber = train.DisplayNumber,
                    Brand = train.Brand,
                    BEntire = train.BEntire,
                    IsFirm = train.IsFirm,
                    HasElectronicRegistration = train.HasElectronicRegistration,
                    HasDynamicPricing = train.HasDynamicPricing,
                    Depart = train.Depart,
                    Arrive = train.Arrive,
                    TripDuration = train.TripDuration,
                    RouteStart = train.RouteStart,
                    RouteEndStation = train.RouteEndStation
                }
            });
        }

        public async Task<GetCars.Result> GetCarsAsync(ISessionStore session, GetCars.Request request)
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
            var selected = trains.Options.First(x => x.OptionRef == request.OptionRef);

            var result = await _parser.GetCarsAsync(login, new Parser.Structs.Layer5764.Request(
                    0, trains.Request.From, trains.Request.To, trains.Request.DepartDateString, selected.DisplayNumber, selected.BEntire
                ));
            
            var optionRef = 0;
            var carsQuery = from c in result.Lst[0].Cars
                            select new GetCars.Result.Car
                            {
                                OptionRef = ++optionRef,
                                Number = c.CNumber,
                                Type = _carTypeConverter.ByCType(c.CType),
                                ServiceClass = c.ClsType,
                                ServiceClassInternational = c.IntServiceClass,
                                Letter = c.Letter,
                                Categories = c.AddSigns,
                                SchemeId = c.SchemeId.ToString(), // TODO: Add scheme converter
                                FreePlaceNumbers = Internal.Converters.FreePlacesConverter.Convert(c.Places),
                                SpecialSeatTypes = c.SpecialSeatTypes?.Split(' '),
                                FreeSeats = c.Seats.Select(s => new GetCars.Result.SeatGroup
                                {
                                    Type = s.Type,
                                    Label = s.Label?.Replace("&nbsp;", " "),
                                    Price = _priceConverter.ToPrice(s.Tariff),
                                    Places = Internal.Converters.FreePlacesConverter.Convert(s.Places),
                                    Count = s.Free
                                }).ToArray(),
                                Services = c.Services.Select(s => new GetCars.Result.CarService
                                {
                                    Name = s.Name,
                                    Description = s.Description
                                }).ToArray(),
                                ServicesDescription = c.ClsName,
                                Price = _priceConverter.ToPriceRange(c.Tariff, c.Tariff2),
                                Carrier = c.Carrier,
                                Owner = c.Owner,
                                HasElectronicRegistration = c.ElReg,
                                HasDynamicPricing = c.VarPrice,
                                IsNoSmoking = c.NoSmok,
                                CanAddBedding = c.Bedding,
                                HasBeddingIncluded = c.ForcedBedding,
                                IsTwoStorey = c.BDeck2,
                                IsWebSalesForbidden = c.InetSaleOff
                            };

            var cars = carsQuery.ToList();

            var ageLimits = new GetCars.Result.AgeRestrictions
            {
                ChildWithPlace = result.ChildrenAge,
                InfantWithoutPlace = result.MotherAndChildAge
            };

            var options = new Internal.CarOptions
            {
                Options = cars,
                // TODO: add Schemes
                AgeLimits = ageLimits
            };

            session.Store("car_options", options);

            var insuranceProviders = result.InsuranceCompany.Select(x => new InsuranceProvider
            {
                Id = x.Id,
                FullName = x.ShortName,
                ShortName = x.ShortName,
                OfferUrl = x.OfferUrl,
                InsuranceCost = x.InsuranceCost,
                InsuranceBenefit = x.InsuranceBenefit
            });

            return new GetCars.Result
            {
                // TODO: add train info
                Cars = cars,
                AgeLimits = ageLimits,
                InsuranceProviders = insuranceProviders
            };
        }

        public Task<GetCar.Result> GetCarAsync(ISessionStore session, GetCar.Request request)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var trains = session.Retrieve<Internal.TrainOptions>("train_options");
            var train = trains.Options.First(x => x.OptionRef == request.TrainRef);

            var cars = session.Retrieve<Internal.CarOptions>("car_options");
            var car = cars.Options.First(x => x.OptionRef == request.OptionRef);

            return Task.FromResult(new GetCar.Result
            {
                // TODO: add train info
                Car = car,
                // TODO: add car schemes
            });
        }

        public async Task<ReserveCreate.Result> CreateReserveAsync(ISessionStore session, ReserveCreate.Request request)
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
                        PlUpdown = $"{request.Option.Range.BottomCount.ToString()}{request.Option.Range.TopCount.ToString()}",
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
            
            return new ReserveCreate.Result
            {
                SaleOrderId = result.SaleOrderId,
                Price = priceWithCharges,
                Orders = from o in result.Orders
                         let durationParts = o.TimeInWay.Split(':').Select(x => int.Parse(x))
                         select new ReserveCreate.Result.ResultOrder
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
                             Car = new ReserveCreate.Result.Car
                             {
                                 Number = o.CNumber,
                                 Type = _carTypeConverter.ByCTypeI(o.Ctype), // WARNING: it's cTypeI here, not cType
                                 ServiceClass = o.ClsType,
                                 AdditionalInfo = o.TimeInfo
                             },
                             Tickets = o.Tickets.Select(t => new ReserveCreate.Result.Ticket
                             {
                                 TicketId = t.TicketId,
                                 Cost = t.Cost,
                                 Seats = t.Seats,
                                 SeatsType = t.SeatsType,
                                 Tariff = new ReserveCreate.Result.Tariff(t.Tariff, t.TariffName),
                                 Teema = t.Teema,
                                 Passengers = t.Pass.Select(p => _passengerConverter.ToPassenger(Array.IndexOf(t.Pass, p) + 1, p))
                             })
                         },
                PaymentSystems = result.PaymentSystems.Select(p => new ReserveCreate.Result.PaymentSystem
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Tip = p.Tip
                })
            };
        }

        public async Task<ReserveCancel.Result> CancelReserveAsync(ISessionStore session, ReserveCancel.Request request)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var login = session.Retrieve<Session>("login");
            var reserve = session.Retrieve<Parser.Structs.Layer5705>("reserve");

            if (reserve == null)
            {
                throw new ArgumentNullException(nameof(reserve));
            }

            var requestData = new Parser.Structs.Layer5769.Request
            {
                OrderId = reserve.SaleOrderId
            };

            var response = await _parser.CancelReserveAsync(login, requestData);

            reserve.Canceled = true;
            session.Store("reserve", reserve);

            return new ReserveCancel.Result
            {
                OrderId = reserve.SaleOrderId,
                Code = response.Result,
                Status = response.Status
            };
        }
    }
}
