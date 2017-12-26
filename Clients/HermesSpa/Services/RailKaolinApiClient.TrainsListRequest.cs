using System;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class TrainsListRequest
        {
            public string From { get; set; }
            public string To { get; set; }
            public DateTime DepartDate { get; set; }
            public int? HourFrom { get; set; }
            public int? HourTo { get; set; }
        }
    }
}
