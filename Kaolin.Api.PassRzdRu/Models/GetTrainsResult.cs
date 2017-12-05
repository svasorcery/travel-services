using Kaolin.Models.Rail;
using System.Collections.Generic;

namespace Kaolin.Api.PassRzdRu.Models
{
    public class GetTrainsResult
    {
        public string SessionId { get; set; }
        public IEnumerable<QueryTrains.Result.Train> Trains { get; set; }
    }
}
