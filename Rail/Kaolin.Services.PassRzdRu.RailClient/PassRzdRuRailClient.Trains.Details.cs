using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Kaolin.Models.Rail;
    using Kaolin.Infrastructure.SessionStore;

    public partial class PassRzdRuRailClient
    {
        public Task<QueryTrain.Result> QueryTrainAsync(ISessionStore session, QueryTrain.Request request)
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

            return Task.FromResult(new QueryTrain.Result
            {
                Train = new QueryTrain.Result.TrainOption
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
