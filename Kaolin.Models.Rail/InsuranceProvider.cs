namespace Kaolin.Models.Rail
{
    public class InsuranceProviderBase
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string OfferUrl { get; set; }
        public decimal InsuranceCost { get; set; }
    }

    public class InsuranceProvider : InsuranceProviderBase
    {
        public decimal InsuranceBenefit { get; set; }
    }
}
