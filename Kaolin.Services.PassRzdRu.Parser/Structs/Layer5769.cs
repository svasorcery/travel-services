namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Cancel reserve
    /// </summary>
    public class Layer5769
    {
        public string Result { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }

        public class Request
        {
            public string Type { get; set; }
            public string OrderId { get; set; }
            public string ActorType { get; set; }
        }
    }
}
