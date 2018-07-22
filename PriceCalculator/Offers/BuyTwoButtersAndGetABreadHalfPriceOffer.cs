using System;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Offers
{
    public class BuyTwoButtersAndGetABreadHalfPriceOffer : IOffer
    {
        public int TimesApplicable(IList<string> scannedItems)
        {
            return Math.Min(
                scannedItems.Count(_ => _ == "Butter") / 2, 
                scannedItems.Count(_ => _ == "Bread"));
        }

        public DiscountItemAndProportion DiscountItemAndProportion()
        {
            return new DiscountItemAndProportion("Bread", 0.5m);
        }
    }
}