using System.Collections.Generic;
using NUnit.Framework;
using PriceCalculator.Offers;

namespace PriceCalculator.Tests.Unit.Offers
{
    [TestFixture]
    class BuyThreeMilksGetFourthFreeOfferTests
    {
        [Test]
        public void TimesApplicable_WhenThreeMilks_ThereIsNoDiscount()
        {
            var items = new List<string> { "Milk", "Milk", "Milk" };

            var sut = new BuyThreeMilksGetFourthFreeOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(0));
        }

        [Test]
        public void TimesApplicable_WhenFourMilks_ThereIsOneDiscount()
        {
            var items = new List<string> { "Milk", "Milk", "Milk", "Milk" };

            var sut = new BuyThreeMilksGetFourthFreeOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(1));
        }

        [Test]
        public void TimesApplicable_WhenSevenMilksTwoBreadsAndThreeButters_ThereIsOneDiscount()
        {
            var items = new List<string> { "Milk", "Milk", "Milk", "Milk", "Milk", "Milk", "Milk", "Bread", "Bread", "Butter", "Butter", "Butter" };

            var sut = new BuyThreeMilksGetFourthFreeOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(1));
        }

        [Test]
        public void TimesApplicable_WhenEightMilks_ThereAreTwoDiscounts()
        {
            var items = new List<string> { "Milk", "Milk", "Milk", "Milk", "Milk", "Milk", "Milk", "Milk" };

            var sut = new BuyThreeMilksGetFourthFreeOffer();

            Assert.That(sut.TimesApplicable(items), Is.EqualTo(2));
        }

        [Test]
        public void DiscountItemAndProprotion_ReturnsResult()
        {
            var sut = new BuyThreeMilksGetFourthFreeOffer();

            Assert.That(sut.DiscountItemAndProportion(), Is.EqualTo(new DiscountItemAndProportion("Milk", 1m)));
        }
    }
}
