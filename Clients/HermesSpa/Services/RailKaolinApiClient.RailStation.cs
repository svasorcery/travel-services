namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class RailStation
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Express { get; set; }
            public int? Esr { get; set; }
            public Location Location { get; set; }
        }

        public class Location
        {
            public string Country { get; set; }
            public string Region { get; set; }
            public string Railway { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
        }
    }
}
