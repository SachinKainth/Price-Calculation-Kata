using System.Collections.Generic;

namespace PriceCalculator
{
    public class Checkout
    {
        private readonly IItemCatalogue _itemCatalogue;
        private readonly IOffersService _offersService;

        public Checkout(IItemCatalogue itemCatalogue, IOffersService offersService)
        {
            _itemCatalogue = itemCatalogue;
            _offersService = offersService;
        }

        public decimal Calculate(IBasket basket)
        {
            var scannedItems = new List<string>();
            decimal total = 0;

            for (var item = basket.Take(); item != null; item = basket.Take())
            {
                total += CalculateItem(item);
                scannedItems.Add(item);
            }

            var discounts = _offersService.GetAllDiscounts(scannedItems, _itemCatalogue);

            return total + discounts;
        }

        private decimal CalculateItem(string item)
        {
            var itemPrice = _itemCatalogue.LookupPrice(item);
            return itemPrice;
        }
    }
}