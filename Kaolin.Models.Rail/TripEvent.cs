using System;
using System.Linq;

namespace Kaolin.Models.Rail
{
    public class TripEvent
    {
        public DateTime DateAndTime { get; set; }
        public TimeType TimeType { get; set; }
        public RailStation Station { get; set; }

        public TripEvent()
        {

        }

        public TripEvent(string date, string time, TimeType timeType, string station, string code)
        {
            var dateParts = date.Split('.').Select(x => int.Parse(x));
            var timeParts = time.Split(':').Select(x => int.Parse(x));

            DateAndTime = new DateTime(
                dateParts.ElementAt(2), dateParts.ElementAt(1), dateParts.ElementAt(0), 
                timeParts.ElementAt(0), timeParts.ElementAt(1), timeParts.ElementAtOrDefault(2));
            TimeType = timeType;
            Station = new RailStation(station, code);
        }

        public TripEvent(DateTime dateAndTime, TimeType timeType, string station, string code)
        {
            DateAndTime = dateAndTime;
            TimeType = timeType;
            Station = new RailStation(station, code);
        }

        public string GetDateString()
        {
            return DateAndTime.ToLocalTime().ToString("dd.MM.yyyy");
        }

        public string GetTimeString(bool shorten = true)
        {
            return shorten ? DateAndTime.ToLocalTime().ToString("HH:mm") : DateAndTime.ToLocalTime().ToString("HH:mm:ss");
        }

        public string GetDateTimeString()
        {
            return $"{GetDateString()} {GetTimeString(shorten: true)}";
        }
    }

    public enum TimeType
    {
        MOSCOW = 0,
        LOCAL = 1,
        GMT = 2
    }

    public class RailStation
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public RailStation(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
