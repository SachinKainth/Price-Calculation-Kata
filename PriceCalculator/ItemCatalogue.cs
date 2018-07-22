using System;
using System.Collections.Generic;
using PriceCalculator.Exceptions;

namespace PriceCalculator
{
    public  class ItemCatalogue : IItemCatalogue
    {
        private readonly Dictionary<string, decimal> _itemPrices = new Dictionary<string, decimal>
        {
            {"Milk", 1.15m},
            {"Bread", 1.00m},
            {"Butter", 0.80m},
        };

        public decimal LookupPrice(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentNullException();
            }

            if (!_itemPrices.ContainsKey(item))
            {
                throw new NoSuchItemException();
            }

            return _itemPrices[item];
        }
    }
}