using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Parser;

    public class PassRzdRuRailClient
    {
        private readonly Config _config;
        private readonly PassRzdRuClient _parser;

        public PassRzdRuRailClient(IOptions<Config> optionsAccessor, PassRzdRuClient parser)
        {
            _config = optionsAccessor.Value;
            _parser = parser;
        }
    }
}
