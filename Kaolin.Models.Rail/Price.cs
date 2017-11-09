using System.Collections.Generic;
using System.Linq;

namespace Kaolin.Models.Rail
{
    public class Price
    {
        public decimal Base { get; protected set; }
        public Charges Charges { get; protected set; }
        public decimal Total => Base; // TODO: Add sum with extra charges, ref #51

        public Price(decimal providerPrice, Charges charges)
        {
            Base = providerPrice;
            Charges = charges;
        }
    }

    public class Charges
    {
        public IEnumerable<Charge> Included { get; set; }
        // TODO: Add extra charges here, ref #51
    }

    public class Charge
    {
        // TODO: Add Type
        public decimal Amount { get; set; }
    }
}
