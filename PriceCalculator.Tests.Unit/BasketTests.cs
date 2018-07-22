using NUnit.Framework;

namespace PriceCalculator.Tests.Unit
{
    [TestFixture]
    class BasketTests
    {
        [Test]
        public void Take_WhenBasketEmpty_ReturnsNull()
        {
            var sut = new Basket();

            Assert.That(sut.Take(), Is.Null);
        }

        [Test]
        public void Take_WhenOneItemInBasket_ReturnsItem()
        {
            const string item = "something";

            var sut = new Basket();
            sut.Add(item);

            Assert.That(sut.Take(), Is.EqualTo(item));
            Assert.That(sut.Take(), Is.Null);
        }

        [Test]
        public void Take_WhenMultipleItemsInBasket_ReturnsThemAll()
        {
            const string item1 = "something 1";
            const string item2 = "something 2";

            var sut = new Basket();
            sut.Add(item1);
            sut.Add(item2);

            Assert.That(sut.Take(), Is.EqualTo(item2));
            Assert.That(sut.Take(), Is.EqualTo(item1));
            Assert.That(sut.Take(), Is.Null);
        }
    }
}
