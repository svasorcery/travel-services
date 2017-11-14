using System;

namespace Kaolin.Models.Rail
{
    public class Person
    {
        public int Ref { get; private set; }
        public Gender? Gender { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public Passport Passport { get; private set; }

        public Person(int @ref, Gender? gender, string firstName, string middleName, string lastName, DateTime? birthDate, Passport passport)
        {
            Ref = @ref;
            Gender = gender;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BirthDate = birthDate;
            Passport = passport;
        }
    }

    public enum Gender
    {
        FEMALE = 0,
        MALE = 1
    }
}
