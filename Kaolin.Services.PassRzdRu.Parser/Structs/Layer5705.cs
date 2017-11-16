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
        public string TotalSum { get; set; }
        public ResultOrder[] Orders { get; set; }
        public PaymentSystem[] PaymentSystems { get; set; }
        public int PayTime { get; set; }
        public string DefShowTime { get; set; }

        public class ResultOrder
        {
            public string OrderId { get; set; }
            public string Created { get; set; }
            public decimal Cost { get; set; }
            public decimal TotalCostPt { get; set; }
            public string TimeInWay { get; set; }

            public string Code0 { get; set; }
            public string Station0 { get; set; }
            public string Route0 { get; set; }
            public bool Msk0 { get; set; }
            public string Date0 { get; set; }
            public string Time0 { get; set; }
            public string TrDate0 { get; set; }
            public string TrTime0 { get; set; }
            
            public string Code1 { get; set; }
            public string Station1 { get; set; }
            public string Route1 { get; set; }
            public bool Msk1 { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }
            public string LocalDate1 { get; set; }
            public string LocalTime1 { get; set; }
            public string TimeDeltaString1 { get; set; }
            public string MskTimeSuffix { get; set; }

            public string DirName { get; set; }
            public string Number { get; set; }
            public string Number2 { get; set; }
            public string CNumber { get; set; }
            public string SeatNums { get; set; }
            public string AddSigns { get; set; }
            public string ClsType { get; set; }
            public string Type { get; set; }
            public int Ctype { get; set; }
            public string TimeInfo { get; set; }
            public string Carrier { get; set; }
            public string Agent { get; set; }
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
            public bool AddFood { get; set; }
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
            public int Insurance { get; set; } // TODO: add insurance, ref #49
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

            internal Dictionary<string, string> ToDictionary()
            {
                return new Dictionary<string, string>
                {
                    ["journeyRequest"] = JsonConvert.SerializeObject(
                        this, 
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    ),
                    ["actorType"] = "desktop_2016"
                };
            }
        }

        public class RequestPassenger
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MidName { get; set; }
            public Gender Gender { get; set; }
            public string Birthdate { get; set; }
            public string Tariff { get; set; }
            public DocumentTypes DocType { get; set; }
            public string DocNumber { get; set; }
            public int Country { get; set; }
        }

        public enum Gender
        {
            FEMALE = 1,
            MALE = 2
        }

        public enum DocumentTypes
        {
            /// <summary>
            /// Общегражданский паспорт РФ
            /// </summary>
            PN = 1,
            /// <summary>
            /// Паспорт формы СССР
            /// </summary>
            PS = 2,
            /// <summary>
            /// Общегражданский заграничный паспорт РФ
            /// </summary>
            PSP = 3,
            /// <summary>
            /// Национальный паспорт 
            /// </summary>
            NP = 4,
            /// <summary>
            /// Паспорт моряка
            /// </summary>
            PM = 5,
            /// <summary>
            /// Свидетельство о рождении
            /// </summary>
            SR = 6,
            /// <summary>
            /// Военный билет
            /// </summary>
            VB = 7,
            /// <summary>
            /// Вид на жительство
            /// </summary>
            VZ = 11,
            /// <summary>
            /// Временное удостоверение личности
            /// </summary>
            VUL,
            /// <summary>
            /// Дипломатический паспорт
            /// </summary>
            DP,
            /// <summary>
            /// Свидетельство на возвращение в страны снг 
            /// </summary>
            SVV,
            /// <summary>
            /// Служебный паспорт 
            /// </summary>
            SP,
            /// <summary>
            /// Справка об освобождении из мест лишения свободы 
            /// </summary>
            SPO,
            /// <summary>
            /// Справка об утере паспорта 
            /// </summary>
            SPU
        }

        public class RequestOrder
        {
            public int Range0 { get; set; }
            public int Range1 { get; set; }
            public bool PlBedding { get; set; }
            /// <summary>
            /// "du" - количество мест: d-нижних, u-верхних
            /// </summary>
            public string PlUpdown { get; set; }
            /// <summary>
            /// О - в одном отсеке, Б - в купейной части, К - в одном купе
            /// </summary>
            public string PlComp { get; set; }
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
