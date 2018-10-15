using System;

namespace Oasis.Api.Traveller.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }


        public Person()
        {

        }
    }

    public enum Gender
    {
        FEMALE = 0,
        MALE = 1
    }
}
