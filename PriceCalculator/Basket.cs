using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator
{
    public class Basket : IBasket
    {
        private readonly Stack<string> _items = new Stack<string>();

        public string Take()
        {
            return _items.Any() ? _items.Pop() : null;
        }

        public void Add(string item)
        {
            _items.Push(item);
        }
    }
}