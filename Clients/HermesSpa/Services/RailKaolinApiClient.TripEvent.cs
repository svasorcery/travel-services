using System;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class TripEvent
        {
            public DateTime DateAndTime { get; set; }
            public TimeType TimeType { get; set; }
            public RailStation Station { get; set; }

            public class RailStation
            {
                public string Name { get; set; }
                public string Code { get; set; }
            }
        }

        public enum TimeType
        {
            MOSCOW = 0,
            LOCAL = 1,
            GMT = 2
        }
    }
}
