using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient : AbstractKaolinApiClient
    {
        private readonly RailKaolinApiClientOptions _options;

        protected override string BaseUrl => "http://localhost:55101"; //_options.BaseUrl; // TODO: fix bug with _options eq null

        public RailKaolinApiClient(IOptions<RailKaolinApiClientOptions> optionsAccessor, ILogger<RailKaolinApiClient> logger) : base(logger)
        {
            _options = optionsAccessor.Value;
        }
    }
}
