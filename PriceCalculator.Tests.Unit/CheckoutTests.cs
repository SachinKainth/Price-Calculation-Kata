using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace PriceCalculator.Tests.Unit
{
    [TestFixture]
    class CheckoutTests
    {
        private readonly Mock<IItemCatalogue> _itemCatalogue = new Mock<IItemCatalogue>();
        private readonly Mock<IBasket> _basket = new Mock<IBasket>();
        private readonly Mock<IOffersService> _offersService = new Mock<IOffersService>();

        private const string Item1 = "item1";
        private const string Item2 = "item2";
        private const decimal Item1Price = 25m;
        private const decimal Item2Price = 40m;
        private const decimal TotalDiscount = -15m;

        [Test]
        public void Calculate_WhenNoItems_ReturnsZero()
        {
            _offersService.Setup(_ => _.GetAllDiscounts(new List<string>(), _itemCatalogue.Object)).Returns(0);

            var sut = new Checkout(_itemCatalogue.Object, _offersService.Object);

            Assert.That(sut.Calculate(_basket.Object), Is.EqualTo(0));
        }

        [Test]
        public void Calculate_WhenItemsButNoDiscounts_ReturnsTotal()
        {
            var scannedItems = new List<string> {Item1, Item2};

            _itemCatalogue.Setup(_ => _.LookupPrice(Item1)).Returns(Item1Price);
            _itemCatalogue.Setup(_ => _.LookupPrice(Item2)).Returns(Item2Price);
            _offersService.Setup(_ => _.GetAllDiscounts(scannedItems, _itemCatalogue.Object)).Returns(0);

            _basket.SetupSequence(_ => _.Take())
                .Returns(Item1)
                .Returns(Item2)
                .Returns((string) null);

            var sut = new Checkout(_itemCatalogue.Object, _offersService.Object);

            Assert.That(sut.Calculate(_basket.Object), Is.EqualTo(Item1Price + Item2Price));
        }

        [Test]
        public void Calculate_WhenItemsAndDiscounts_ReturnsTotal()
        {
            var scannedItems = new List<string> { Item1, Item2 };
            _itemCatalogue.Setup(_ => _.LookupPrice(Item1)).Returns(Item1Price);
            _itemCatalogue.Setup(_ => _.LookupPrice(Item2)).Returns(Item2Price);
            _offersService.Setup(_ => _.GetAllDiscounts(scannedItems, _itemCatalogue.Object)).Returns(TotalDiscount);
            _basket.SetupSequence(_ => _.Take())
                .Returns(Item1)
                .Returns(Item2)
                .Returns((string)null);

            var sut = new Checkout(_itemCatalogue.Object, _offersService.Object);

            Assert.That(sut.Calculate(_basket.Object), Is.EqualTo(Item1Price + Item2Price + TotalDiscount));
        }
    }
}
