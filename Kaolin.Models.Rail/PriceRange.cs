namespace Kaolin.Models.Rail
{
    public class PriceRange
    {
        public Price Min { get; internal set; }
        public Price Max { get; internal set; }

        public PriceRange(Price min, Price max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString() => Min == Max ? $"{Min.Total}" : $"{Min.Total}...{Max.Total}";
    }
}
