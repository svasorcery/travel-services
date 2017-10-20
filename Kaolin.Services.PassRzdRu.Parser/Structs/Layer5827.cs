using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Train list
    /// </summary>
    public class Layer5827 : IRidRequestResponse
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
            public string DefShowTime { get; set; }
            public string State { get; set; }

            public TpTrain[] List { get; set; }

            public ServiceMessage[] MsgList { get; set; }

            public string TransferSearchMode { get; set; }
            public bool? FlFPKRoundBonus { get; set; }
            //public string Discounts { get; set; }


            public class TpTrain
            {
                public string Number { get; set; }
                public string Number2 { get; set; }
                public int Type { get; set; }
                public int TypeEx { get; set; }
                public int Depth { get; set; }
                public bool New { get; set; }
                public bool ElReg { get; set; }
                public bool DeferredPayment { get; set; }
                public bool VarPrice { get; set; }
                public bool BEntire { get; set; }
                public bool BFirm { get; set; }
                public bool CarMods { get; set; }
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
                public string LocalDate1 { get; set; }
                public string LocalTime1 { get; set; }
                public string TimeDeltaString1 { get; set; }
                public string TimeInWay { get; set; }
                public int FlMsk { get; set; }
                public long Train_id { get; set; }
                public bool? DisabledType { get; set; }
                public Car[] Cars { get; set; }
            }

            public class Car
            {
                public int IType { get; set; }
                public string Type { get; set; }
                public string TypeLoc { get; set; }
                public int FreeSeats { get; set; }
                public decimal Pt { get; set; }
                public decimal Tariff { get; set; }
                public string ServCls { get; set; }
                public bool? BFreeInvisible { get; set; }
                public bool? DisabledPerson { get; set; }
            }

            public class ServiceMessage
            {
                public string Message { get; set; }
                public string AddInfo { get; set; }
                public string Type { get; set; }
            }
        }

        public class Request
        {
            public string From { get; set; }
            public string To { get; set; }
            public DateTime DepartDate { get; set; }
            public HourInterval DepartTime { get; set; }

            public Request(string from, string to, DateTime departDate, int hourFrom = 0, int hourTo = 24)
            {
                From = from;
                To = to;
                DepartDate = departDate;
                DepartTime = HourInterval.Get(hourFrom, hourTo);
            }

            public Dictionary<string, string> ToDictionary()
            {
                var requestParams = new Dictionary<string, string>
                {
                    ["dir"] = "0",
                    ["tfl"] = "1",
                    ["code0"] = From,
                    ["code1"] = To,
                    ["dt0"] = DepartDate.ToString("dd.MM.yyyy"),
                    ["ti0"] = DepartTime.ToString(),
                    ["checkSeats"] = "1"
                };

                return requestParams;
            }


            public class HourInterval
            {
                public int From { get; set; }
                public int To { get; set; }
                public override string ToString() => $"{From}-{To}";

                HourInterval(int from, int to)
                {
                    From = from;
                    To = to;
                }

                public static HourInterval Get(int from, int to)
                {
                    return CheckRange(from, to) ? new HourInterval(from, to) : new HourInterval(0, 24);
                }

                private static bool CheckRange(int from, int to)
                {
                    return (from >= 0 && from < 24)
                        && (to > 0 && to <= 24)
                        && from < to;
                }
            }
        }
    }
}
