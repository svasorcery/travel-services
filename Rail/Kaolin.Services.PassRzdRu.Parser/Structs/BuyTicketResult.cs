namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    /// <summary>
    /// Buy ticket
    /// </summary>
    public class BuyTicketResult
    {
        public string SaleOrderId { get; set; }

        public string OrderId { get; set; }

        public decimal TransactionTotal { get; set; }
    }
}
