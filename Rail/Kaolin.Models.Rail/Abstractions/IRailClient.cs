using System.Threading.Tasks;
using Kaolin.Infrastructure.SessionStore;

namespace Kaolin.Models.Rail.Abstractions
{
    public interface IRailClient
    {
        Task<QueryTrains.Result> SearchTrainsAsync(ISessionStore session, QueryTrains.Request request);
        Task<QueryTrain.Result> GetTrainAsync(ISessionStore session, QueryTrain.Request request);

        Task<QueryCars.Result> GetCarsAsync(ISessionStore session, QueryCars.Request request);
        Task<QueryCar.Result> GetCarAsync(ISessionStore session, QueryCar.Request request);

        Task<QueryReserveCreate.Result> CreateReserveAsync(ISessionStore session, QueryReserveCreate.Request request);
        Task<QueryReserveCancel.Result> CancelReserveAsync(ISessionStore session);
    }
}
