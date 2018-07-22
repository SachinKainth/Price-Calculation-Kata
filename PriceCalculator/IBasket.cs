namespace PriceCalculator
{
    public interface IBasket
    {
        string Take();
        void Add(string item);
    }
}
