namespace Kaolin.Models.Rail
{
    public abstract class FeeType
    {
        public decimal Amount { get; protected set; }

        public abstract decimal Calculate(decimal price);

        public Charge ToCharge(decimal price) => new Charge(Calculate(price));
    }


    public class ZeroFeeType : FeeType
    {
        public ZeroFeeType()
        {

        }

        public override decimal Calculate(decimal price) => 0m;
    }

    public class FixedFeeType : FeeType
    {
        public FixedFeeType(decimal amount)
        {
            Amount = amount;
        }

        public override decimal Calculate(decimal price) => Amount;
    }

    public class PercentFeeType : FeeType
    {
        public PercentFeeType(decimal amount)
        {
            Amount = amount;
        }

        public override decimal Calculate(decimal price)
        {
            return price * Amount / 100m;
        }
    }
}
