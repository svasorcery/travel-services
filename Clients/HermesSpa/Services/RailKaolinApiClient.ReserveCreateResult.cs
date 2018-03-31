using System;
using System.Collections.Generic;

namespace HermesSpa.Services
{
    public partial class RailKaolinApiClient
    {
        public class ReserveCreateResult
        {
            public string SaleOrderId { get; set; }
            public Price Price { get; set; }
            public IEnumerable<ResultOrder> Orders { get; set; }
            public IEnumerable<PaymentSystem> PaymentSystems { get; set; }

            public class ResultOrder
            {
                public string OrderId { get; set; }
                public decimal Cost { get; set; }
                public int TotalCostPt { get; set; }

                public TrainInfo Train { get; set; }
                public Car Car { get; set; }
                public IEnumerable<Ticket> Tickets { get; set; }

                public string Created { get; set; }
                public string SeatNums { get; set; }
                public string DirName { get; set; }
                public string Agent { get; set; }
                //public bool DeferredPayment { get; set; }
            }

            public class TrainInfo
            {
                public int OptionRef { get; set; }
                public string Name { get; set; }
                public string Number { get; set; }
                public string DisplayNumber { get; set; }
                public TripEvent RouteStart { get; set; }
                public string RouteEndStation { get; set; }
                public TripEvent Depart { get; set; }
                public TripEvent Arrive { get; set; }
                public TripEvent ArriveLocal { get; set; }
                public TimeSpan TripDuration { get; set; }
                public string TimezoneDifference { get; set; }
                public string Carrier { get; set; }
                public string Brand { get; set; }
                public bool BEntire { get; set; }
                public bool IsFirm { get; set; }
                public bool IsComponent { get; set; }
                public bool HasElectronicRegistration { get; set; }
                public bool HasDynamicPricing { get; set; }
                public int? TripDistance { get; set; }
            }

            public class Car
            {
                public string Number { get; set; }
                public CarType Type { get; set; }
                public string ServiceClass { get; set; }
                public string AdditionalInfo { get; set; }
            }

            public class Ticket
            {
                public string TicketId { get; set; }
                public decimal Cost { get; set; }
                public string Seats { get; set; }
                public string SeatsType { get; set; }
                public Tariff Tariff { get; set; }
                public bool Teema { get; set; }
                public IEnumerable<Passenger> Passengers { get; set; }
            }

            public class Passenger : Person
            {
                public int Ref { get; set; }
                public InsuranceProviderBase Insurance { get; set; }
                public MedicalPolicy Policy { get; set; }
            }

            public class InsuranceProviderBase
            {
                public int Id { get; set; }
                public string FullName { get; set; }
                public string ShortName { get; set; }
                public string OfferUrl { get; set; }
                public decimal InsuranceCost { get; set; }
            }

            public class MedicalPolicy
            {
                public int StatusId { get; set; }
                public int Number { get; set; }
                public int AreaId { get; set; }
                public decimal Cost { get; set; }
                public string DateStart { get; set; }
                public string DateEnd { get; set; }
            }

            public class Tariff
            {
                public string Code { get; set; }
                public string Name { get; set; }
            }

            public class PaymentSystem
            {
                public int Id { get; set; }
                public string Code { get; set; }
                public string Name { get; set; }
                public string Tip { get; set; }
            }
        }
    }
}
