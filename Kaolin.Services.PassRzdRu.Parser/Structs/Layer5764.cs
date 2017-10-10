namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Car list
    /// </summary>
    public class Layer5764
    {
        public string Result { get; set; }
        public string RID { get; set; }
        public string Timestamp { get; set; }

        public LstItem[] Lst { get; set; }
        public Scheme[] Schemes { get; set; }
        public InsuranceProvider[] InsuranceCompany { get; set; }
        public FoodIconTip[] FoodIconTips { get; set; }

        public class LstItem
        {
            public string Number { get; set; }
            public string Number2 { get; set; }
            public string Type { get; set; }
            public bool? Virtual { get; set; }
            public bool? Bus { get; set; }

            public string DefShowTime { get; set; }
            public string Date0 { get; set; }
            public string Time0 { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }
            public string LocalDate1 { get; set; }
            public string LocalTime1 { get; set; }
            public string TimeDeltaString1 { get; set; }
            public string TimeSt0 { get; set; }
            public string TimeSt1 { get; set; }

            public string Station0 { get; set; }
            public string Code0 { get; set; }
            public string Station1 { get; set; }
            public string Code1 { get; set; }
            public string Route0 { get; set; }
            public string Route1 { get; set; }

            public Car[] Cars { get; set; }
            public FunctionBlock[] FunctionBlocks { get; set; }

            public int ChildrenAge { get; set; }
            public int MotherAndChildAge { get; set; }
            public bool PartialPayment { get; set; }

            public class Car
            {
                public string CNumber { get; set; }
                public string Type { get; set; }
                public string TypeLoc { get; set; }
                public int CTypeI { get; set; }
                public int CType { get; set; }
                public string Letter { get; set; }
                public string ClsType { get; set; }
                public string ClsName { get; set; }
                public Service[] Services { get; set; }
                public string Tariff { get; set; }
                public string Tariff2 { get; set; }
                public string TariffServ { get; set; }
                public string AddSigns { get; set; }
                public string Carrier { get; set; }
                public long CarrierId { get; set; }
                public bool? InsuranceFlag { get; set; }
                public string Owner { get; set; }
                public bool ElReg { get; set; }
                public bool Food { get; set; }
                public bool RegularFoodService { get; set; }
                public bool NoSmok { get; set; }
                public bool InetSaleOff { get; set; }
                public bool BVip { get; set; }
                public bool ConferenceRoomFlag { get; set; }
                public bool BDeck2 { get; set; }
                public string IntServiceClass { get; set; }
                public string SpecialSeatTypes { get; set; }
                public bool DeferredPayment { get; set; }
                public bool VarPrice { get; set; }
                public bool Ferry { get; set; }
                public string SeniorTariff { get; set; }
                public bool Bedding { get; set; }
                public bool Youth { get; set; }
                public bool Unior { get; set; }
                public string Places { get; set; }
                public Seat[] Seats { get; set; }
                public long SchemeId { get; set; }
                public bool ForcedBedding { get; set; }
                public bool PolicyAvailable { get; set; }

                public class Service
                {
                    public long Id { get; set; }
                    public string Name { get; set; }
                    public string Description { get; set; }
                    public bool? HasImage { get; set; }
                }

                public class Seat
                {
                    public string Type { get; set; }
                    public int Free { get; set; }
                    public string Label { get; set; }
                    public string Tariff { get; set; }
                    public string TariffServ { get; set; }
                    public string Places { get; set; }
                }
            }

            public class FunctionBlock
            {
                public string ClassName { get; set; }
                public string Name { get; set; }
            }
        }

        public class Scheme
        {
            public long Id { get; set; }
            public string Html { get; set; }
            public string Image { get; set; }
        }

        public class InsuranceProvider
        {
            public int Id { get; set; }
            public int SortOrder { get; set; }
            public string ShortName { get; set; }
            public string OfferUrl { get; set; }
            public decimal InsuranceCost { get; set; }
            public decimal InsuranceBenefit { get; set; }
        }

        public class FoodIconTip
        {
            public string Name { get; set; }
            public string Tip { get; set; }
        }


        public class Request
        {
            public int Dir { get; set; }
            public string Code0 { get; set; }
            public string Code1 { get; set; }
            public string TNum0 { get; set; }
            public string Dt0 { get; set; }
            public bool BEntire { get; set; }

            public Request(int dir, string code0, string code1, string tnum0, string dt0, bool bEntire)
            {
                Dir = dir;
                Code0 = code0;
                Code1 = code1;
                TNum0 = tnum0;
                Dt0 = dt0;
                BEntire = bEntire;
            }
        }
    }
}
