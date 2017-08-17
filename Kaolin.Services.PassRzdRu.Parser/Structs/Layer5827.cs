using System;

namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Train list
    /// </summary>
    public class Layer5827
    {
        public string Result { get; set; }
        public string RID { get; set; }
        public string Timestamp { get; set; }
        public TpItem[] Tp { get; set; }

        public class TpItem
        {
            public string From { get; set; }
            public string FromCode { get; set; }
            public string Where { get; set; }
            public string WhereCode { get; set; }
            public string Date { get; set; }
            public bool NoSeats { get; set; }
            public string State { get; set; }

            public TpTrain[] List { get; set; }
        }

        public class TpTrain
        {
            public string Number { get; set; }
            public string Number2 { get; set; }
            public int Type { get; set; }
            public int TypeEx { get; set; }
            public bool ElReg { get; set; }
            public bool DeferredPayment { get; set; }
            public bool VarPrice { get; set; }
            public bool BEntire { get; set; }
            public string TrainName { get; set; }
            public string Brand { get; set; }
            public string Carrier { get; set; }

            public string Route0 { get; set; }
            public string Route1 { get; set; }
            public string TrDate0 { get; set; }
            public string TrTime0 { get; set; }
            public string Station0 { get; set; }
            public string Station1 { get; set; }
            public string Date0 { get; set; }
            public string Time0 { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }
            public string TimeInWay { get; set; }
            public int FlMsk { get; set; }
            public long Train_id { get; set; }
            public Car[] Cars { get; set; }
            public bool? DisabledType { get; set; }

            public bool CarMods { get; set; }
            public bool BFirm { get; set; }
        }

        public class Car
        {
            public int IType { get; set; }
            public string Type { get; set; }
            public string TypeLoc { get; set; }
            public int FreeSeats { get; set; }
            public bool BFreeInvisible { get; set; }
            public decimal Pt { get; set; }
            public decimal Tariff { get; set; }
            public string ServCls { get; set; }
        }

        public class Request
        {
            public string From { get; set; }
            public string To { get; set; }
            public DateTime FromDate { get; set; }
            public string Ti0 { get; set; }

            public Request(string from, string to, DateTime fromDate, string ti0)
            {
                From = from;
                To = to;
                FromDate = fromDate;
                Ti0 = ti0;
            }
        }
    }
}
