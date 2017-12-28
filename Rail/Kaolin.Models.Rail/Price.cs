using System.Collections.Generic;
using System.Linq;

namespace Kaolin.Models.Rail
{
    public class Price
    {
        public decimal Base { get; protected set; }
        public Charges Charges { get; protected set; }
        public decimal Total => Base + (Charges?.Extra != null ? Charges.Extra.Sum(x => x.Amount) : 0m);

        public Price(decimal providerPrice, Charges charges = null)
        {
            Base = providerPrice;
            Charges = charges;
        }
    }

    public class Charges
    {
        public IEnumerable<Charge> Included { get; set; }
        public IEnumerable<Charge> Extra { get; set; }
    }

    public class Charge
    {
        // TODO: Add Type
        public decimal Amount { get; set; }

        public Charge(decimal amount)
        {
            Amount = amount;
        }
    }
}
