using System;
using Xunit;
using AssetTrackerApp.Models;

namespace AssetTrackerApp.Tests
{
    public class AssetAgeTests
    {
        [Fact]
        public void AgeInMonths_ReturnsExpectedValue()
        {
            var office = new Office("Test", Currency.USD);
            var price = new Price(1, Currency.USD);
            var purchaseDate = DateTime.Now.AddMonths(-5);
            var asset = new Computer(price, purchaseDate, "Brand", "Model", office);

            int expected = (int)((DateTime.Now - purchaseDate).TotalDays / 30.4375);
            Assert.Equal(expected, asset.AgeInMonths);
        }
    }
}
