namespace PriceCalculator
{
    public interface IItemCatalogue
    {
        decimal LookupPrice(string item);
    }
}