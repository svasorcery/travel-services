using Kaolin.Models.Rail;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal
{
    public class CarOptions
    {
        public IEnumerable<QueryCars.Result.Car> Options { get; set; }
        public IEnumerable<QueryCars.Result.CarScheme> Schemes { get; set; }
        public QueryCars.Result.AgeRestrictions AgeLimits { get; set; }
    }
}
