namespace Kaolin.Models.Rail
{
    public class Passenger : Person
    {
        public int Ref { get; set; }
        public int? Insurance { get; set; } // TODO: add insurance, ref #49

        public Passenger(int @ref, Person person, int? insurance = null) :
            base(person.Gender, person.FirstName, person.MiddleName, person.LastName, person.BirthDate, person.Passport)
        {
            Ref = @ref;
            Insurance = insurance;
        }
    }
}
