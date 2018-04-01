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
            var train = trains.Options.First(x => x.OptionRef == request.TrainRef);

            var cars = session.Retrieve<Internal.CarOptions>("car_options");
            var car = cars.Options.First(x => x.OptionRef == request.OptionRef);

            return Task.FromResult(new QueryCar.Result
            {
                // TODO: add train info
                Car = car,
                // TODO: add car schemes
            });
        }
    }
}
