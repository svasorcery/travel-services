namespace Kaolin.Models.Rail
{
    public class Passenger : Person
    {
        public int Ref { get; set; }
        public InsuranceProviderBase Insurance { get; set; }
        public MedicalPolicy Policy { get; set; }

        public Passenger(int @ref, Person person, InsuranceProviderBase insurance = null, MedicalPolicy policy = null) :
            base(person.Gender, person.FirstName, person.MiddleName, person.LastName, person.BirthDate, person.Passport)
        {
            Ref = @ref;
            Insurance = insurance;
            Policy = policy;
        }
    }

    public class PassengerRequest : Person
    {
        public int Ref { get; set; }
        public int? InsuranceProviderId { get; set; }
        public string PolicyEndDate { get; set; }

        public PassengerRequest(int @ref, Person person, int? insurance = null, string policy = null) :
            base(person.Gender, person.FirstName, person.MiddleName, person.LastName, person.BirthDate, person.Passport)
        {
            InsuranceProviderId = insurance;
            PolicyEndDate = policy;
        }
    }
}
