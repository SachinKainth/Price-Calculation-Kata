using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PriceCalculator.Offers;

namespace PriceCalculator.Tests.Unit
{
    [TestFixture]
    class OffersServiceTests
    {
        private const int Offer1TimesApplicable = 1;
        private const int Offer2TimesApplicable = 4;
        private const decimal Offer1Proportion = 0.2m;
        private const decimal Offer2Proportion = 0.5m;
        private const decimal Item1Price = 1.5m;
        private const decimal Item2Price = 2m;

        [Test]
        public void AddOffer_NullOffer_ThrowsException()
        {
            var sut = new OffersService();

            Assert.Throws<ArgumentNullException>(() => sut.AddOffer(null));
        }

        [Test]
        public void AddOffer_OfferAddedForTheFirstTime_DoesNotThrowException()
        {
            var offer = new Mock<IOffer>();

            var sut = new OffersService();

            Assert.DoesNotThrow(() => sut.AddOffer(offer.Object));
        }

        [Test]
        public void AddOffer_TwoDifferentOffersAdded_DoesNotThrowException()
        {
            var offer1 = new Mock<IOffer>();
            var offer2 = new Mock<IOffer>();

            var sut = new OffersService();

            Assert.DoesNotThrow(() => sut.AddOffer(offer1.Object));
            Assert.DoesNotThrow(() => sut.AddOffer(offer2.Object));
        }

        [Test]
        public void GetAllDiscounts_NoOffers_ReturnsZero()
        {
            var sut = new OffersService();

            Assert.AreEqual(-0m, sut.GetAllDiscounts(null, null));
        }

        [Test]
        public void GetAllDiscounts_OneOfferApplicableOnce_ReturnsDiscount()
        {
            var scannedItems = new Mock<IList<string>>();

            var itemCatalogue = new Mock<IItemCatalogue>();
            itemCatalogue.Setup(_ => _.LookupPrice(It.IsAny<string>())).Returns(Item1Price);

            var offer = new Mock<IOffer>();
            offer.Setup(_ => _.DiscountItemAndProportion()).Returns(new DiscountItemAndProportion(It.IsAny<string>(), Offer1Proportion));
            offer.Setup(_ => _.TimesApplicable(scannedItems.Object)).Returns(Offer1TimesApplicable);

            var sut = new OffersService();

            sut.AddOffer(offer.Object);

            Assert.AreEqual(-(Item1Price*Offer1Proportion*Offer1TimesApplicable), sut.GetAllDiscounts(scannedItems.Object, itemCatalogue.Object));
        }

        [Test]
        public void GetAllDiscounts_OneInapplicableOffer_ReturnsZero()
        {
            var scannedItems = new Mock<IList<string>>();

            var itemCatalogue = new Mock<IItemCatalogue>();

            var offer = new Mock<IOffer>();
            offer.Setup(_ => _.TimesApplicable(scannedItems.Object)).Returns(0);

            var sut = new OffersService();

            sut.AddOffer(offer.Object);

            Assert.AreEqual(-0m, sut.GetAllDiscounts(scannedItems.Object, itemCatalogue.Object));
        }

        [Test]
        public void GetAllDiscounts_OneInapplicableOfferOneApplicableOffer_ReturnsDiscount()
        {
            var scannedItems = new Mock<IList<string>>();

            var itemCatalogue = new Mock<IItemCatalogue>();
            itemCatalogue.Setup(_ => _.LookupPrice(It.IsAny<string>())).Returns(Item2Price);

            var offer1 = new Mock<IOffer>();
            offer1.Setup(_ => _.TimesApplicable(scannedItems.Object)).Returns(0);

            var offer2 = new Mock<IOffer>();
            offer2.Setup(_ => _.DiscountItemAndProportion()).Returns(new DiscountItemAndProportion(It.IsAny<string>(), Offer2Proportion));
            offer2.Setup(_ => _.TimesApplicable(scannedItems.Object)).Returns(Offer2TimesApplicable);

            var sut = new OffersService();

            sut.AddOffer(offer1.Object);
            sut.AddOffer(offer2.Object);

            Assert.AreEqual(-(Item2Price * Offer2Proportion * Offer2TimesApplicable), sut.GetAllDiscounts(scannedItems.Object, itemCatalogue.Object));
        }

        [Test]
        public void GetAllDiscounts_TwoApplicableOffers_ReturnsDiscount()
        {
            var scannedItems = new Mock<IList<string>>();

            var itemCatalogue = new Mock<IItemCatalogue>();
            itemCatalogue.SetupSequence(_ => _.LookupPrice(It.IsAny<string>()))
                .Returns(Item1Price)
                .Returns(Item2Price);

            var offer1 = new Mock<IOffer>();
            offer1.Setup(_ => _.DiscountItemAndProportion()).Returns(new DiscountItemAndProportion(It.IsAny<string>(), Offer1Proportion));
            offer1.Setup(_ => _.TimesApplicable(scannedItems.Object)).Returns(Offer1TimesApplicable);

            var offer2 = new Mock<IOffer>();
            offer2.Setup(_ => _.DiscountItemAndProportion()).Returns(new DiscountItemAndProportion(It.IsAny<string>(), Offer2Proportion));
            offer2.Setup(_ => _.TimesApplicable(scannedItems.Object)).Returns(Offer2TimesApplicable);

            var sut = new OffersService();

            sut.AddOffer(offer1.Object);
            sut.AddOffer(offer2.Object);

            Assert.AreEqual(
                -(Item1Price * Offer1Proportion * Offer1TimesApplicable + Item2Price * Offer2Proportion * Offer2TimesApplicable), 
                sut.GetAllDiscounts(scannedItems.Object, itemCatalogue.Object));
        }
    }
}
