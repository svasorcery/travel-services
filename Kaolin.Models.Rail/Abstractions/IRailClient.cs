using System.Threading.Tasks;
using Kaolin.Infrastructure.SessionStore;

namespace Kaolin.Models.Rail.Abstractions
{
    public interface IRailClient
    {
        Task<QueryTrains.Result> SearchTrainsAsync(ISessionStore session, QueryTrains.Request request);
        Task<QueryTrain.Result> GetTrainAsync(ISessionStore session, QueryTrain.Request request);

        Task<GetCars.Result> GetCarsAsync(ISessionStore session, GetCars.Request request);
        Task<GetCar.Result> GetCarAsync(ISessionStore session, GetCar.Request request);

        Task<ReserveCreate.Result> CreateReserveAsync(ISessionStore session, ReserveCreate.Request request);
        Task<ReserveCancel.Result> CancelReserveAsync(ISessionStore session, ReserveCancel.Request request);
    }
}
