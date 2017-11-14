using Kaolin.Models.Rail;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    public class PriceConverter
    {
        public decimal ToDecimal(string value)
        {
            return decimal.Parse(value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public Price ToPrice(string value)
        {
            return new Price(ToDecimal(value));
        }

        public PriceRange ToPriceRange(string min, string max = null)
        {
            var minPrice = ToPrice(min);
            var maxPrice = ToPrice(System.String.IsNullOrEmpty(max) ? min : max);
            return new PriceRange(minPrice, maxPrice);
        }
    }
}
