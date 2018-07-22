using NUnit.Framework;
using PriceCalculator.Offers;
using TechTalk.SpecFlow;

namespace PriceCalculator.Tests.Acceptance.Steps
{
    [Binding]
    public class PriceCalculatorSteps
    {
        private readonly IBasket _basket = new Basket();
        private decimal Total { get; set; }

        [Given(@"I have added '(.*)' '(.*)' to the basket")]
        public void GivenIHaveAddedNInstancesOfAnItemToTheBasket(int number, string item)
        {
            for (var i = 0; i < number; i++)
            {
                _basket.Add(item);
            }
        }

        [When(@"I calculate the total")]
        public void WhenICalculateTheTotal()
        {
            IOffersService offersService = new OffersService();
            offersService.AddOffer(new BuyThreeMilksGetFourthFreeOffer());
            offersService.AddOffer(new BuyTwoButtersAndGetABreadHalfPriceOffer());

            var checkout = new Checkout(new ItemCatalogue(), offersService);

            Total = checkout.Calculate(_basket);
        }

        [Then(@"the total should be '(.*)'")]
        public void ThenTheTotalShouldBe(decimal expectedTotal)
        {
            Assert.That(expectedTotal, Is.EqualTo(Total));
        }
    }
}
