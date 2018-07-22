using System.Collections.Generic;

namespace PriceCalculator.Offers
{
    public interface IOffer
    {
        int TimesApplicable(IList<string> scannedItems);
        DiscountItemAndProportion DiscountItemAndProportion();
    }
}
