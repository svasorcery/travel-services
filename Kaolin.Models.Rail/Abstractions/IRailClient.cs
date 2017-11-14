using System.Threading.Tasks;
using Kaolin.Infrastructure.SessionStore;

namespace Kaolin.Models.Rail.Abstractions
{
    public interface IRailClient
    {
        Task<SearchTrains.Result> SearchTrainsAsync(ISessionStore session, SearchTrains.Request request);
        Task<GetTrain.Result> GetTrainAsync(ISessionStore session, GetTrain.Request request);

        Task<GetCars.Result> GetCarsAsync(ISessionStore session, GetCars.Request request);
        Task<GetCar.Result> GetCarAsync(ISessionStore session, GetCar.Request request);

        Task<ReserveCreate.Result> CreateReserveAsync(ISessionStore session, ReserveCreate.Request request);
        Task<ReserveCancel.Result> CancelReserveAsync(ISessionStore session, ReserveCancel.Request request);
    }
}
