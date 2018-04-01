using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Parser;
    using Kaolin.Models.Rail;
    using Kaolin.Infrastructure.SessionStore;

    public partial class PassRzdRuRailClient
    {
        public async Task<QueryCars.Result> QueryCarsAsync(ISessionStore session, QueryCars.Request request)
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
                            select new QueryCars.Result.Car
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
                                FreeSeats = c.Seats.Select(s => new QueryCars.Result.SeatGroup
                                {
                                    Type = s.Type,
                                    Label = s.Label?.Replace("&nbsp;", " "),
                                    Price = _priceConverter.ToPrice(s.Tariff),
                                    Places = Internal.Converters.FreePlacesConverter.Convert(s.Places),
                                    Count = s.Free
                                }).ToArray(),
                                Services = c.Services.Select(s => new QueryCars.Result.CarService
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

            var ageLimits = new QueryCars.Result.AgeRestrictions
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

            return new QueryCars.Result
            {
                // TODO: add train info
                Cars = cars,
                AgeLimits = ageLimits,
                InsuranceProviders = insuranceProviders
            };
        }
    }
}
