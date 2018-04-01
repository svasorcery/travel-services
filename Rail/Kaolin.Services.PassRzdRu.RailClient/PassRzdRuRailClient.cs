using Microsoft.Extensions.Options;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Parser;
    using Kaolin.Models.Rail.Abstractions;

    public partial class PassRzdRuRailClient : IRailClient
    {
        private readonly Config _config;
        private readonly PassRzdRuClient _parser;
        private readonly Internal.Converters.PriceConverter _priceConverter;
        private readonly Internal.Converters.PassengerToLayer5705 _passengerConverter;
        private readonly Internal.Converters.CarTypeConverter _carTypeConverter;

        public PassRzdRuRailClient(IOptions<Config> optionsAccessor, PassRzdRuClient parser)
        {
            _config = optionsAccessor.Value;
            _parser = parser;
            _priceConverter = new Internal.Converters.PriceConverter();
            _passengerConverter = new Internal.Converters.PassengerToLayer5705();
            _carTypeConverter = new Internal.Converters.CarTypeConverter();
        }
    }
}
