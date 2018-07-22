namespace PriceCalculator
{
    public struct DiscountItemAndProportion
    {
        public string Item { get; }
        public decimal Proportion { get; }

        public DiscountItemAndProportion(string item, decimal proportion)
        {
            Item = item;
            Proportion = proportion;
        }
    }
}