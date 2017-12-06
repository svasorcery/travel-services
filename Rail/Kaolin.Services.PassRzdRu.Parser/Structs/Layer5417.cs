namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Order status
    /// </summary>
    public class Layer5417
    {
        public string Result { get; set; }
        public string RID { get; set; }

        public OrderStatus[] Lst { get; set; }

        public class OrderStatus
        {
            public string N { get; set; }
            public string Status { get; set; }
            public string Name_ru { get; set; }
            public string Name_en { get; set; }
        }
    }
}
