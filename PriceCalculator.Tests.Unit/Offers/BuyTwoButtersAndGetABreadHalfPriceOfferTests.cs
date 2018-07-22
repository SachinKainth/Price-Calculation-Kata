using System.Collections.Generic;
using NUnit.Framework;
using PriceCalculator.Offers;

namespace PriceCalculator.Tests.Unit.Offers
{
    [TestFixture]
    class BuyTwoButtersAndGetABreadHalfPriceOfferTests
    {
        [Test]
        public void TimesApplicable_WhenOneButterAndOneBread_ThereIsNoDiscount()
        {
            var items = new List<string> { "Butter", "Bread" };

            var sut = new BuyTwoButtersAndGetABreadHalfPriceOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(0));
        }

        [Test]
        public void TimesApplicable_WhenTwoButtersAndOneBread_ThereIsOneDiscount()
        {
            var items = new List<string> { "Butter", "Butter", "Bread" };

            var sut = new BuyTwoButtersAndGetABreadHalfPriceOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(1));
        }

        [Test]
        public void TimesApplicable_WhenTwoButtersAndNoBread_ThereIsNoDiscount()
        {
            var items = new List<string> { "Butter", "Butter" };

            var sut = new BuyTwoButtersAndGetABreadHalfPriceOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(0));
        }

        [Test]
        public void TimesApplicable_WhenTwoButtersAndTwoBreads_ThereIsOneDiscount()
        {
            var items = new List<string> { "Butter", "Butter", "Bread", "Bread" };

            var sut = new BuyTwoButtersAndGetABreadHalfPriceOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(1));
        }

        [Test]
        public void TimesApplicable_WhenFourButtersAndTwoBreads_ThereAreTwoDiscounts()
        {
            var items = new List<string> { "Butter", "Butter", "Butter", "Butter", "Bread", "Bread" };

            var sut = new BuyTwoButtersAndGetABreadHalfPriceOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(2));
        }

        [Test]
        public void DiscountItemAndProprotion_ReturnsResult()
        {
            var sut = new BuyTwoButtersAndGetABreadHalfPriceOffer();

            Assert.That(sut.DiscountItemAndProportion(), Is.EqualTo(new DiscountItemAndProportion("Bread", 0.5m)));
        }
    }
}
