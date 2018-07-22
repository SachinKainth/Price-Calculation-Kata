using System;
using System.Collections.Generic;
using PriceCalculator.Offers;

namespace PriceCalculator
{
    public class OffersService : IOffersService
    {
        private readonly IList<IOffer> _currentOffers;

        public OffersService()
        {
            _currentOffers = new List<IOffer>();
        }

        public decimal GetAllDiscounts(IList<string> scannedItems, IItemCatalogue itemCatalogue)
        {
            var discounts = 0m;
            foreach (var offer in _currentOffers)
            {
                var discountAndProportion = offer.DiscountItemAndProportion();
                var timesApplicable = offer.TimesApplicable(scannedItems);

                var itemPrice = itemCatalogue.LookupPrice(discountAndProportion.Item);
                var priceProportion = discountAndProportion.Proportion;

                discounts += itemPrice * priceProportion * timesApplicable;
            }

            return -discounts;
        }

        public void AddOffer(IOffer offer)
        {
            if (offer == null)
            {
                throw new ArgumentNullException();
            }

            _currentOffers.Add(offer);
        }
    }
}