using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Kaolin.Services.PassRzdRu.Parser.Structs;

namespace Kaolin.Services.PassRzdRu.Parser
{
    public partial class PassRzdRuClient
    {
        private readonly Config _config;
        private readonly ILogger _log;

        public PassRzdRuClient(IOptions<Config> configAccessor, ILogger<PassRzdRuClient> logger)
        {
            _config = configAccessor.Value;
            _log = logger;
        }


        public Task<Layer5827> GetTrainsAsync(Session session, Layer5827.Request request)
           => PostRidDictionary<Layer5827>("https://pass.rzd.ru/timetable/public/ru?layer_id=5827", session, _config.Polling.TrainList, request.ToDictionary());
    }
}
