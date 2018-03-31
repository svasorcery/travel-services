namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class ReserveCreateRequest
        {
            public OptionParams Option { get; set; }
            public PassengerRequest[] Passengers { get; set; }


            public class OptionParams
            {
                public string SessionId { get; set; }
                public int TrainOptionRef { get; set; }
                public int CarOptionRef { get; set; }
                public PlacesRange Range { get; set; }
                public bool Bedding { get; set; }
                public string Location { get; set; }
            }

            public class PlacesRange
            {
                public int From { get; set; }
                public int To { get; set; }
                public int TopCount { get; set; }
                public int BottomCount { get; set; }
            }

            public class PassengerRequest : Person
            {
                public int Ref { get; set; }
                public int? InsuranceProviderId { get; set; }
                public string PolicyEndDate { get; set; }
                public string LoalityCardNumber { get; set; }
                public string PartnerCardNumber { get; set; }
            }
        }
    }
}
