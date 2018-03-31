using System;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class Person
        {
            public string Gender { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime? BirthDate { get; set; }
            public Passport Passport { get; set; }
        }
    }
}
