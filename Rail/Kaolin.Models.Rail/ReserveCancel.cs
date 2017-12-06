namespace Kaolin.Models.Rail
{
    public class ReserveCancel
    {
        public class Request
        {
            public string SessionId { get; set; }
        }

        public class Result
        {
            public string OrderId { get; set; }
            public string Code { get; set; }
            public string Status { get; set; }
        }
    }
}
