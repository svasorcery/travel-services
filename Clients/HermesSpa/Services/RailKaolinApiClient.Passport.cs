using System;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class Passport
        {
            public string Type { get; set; }
            public string Series { get; set; }
            public string Number { get; set; }
            public Country Citizenship { get; set; }
            public DateTime? Expire { get; set; }
        }
    }
}
