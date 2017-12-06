namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Payment
    /// </summary>
    public class Layer5734
    {
        public string Result { get; set; }
        public string Timestamp { get; set; }

        public string Url { get; set; }
        public PaymentOptions BankExit { get; set; }

        public class PaymentOptions
        {
            public string Ok { get; set; }
            public string Cancel { get; set; }
            public string Decline { get; set; }
        }

        public class Request
        {
            public string Type { get; set; }
            public string OrderId { get; set; }
            public string ActorType { get; set; }
        }
    }
}
