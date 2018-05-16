using Kaolin.Models.Rail;
using System.Collections.Generic;

namespace Kaolin.Api.Rail.Models
{
    public class GetCarsResult
    {
        public string SessionId { get; set; }
        public int TrainOption { get; set; }
        public IEnumerable<QueryCars.Result.Car> Cars { get; set; }
        public IEnumerable<InsuranceProvider> InsuranceProviders { get; set; }
        public QueryCars.Result.AgeRestrictions AgeLimits { get; set; }
    }
}
