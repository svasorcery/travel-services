using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Medical policy cost
    /// </summary>
    public class Layer5887 : IRidRequestResponse
    {
        public string Result { get; set; }
        public string RID { get; set; }
        public string Timestamp { get; set; }
        public MedicalPolicy Response { get; set; }

        public class MedicalPolicy
        {
            public int AreaId { get; set; }
            public decimal Cost { get; set; }
            public string StartDate { get; set; }
            public string FinishDate { get; set; }
        }


        public class Request
        {
            public string BirthDate { get; set; }
            public string StationCode { get; set; }
            public string ForwardDepartureDate { get; set; }
            public string ForwardArrivalDate { get; set; }
            public string BackwardDepartureDate { get; set; }
            public string BackwardArrivalDate { get; set; }
            
            internal Dictionary<string, string> ToDictionary()
            {
                return new Dictionary<string, string>
                {
                    ["jsonRequest"] = JsonConvert.SerializeObject(
                        this,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    )
                };
            }
        }
    }
}
