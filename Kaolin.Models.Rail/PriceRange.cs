namespace Kaolin.Models.Rail
{
    public class PriceRange
    {
        public decimal Min { get; internal set; }
        public decimal Max { get; internal set; }

        public PriceRange(decimal min, decimal max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString() => Min == Max ? $"[{Min}]" : $"[{Min}...{Max}]";
    }
}
