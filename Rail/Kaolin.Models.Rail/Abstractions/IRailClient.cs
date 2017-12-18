using System.Threading.Tasks;
using Kaolin.Infrastructure.SessionStore;

namespace Kaolin.Models.Rail.Abstractions
{
    public interface IRailClient
    {
        /// <summary>
        /// Search trains list
        /// </summary>
        /// <param name="session">Session temporary data store</param>
        /// <param name="request">Trains list request</param>
        /// <returns>Trains list</returns>
        Task<QueryTrains.Result> SearchTrainsAsync(ISessionStore session, QueryTrains.Request request);


        /// <summary>
        /// Get train info
        /// </summary>
        /// <param name="session">Session temporary data store</param>
        /// <param name="request">Train info request</param>
        /// <returns>Train info</returns>
        Task<QueryTrain.Result> GetTrainAsync(ISessionStore session, QueryTrain.Request request);


        /// <summary>
        /// Get train cars list
        /// </summary>
        /// <param name="session">Session temporary data store</param>
        /// <param name="request">Cars list request</param>
        /// <returns>Train cars list</returns>
        Task<QueryCars.Result> GetCarsAsync(ISessionStore session, QueryCars.Request request);

        /// <summary>
        /// Get train car info
        /// </summary>
        /// <param name="session">Session temporary data store</param>
        /// <param name="request">Car info request</param>
        /// <returns>Train, car, scheme and free seats info</returns>
        Task<QueryCar.Result> GetCarAsync(ISessionStore session, QueryCar.Request request);

        /// <summary>
        /// Create seat ticket reserve
        /// </summary>
        /// <param name="session">Session temporary data store</param>
        /// <param name="request">Reserve creation request</param>
        /// <returns>Reserve creation result</returns>
        Task<QueryReserveCreate.Result> CreateReserveAsync(ISessionStore session, QueryReserveCreate.Request request);

        /// <summary>
        /// Cancel existing seat ticket reserve
        /// </summary>
        /// <param name="session">Session temporary data store</param>
        /// <returns>Reserve cancelation result status</returns>
        Task<QueryReserveCancel.Result> CancelReserveAsync(ISessionStore session);
    }
}
