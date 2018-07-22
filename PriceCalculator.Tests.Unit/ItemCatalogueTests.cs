using System;
using NUnit.Framework;
using PriceCalculator.Exceptions;

namespace PriceCalculator.Tests.Unit
{
    [TestFixture]
    class ItemCatalogueTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void LookupPrice_WhenNullOrEmpty_ThrowsException(string item)
        {
            var sut = new ItemCatalogue();
            Assert.Throws<ArgumentNullException>(() => sut.LookupPrice(item));
        }

        [Test]
        public void LookupPrice_WhenNoSuchItem_ThrowsException()
        {
            var sut = new ItemCatalogue();
            Assert.Throws<NoSuchItemException>(() => sut.LookupPrice("No such item"));
        }

        [Test]
        public void LookupPrice_WhenBread_ReturnsPrice()
        {
            var sut = new ItemCatalogue();
            Assert.That(sut.LookupPrice("Bread"), Is.EqualTo(1.00m));
        }

        [Test]
        public void LookupPrice_WhenButter_ReturnsPrice()
        {
            var sut = new ItemCatalogue();
            Assert.That(sut.LookupPrice("Butter"), Is.EqualTo(0.80m));
        }

        [Test]
        public void LookupPrice_WhenMilk_ReturnsPrice()
        {
            var sut = new ItemCatalogue();
            Assert.That(sut.LookupPrice("Milk"), Is.EqualTo(1.15m));
        }
    }
}
