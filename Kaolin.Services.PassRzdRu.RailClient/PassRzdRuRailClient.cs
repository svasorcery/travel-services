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

        public PassRzdRuRailClient(IOptions<Config> optionsAccessor, PassRzdRuClient parser)
        {
            _config = optionsAccessor.Value;
            _parser = parser;
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
                var tp = new Internal.SearchTrainOptions
                {
                    Request = request,
                    Options = (from t in result.Tp[0].List
                               let tt = t.FlMsk == 3 || t.FlMsk == 2 ? TimeType.MOSCOW : TimeType.LOCAL
                               let durationParts = t.TimeInWay.Split(':').Select(x => int.Parse(x))
                               select new Internal.SearchTrainOptions.Option
                               {
                                   OptionRef = ++optionRef,
                                   DisplayNumber = t.Number2,
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
                session.Store("train_options", tp);
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

            var trains = session.Retrieve<Internal.SearchTrainOptions>("train_options");
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
    }
}
