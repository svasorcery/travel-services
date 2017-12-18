using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Kaolin.Services.PassRzdRu.Parser
{
    using Kaolin.Services.PassRzdRu.Parser.Structs;

    public partial class PassRzdRuClient
    {
        private readonly Config _config;
        private readonly ILogger _log;

        public PassRzdRuClient(IOptions<Config> configAccessor, ILogger<PassRzdRuClient> logger)
        {
            _config = configAccessor.Value;
            _log = logger;
            _cookieContainer = new System.Net.CookieContainer();
            _sessionUri = new System.Uri("https://pass.rzd.ru");
        }

        /// <summary>
        /// Trains list
        /// </summary>
        /// <param name="session">Authentication session</param>
        /// <param name="request">Trains list request</param>
        /// <returns>Trains list</returns>
        public Task<Layer5827> GetTrainsAsync(Session session, Layer5827.Request request)
           => PostRidDictionary<Layer5827>("https://pass.rzd.ru/timetable/public/ru?layer_id=5827", session, _config.Polling.TrainList, request.ToDictionary());

        /// <summary>
        /// Cars list
        /// </summary>
        /// <param name="session">Authentication session</param>
        /// <param name="request">Cars list request</param>
        /// <returns>Cars list</returns>
        public Task<Layer5764> GetCarsAsync(Session session, Layer5764.Request request)
            => PostRidDictionary<Layer5764>("https://pass.rzd.ru/timetable/public/ru?layer_id=5764", session, _config.Polling.CarList, request.ToDictionary());

        /// <summary>
        /// Reserve rzd ticket
        /// </summary>
        /// <param name="session">Authentication session</param>
        /// <param name="request">Reserve ticket request</param>
        /// <returns>Reserve creation result</returns>
        public Task<Layer5705> ReserveTicketAsync(Session session, Layer5705.Request request)
            => PostRidDictionary<Layer5705>("https://pass.rzd.ru/ticket/secure/ru?layer_id=5705&STRUCTURE_ID=735", session, _config.Polling.Order, request.ToDictionary());

        /// <summary>
        /// Cancel existing rzd ticket reserve
        /// </summary>
        /// <param name="session">Authentication session</param>
        /// <param name="request">Cancel reserve request</param>
        /// <returns>Reserve cancelation result</returns>
        public Task<Layer5769> CancelReserveAsync(Session session, Layer5769.Request request)
            => PostDictionary<Layer5769>("https://pass.rzd.ru/ticket/secure/ru?layer_id=5769", session, request.ToDictionary());
    }
}
