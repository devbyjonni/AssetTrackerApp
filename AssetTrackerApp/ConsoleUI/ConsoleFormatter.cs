using AssetTrackerApp.Models;
using AssetTrackerApp.Services;
using System;

namespace AssetTrackerApp.ConsoleUI
{
    public static class ConsoleFormatter
    {
        private const int LifeSpanInMonths = 36;

        public static void PrintAsset(Asset asset)
        {
            int ageInMonths = (int)((DateTime.Now - asset.PurchaseDate).TotalDays / 30.4375); // Approx months
            int monthsLeft = LifeSpanInMonths - ageInMonths;

            // Calculate converted price
            decimal localPrice = CurrencyConverter.ConvertTo(asset.Price.Amount, asset.Office.LocalCurrency, out decimal converted);
            string priceDisplay = $"{Math.Round(converted, 2)} {asset.Office.LocalCurrency}";

            // Determine color
            if (monthsLeft <= 3)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (monthsLeft <= 6)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ResetColor();

            // Output the asset
            Console.WriteLine($"{asset.Office.Name,-10} {asset.Type,-10} {asset.Brand,-10} {asset.Model,-15} " +
                              $"{asset.Price.Amount,8} {asset.Price.Currency,-4} " +
                              $"→ {priceDisplay,-12} " +
                              $"{asset.PurchaseDate:yyyy-MM-dd}");

            Console.ResetColor();
        }
    }
}
