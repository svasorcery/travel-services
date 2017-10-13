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
    }
}
