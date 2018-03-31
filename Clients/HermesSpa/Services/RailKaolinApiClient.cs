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


        public Task<IEnumerable<Country>> SearchCountriesAsync(string q)
            => Get<IEnumerable<Country>>($"api/countries?term={q}");

        public Task<IEnumerable<RailStation>> SearchStationsAsync(string q)
            => Get<IEnumerable<RailStation>>($"api/stations?term={q}");

        public Task<TrainsListResult> QueryTrainsAsync(TrainsListRequest request)
            => Get<TrainsListResult>("api/trains", 
                ("from", request.From),
                ("to", request.To),
                ("departDate", request.DepartDate.ToString("yyyy.MM.dd")),
                ("hourFrom", request.HourFrom.HasValue ? request.HourFrom.ToString() : "0"),
                ("hourTo",  request.HourTo.HasValue ? request.HourTo.ToString() : "24")
                );

        public Task<CarsListResult> QueryCarsAsync(string sessionId, int optionRef)
            => Get<CarsListResult>($"api/trains/{sessionId}/{optionRef}/cars");

        public Task<SeatsListResult> QuerySeatsAsync(string sessionId, int trainsRef, int optionRef)
            => Get<SeatsListResult>($"api/trains/{sessionId}/{trainsRef}/cars/{optionRef}");

        public Task<ReserveCreateResult> CreateReserveAsync(ReserveCreateRequest request)
            => Post<ReserveCreateResult, ReserveCreateRequest>("api/reserve", request);
    }
}
