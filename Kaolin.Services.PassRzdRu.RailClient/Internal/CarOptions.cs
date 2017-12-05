using Kaolin.Models.Rail;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal
{
    public class CarOptions
    {
        public IEnumerable<QueryCars.Result.Car> Options { get; set; }
        // TODO: add Schemes
        public QueryCars.Result.AgeRestrictions AgeLimits { get; set; }
    }
}
