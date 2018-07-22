using System.Collections.Generic;
using PriceCalculator.Offers;

namespace PriceCalculator
{
    public interface IOffersService
    {
        decimal GetAllDiscounts(IList<string> scannedItems, IItemCatalogue itemCatalogue);
        void AddOffer(IOffer offer);
    }
}