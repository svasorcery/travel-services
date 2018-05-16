namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class CarScheme
        {
            public long Id { get; set; }
            public CarSchemeCell[][] Rows { get; set; }
        }

        public class CarSchemeCell
        {
            public string Type { get; set; }
            public CarPlace Place { get; set; }
            public string Content { get; set; }
            public string Border { get; set; }
            public string StyleClass { get; set; }
        }
    }
}
