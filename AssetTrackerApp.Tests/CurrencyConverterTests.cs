using Xunit;
using AssetTrackerApp.Services;
using AssetTrackerApp.Models;

namespace AssetTrackerApp.Tests
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void Convert_SameCurrency_ReturnsSameValue()
        {
            decimal amount = 123.45m;
            decimal converted = CurrencyConverter.Convert(amount, Currency.USD, Currency.USD, out decimal result);
            Assert.Equal(amount, converted);
            Assert.Equal(amount, result);
        }
    }
}
