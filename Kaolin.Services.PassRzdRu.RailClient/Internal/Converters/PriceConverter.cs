using Kaolin.Models.Rail;

namespace Kaolin.Services.PassRzdRu.RailClient.Internal.Converters
{
    public static class PriceConverter
    {
        public static decimal ToDecimal(string value)
        {
            return decimal.Parse(value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public static PriceRange ToPriceRange(string min, string max)
        {
            return new PriceRange(new Price(ToDecimal(min)), new Price(ToDecimal(max)));
        }
    }
}
