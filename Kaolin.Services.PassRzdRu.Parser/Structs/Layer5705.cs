using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Reserve seat
    /// </summary>
    public class Layer5705 : IRidRequestResponse
    {
        public string Result { get; set; }
        public string RID { get; set; }
        public string Type { get; set; }
        public string Error { get; set; }
        public string Info { get; set; }
        public string Timestamp { get; set; }
        public bool Canceled { get; set; }

        public string SaleOrderId { get; set; }
        public ResultOrder[] Orders { get; set; }
        public string TotalSum { get; set; }
        public PaymentSystem[] PaymentSystems { get; set; }

        public class ResultOrder
        {
            public string OrderId { get; set; }
            public string Created { get; set; }
            public string DirName { get; set; }

            public string Code0 { get; set; }
            public string Station0 { get; set; }
            public bool Msk0 { get; set; }

            public string Code1 { get; set; }
            public string Station1 { get; set; }
            public bool Msk1 { get; set; }

            public string TimeInfo { get; set; }
            public decimal Cost { get; set; }

            public string Date0 { get; set; }
            public string Time0 { get; set; }
            public string Route0 { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }
            public string Route1 { get; set; }

            public string Number { get; set; }
            public string Number2 { get; set; }
            public string TimeInWay { get; set; }
            public string CNumber { get; set; }
            public string AddSigns { get; set; }
            public string ClsType { get; set; }
            public string Type { get; set; }
            public int Ctype { get; set; }
            public string Carrier { get; set; }
            public string Agent { get; set; }
            public string SeatNums { get; set; }
            public decimal TotalCostPt { get; set; }
            public bool Bus { get; set; }
            public bool DeferredPayment { get; set; }

            public Ticket[] Tickets { get; set; }
        }

        public class Ticket
        {
            public string TicketId { get; set; }
            public string Seats { get; set; }
            public decimal Cost { get; set; }
            public string Tariff { get; set; }
            public string TariffName { get; set; }
            public string SeatsType { get; set; }
            public bool Teema { get; set; }
            public ResultPassenger[] Pass { get; set; }
        }

        public class ResultPassenger
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MidName { get; set; }
            public int DocType { get; set; }
            public string DocTypeName { get; set; }
            public string DocNumber { get; set; }
            public string BirthDate { get; set; }
            public int GenderId { get; set; }
            public int Insurance { get; set; }
        }

        public class PaymentSystem
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Tip { get; set; }
        }

        public class Request
        {
            public RequestPassenger[] Passengers { get; set; }
            public RequestOrder[] Orders { get; set; }
            public string ActorType { get; set; }

            internal Dictionary<string, string> ToDictionary()
            {
                return new Dictionary<string, string>
                {
                    ["journeyRequest"] = JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                    ["actorType"] = this.ActorType
                };
            }
        }

        public class RequestPassenger
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MidName { get; set; }
            public int Gender { get; set; }
            public string Birthdate { get; set; }
            public string Tariff { get; set; }
            public int DocType { get; set; }
            public string DocNumber { get; set; }
            public int Country { get; set; }
        }

        public class RequestOrder
        {
            public int Range0 { get; set; }
            public int Range1 { get; set; }
            public bool PlBedding { get; set; }
            public string PlUpdown { get; set; } // "du" - количество мест: d-нижних, u-верхних
            public string PlComp { get; set; }   // О - в одном отсеке, Б - в купейной части, К - в одном купе
            public int Dir { get; set; }
            public int Code0 { get; set; }
            public int Code1 { get; set; }
            public string Route0 { get; set; }
            public string Route1 { get; set; }
            public string Number { get; set; }
            public string Number2 { get; set; }
            public string Brand { get; set; }
            public string Letter { get; set; }
            public int Ctype { get; set; }
            public string Cnumber { get; set; }
            public string ClsType { get; set; }
            public bool ElReg { get; set; }
            public bool Ferry { get; set; }
            public string SeatType { get; set; }
            public int TicketPriceInPoints { get; set; }
            public string TrainType { get; set; }
            public bool ConferenceRoomFlag { get; set; }
            public int CarrierGroupId { get; set; }
            public string Datetime0 { get; set; }
            public int Teema { get; set; }
            public int CarVipFlag { get; set; }
        }
    }
}
