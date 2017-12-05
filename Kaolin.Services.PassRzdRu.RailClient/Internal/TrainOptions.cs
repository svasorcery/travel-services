using System;
using Kaolin.Models.Rail;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal
{
    public class TrainOptions
    {
        public QueryTrains.Request Request { get; set; }
        public IEnumerable<Option> Options { get; set; }

        public class Option
        {
            public int OptionRef { get; set; }
            public string Number { get; set; }
            public string DisplayNumber { get; set; }
            public string Type { get; set; }
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
