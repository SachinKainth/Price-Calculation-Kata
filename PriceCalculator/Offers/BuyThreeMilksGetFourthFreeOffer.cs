using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Offers
{
    public class BuyThreeMilksGetFourthFreeOffer : IOffer
    {
        public int TimesApplicable(IList<string> scannedItems)
        {
            return scannedItems.Count(_ => _ == "Milk") / 4;
        }

        public DiscountItemAndProportion DiscountItemAndProportion()
        {
            return new DiscountItemAndProportion("Milk", 1m);
        }
    }
}
