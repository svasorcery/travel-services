using Kaolin.Models.Rail;
using System.Collections.Generic;

namespace Kaolin.Api.PassRzdRu.Models
{
    public class GetCarsResult
    {
        public IEnumerable<QueryCars.Result.Car> Cars { get; set; }
    }
}
