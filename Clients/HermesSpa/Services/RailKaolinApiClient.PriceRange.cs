namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class PriceRange
        {
            public Price Min { get; set; }
            public Price Max { get; set; }
        }

        public class Price
        {
            public decimal Total { get; set; }
        }
    }
}
