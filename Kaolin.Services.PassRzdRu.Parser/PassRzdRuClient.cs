using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

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
