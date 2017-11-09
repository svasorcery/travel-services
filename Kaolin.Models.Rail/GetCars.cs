using System.Collections.Generic;

namespace Kaolin.Models.Rail
{
    public class GetCars
    {
        public class Request
        {
            public int OptionRef { get; set; }
        }

        public class Result
        {
            public TrainInfo Train { get; set; }
            public IEnumerable<Car> Cars { get; set; }
            public IEnumerable<InsuranceProvider> InsuranceProviders { get; set; }
            public AgeRestrictions AgeLimits { get; set; }

            public class Car
            {
                public int OptionRef { get; set; }
                public string Number { get; set; }
                public CarType CarType { get; set; }
                public string ServiceClass { get; set; }
                public string ServiceClassInternational { get; set; }
                public string Letter { get; set; }
                public string[] Categories { get; set; }
                public string SchemeId { get; set; }
                public int[] FreePlaceNumbers { get; set; }
                public string[] SpecialSeatTypes { get; set; }
                public SeatGroup[] FreeSeats { get; set; }
                public CarService[] Services { get; set; }
                public string ServicesDescription { get; set; }
                public PriceRange Price { get; set; }
                public string Carrier { get; set; }
                public string Owner { get; set; }
                public bool HasElectronicRegistration { get; set; }
                public bool HasDynamicPricing { get; set; }
                public bool IsNoSmoking { get; set; }
                public bool CanAddBedding { get; set; }
                public bool HasBeddingIncluded { get; set; }
                public bool IsTwoStorey { get; set; }
                public bool IsWebSalesForbidden { get; set; }
            }

            public class SeatGroup
            {
                public string Type { get; set; }
                public string Label { get; set; }
                public Price Price { get; set; }
                public int[] Places { get; set; }
                public int Count { get; set; }
            }

            public class CarService
            {
                public string Name { get; set; }
                public string Description { get; set; }
            }

            public class InsuranceProvider
            {
                public int Id { get; set; }
                public string ShortName { get; set; }
                public string OfferUrl { get; set; }
                public decimal InsuranceCost { get; set; }
                public decimal InsuranceBenefit { get; set; }
            }

            public class AgeRestrictions
            {
                public int InfantWithoutPlace { get; set; }
                public int ChildWithPlace { get; set; }
            }
        }
    }
}
