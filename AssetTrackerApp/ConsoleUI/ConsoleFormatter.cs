using AssetTrackerApp.Models;
using AssetTrackerApp.Services;
using System;

namespace AssetTrackerApp.ConsoleUI
{
    /// <summary>
    /// Formats and prints asset information to the console, including local currency conversion and age-based coloring.
    /// </summary>
    public static class ConsoleFormatter
    {
        // Expected lifespan of an asset in months (used to calculate color thresholds)
        private const int LifeSpanInMonths = 36;

        /// <summary>
        /// Prints a single asset to the console with color-coding based on remaining lifespan.
        /// </summary>
        /// <param name="asset">The asset to print.</param>
        public static void PrintAsset(Asset asset)
        {
            if (asset == null)
            {
                Console.WriteLine("⚠️  Cannot print null asset.");
                return;
            }

            // Calculate asset age and remaining lifespan
            int ageInMonths = (int)((DateTime.Now - asset.PurchaseDate).TotalDays / 30.4375); // Average month length
            int monthsLeft = LifeSpanInMonths - ageInMonths;

            // Convert price to local currency
            decimal localPrice = CurrencyConverter.ConvertTo(asset.Price.Amount, asset.Office.LocalCurrency, out decimal convertedAmount);
            string priceDisplay = $"{Math.Round(convertedAmount, 2)} {asset.Office.LocalCurrency}";

            // Apply color based on remaining lifespan
            if (monthsLeft <= 3)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (monthsLeft <= 6)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ResetColor();

            // Print formatted output
            Console.WriteLine($"{asset.Office.Name,-10} {asset.Type,-10} {asset.Brand,-10} {asset.Model,-15} " +
                              $"{asset.Price.Amount,8} {asset.Price.Currency,-4} " +
                              $"→ {priceDisplay,-12} " +
                              $"{asset.PurchaseDate:yyyy-MM-dd}");

            Console.ResetColor();
        }
    }
}
