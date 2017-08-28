namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Ticket data
    /// </summary>
    public class Layer5744
    {
        public string Result { get; set; }
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string TimeInfo { get; set; }
        public string CreateDate { get; set; }
        public string CurrentDate { get; set; }
        public bool IsFerry { get; set; }
        public bool IsBus { get; set; }
        public string IssueType { get; set; }
        public bool IsForeign { get; set; }
        public bool IsSNG { get; set; }
        public string ExpiredDate { get; set; }
        public bool BExpiredER { get; set; }
        public string Station0 { get; set; }
        public string Station0_lat { get; set; }
        public string Station1 { get; set; }
        public string Station1_lat { get; set; }
        public int DepartureYear { get; set; }
        public string DepartureDayMonth { get; set; }
        public string DepartureTime { get; set; }
        public int ArrivalYear { get; set; }
        public string ArrivalDayMonth { get; set; }
        public string ArrivalTime { get; set; }
        public string Agent { get; set; }
        public string CarrierName { get; set; }
        public string CarrierInn { get; set; }
        public string CarrierCode { get; set; }
        public bool WithoutServices { get; set; }
        public string ServiceInfo { get; set; }
        public string TrainNumber { get; set; }
        public string TrainNumber2 { get; set; }
        public string TrainEx { get; set; }
        public string CarNumber { get; set; }
        public string CarTypeLoc { get; set; }
        public string CarTypeRu { get; set; }
        public string CarTypeEn { get; set; }
        public string ServiceClass { get; set; }
        public string OrderAttrs { get; set; }
        public string AddSigns { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentForm { get; set; }
        public Ticket[] Tickets { get; set; }
        public string Timestamp { get; set; }

        public class Ticket
        {
            public long Id { get; set; }
            public string Number { get; set; }
            public bool BDeferred { get; set; }
            public string Price { get; set; }
            public string PriceEuro { get; set; }
            public string DocCode { get; set; }
            public string Birthdate { get; set; }
            public string CountryCode { get; set; }
            public string Gender { get; set; }
            public Passlist[] PassList { get; set; }
            public string PassCount { get; set; }
            public bool BRefund { get; set; }
            public decimal InsTariffValue { get; set; }
            public bool IsRefundedInReissue { get; set; }
            public string RefundDate { get; set; }
            public string Seats { get; set; }
            public string DPeople { get; set; }
            public string SeatsName { get; set; }
            public string SeatsNameEn { get; set; }
            public string TariffType { get; set; }
            public string DFifaAdditionalInfo { get; set; }
            public string TariffName { get; set; }
            public string TariffNameEng { get; set; }
            public bool BSchool { get; set; }
            public string Tariff { get; set; }
            public string TariffNDS { get; set; }
            public string TariffRetNDS { get; set; }
            public decimal TariffService { get; set; }
            public bool BFood { get; set; }
            public string Code2D { get; set; }
            public string TariffInfo { get; set; }
            public string VedTT { get; set; }
            public decimal TariffVed { get; set; }
            public decimal NdsRate1 { get; set; }
            public decimal TariffVedNds { get; set; }
            public decimal TariffVedService { get; set; }
            public decimal NdsRate2 { get; set; }
        }

        public class Passlist
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MidName { get; set; }
            public string DocCode { get; set; }
            public string DocNumber { get; set; }
            public string Birthdate { get; set; }
            public string CountryCode { get; set; }
            public string Gender { get; set; }
        }
    }
}
