using System;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class SeatsListResult
        {
            public TrainOption Train { get; set; }
            public Car Car { get; set; }
        }

        public class TrainOption
        {
            public int OptionRef { get; set; }
            public string DisplayNumber { get; set; }
            public string Brand { get; set; }
            public bool BEntire { get; set; }
            public bool IsFirm { get; set; }
            public bool HasElectronicRegistration { get; set; }
            public bool HasDynamicPricing { get; set; }
            public TimeSpan TripDuration { get; set; }
            public TripEvent RouteStart { get; set; }
            public string RouteEndStation { get; set; }
            public TripEvent Depart { get; set; }
            public TripEvent Arrive { get; set; }
        }
    }
}
