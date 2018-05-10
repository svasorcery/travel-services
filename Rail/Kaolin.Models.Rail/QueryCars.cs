using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Kaolin.Models.Rail
{
    public class QueryCars
    {
        public class Request
        {
            public int OptionRef { get; set; }


            public Request()
            {

            }

            public Request(int optionRef)
            {
                OptionRef = optionRef;
            }
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
                public CarType Type { get; set; }
                public string ServiceClass { get; set; }
                public string ServiceClassInternational { get; set; }
                public string Letter { get; set; }
                public string Categories { get; set; }
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
                public CarPlace[] Places { get; set; }
                public int Count { get; set; }
            }

            public class CarService
            {
                public string Name { get; set; }
                public string Description { get; set; }
            }

            public class AgeRestrictions
            {
                public int InfantWithoutPlace { get; set; }
                public int ChildWithPlace { get; set; }
            }

            public class CarScheme
            {
                public long Id { get; set; }
                public CarSchemeCell[][] Rows { get; set; }
            }

            public class CarSchemeCell
            {
                public string Type { get; set; }
                [BsonIgnoreIfNull]
                public CarPlace Place { get; set; }
                [BsonIgnoreIfNull]
                public string Content { get; set; }
                [BsonIgnoreIfNull]
                public string Border { get; set; }
                [BsonIgnoreIfNull]
                public string StyleClass { get; protected set; }

                public void AppendStyleClass(string style)
                {
                    if (!String.IsNullOrEmpty(StyleClass))
                    {
                        StyleClass += " ";
                    }
                    StyleClass += style;
                }
            }

            public class CarPlace
            {
                public int Number { get; set; }
                [BsonIgnoreIfNull]
                public string Gender { get; set; }
                [BsonIgnoreIfNull]
                public Price Price { get; set; }
                [BsonIgnoreIfDefault]
                public bool IsFree { get; set; }

                public CarPlace(int number, string gender, Price price = null)
                {
                    Number = number;
                    Gender = gender;
                    Price = price;
                    IsFree = true;
                }
                public CarPlace(int number, bool isFree)
                {
                    Number = number;
                    IsFree = isFree;
                }

                public CarPlace()
                {

                }
            }
        }
    }
}
