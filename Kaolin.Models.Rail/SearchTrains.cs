using System;
using System.Collections.Generic;

namespace Kaolin.Models.Rail
{
    public class SearchTrains
    {
        public class Request
        {
            public string From { get; set; }
            public string To { get; set; }
            public DateTime DepartDate { get; set; }
            public int? HourFrom { get; set; }
            public int? HourTo { get; set; }

            public string DepartDateString => DepartDate.ToString("dd-MM-yyyy");
        }

        public class Result
        {
            public string Origin { get; set; }
            public string OriginCode { get; set; }
            public string Destination { get; set; }
            public string DestinationCode { get; set; }
            public DateTime DepartureDate { get; set; }
            public TimeType TimeType { get; set; }
            public bool? NoFreeTickets { get; set; }
            public IEnumerable<Train> Trains { get; set; }


            public class Train : TrainInfo
            {
                public IEnumerable<Car> Cars { get; set; }
            }

            public class Car
            {
                public string Type { get; set; }
                public string ServiceClass { get; set; }
                public int FreeSeats { get; set; }
                public decimal MinPrice { get; set; }
                public int? BonusPoints { get; set; }
            }
        }
    }
}
