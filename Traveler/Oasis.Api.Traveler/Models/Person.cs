using System;

namespace Oasis.Api.Traveler.Models
{
    public class Person
    {
        public int? Id { get; set; }
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
