namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class Country
        {
            public string Id { get; set; }
            public int CountryId { get; set; }
            public int? CountryIdRzd { get; set; }
            public string Alpha2 { get; set; }
            public string NameRu { get; set; }
            public string NameEn { get; set; }
        }
    }
}
