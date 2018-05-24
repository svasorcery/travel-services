using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Kaolin.Models.Rail;
    using Kaolin.Infrastructure.SessionStore;

    public partial class PassRzdRuRailClient
    {
        public Task<QueryCar.Result> QueryCarAsync(ISessionStore session, QueryCar.Request request)
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
            var trainOption = trains.Options.First(x => x.OptionRef == request.TrainRef);
            var train = new QueryTrain.Result.TrainOption
            {
                OptionRef = trainOption.OptionRef,
                DisplayNumber = trainOption.DisplayNumber,
                Brand = trainOption.Brand,
                BEntire = trainOption.BEntire,
                IsFirm = trainOption.IsFirm,
                HasElectronicRegistration = trainOption.HasElectronicRegistration,
                HasDynamicPricing = trainOption.HasDynamicPricing,
                TripDuration = trainOption.TripDuration,
                RouteStart = trainOption.RouteStart,
                RouteEndStation = trainOption.RouteEndStation,
                Depart = trainOption.Depart,
                Arrive = trainOption.Arrive,
            };

            var cars = session.Retrieve<Internal.CarOptions>("car_options");
            var car = cars.Options.First(x => x.OptionRef == request.OptionRef);
            var scheme = cars.Schemes.First(x => x.Id.ToString() == car.SchemeId);

            var freeSeats = car.FreeSeats.SelectMany(x => x.Places).Where(x => x != null).OrderBy(x => x.Number);
            var freeSeatsCells = scheme.Rows.SelectMany(x => x).Where(x => x.Place != null);
            foreach (var seat in freeSeats)
            {
                freeSeatsCells.First(x => x.Place.Number == seat.Number).Place = seat;
            }

            scheme = Internal.Converters.CarSchemeConverter.SetCellStyleClasses(scheme, freeSeats);

            return Task.FromResult(new QueryCar.Result
            {
                Train = train,
                Car = car,
                Scheme = scheme
            });
        }
    }
}
