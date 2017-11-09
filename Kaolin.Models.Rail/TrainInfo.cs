using System;

namespace Kaolin.Models.Rail
{
    public class TrainInfo
    {
        public int OptionRef { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string DisplayNumber { get; set; }
        public TripEvent RouteStart { get; set; }
        public string RouteEndStation { get; set; }
        public TripEvent Depart { get; set; }
        public TripEvent Arrive { get; set; }
        public TripEvent ArriveLocal { get; set; }
        public TimeSpan TripDuration { get; set; }
        public string TimezoneDifference { get; set; }
        public string Carrier { get; set; }
        public string Brand { get; set; }
        public bool BEntire { get; set; }
        public bool IsFirm { get; set; }
        public bool IsComponent { get; set; }
        public bool HasElectronicRegistration { get; set; }
        public bool HasDynamicPricing { get; set; }
        public int? TripDistance { get; set; }
    }
}
