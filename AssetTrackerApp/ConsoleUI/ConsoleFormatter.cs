using AssetTrackerApp.Models;
using AssetTrackerApp.Services;
using System;
using System.IO;

namespace AssetTrackerApp.ConsoleUI
{
    public static class ConsoleFormatter
    {
        private const int LifeSpanInMonths = 36;

        /// <summary>
        /// Prints the details of an asset using the provided TextWriter. 
        /// If no writer is provided, it defaults to Console.Out, allowing for both production output and easy testing.
        /// </summary>
        public static void PrintAsset(Asset asset, TextWriter? writer = null)
        {
            // Ensure we have a valid output stream.
            // If 'writer' is null, default to Console.Out so we can safely write output.
            writer ??= Console.Out;

            if (asset == null)
            {
                writer.WriteLine("⚠️  Cannot print null asset.");
                return;
            }

            // Calculate age
            int ageInMonths = (int)((DateTime.Now - asset.PurchaseDate).TotalDays / 30.4375);
            int monthsLeft = LifeSpanInMonths - ageInMonths;

            // Convert price from the asset's currency to the office's local currency
            decimal localPrice = CurrencyConverter.Convert(
                asset.Price.Amount,
                asset.Price.Currency,
                asset.Office.LocalCurrency,
                out decimal convertedAmount);
            string priceDisplay = $"{Math.Round(convertedAmount, 2)} {asset.Office.LocalCurrency}";

            // If writing to console, apply color
            if (writer == Console.Out)
            {
                if (monthsLeft <= 3) Console.ForegroundColor = ConsoleColor.Red;
                else if (monthsLeft <= 6) Console.ForegroundColor = ConsoleColor.Yellow;
            }

            // Build the line with all left-aligned fields.
            // Matches the header widths:
            //  Office:10, Type:10, Brand:10, Model:15, Amount:8, Curr:4, Converted:12, Date:10
            string format = $"{{0,-{AssetTableHeader.OfficeWidth}}} " +
                $"{{1,-{AssetTableHeader.TypeWidth}}} " +
                $"{{2,-{AssetTableHeader.BrandWidth}}} " +
                $"{{3,-{AssetTableHeader.ModelWidth}}} " +
                $"{{4,-{AssetTableHeader.AmountWidth}}} " +
                $"{{5,-{AssetTableHeader.CurrWidth}}} " +
                $"{{6,-{AssetTableHeader.ConvertedWidth}}} " +
                $"{{7,-{AssetTableHeader.DateWidth}:yyyy-MM-dd}}";

            string formattedLine = string.Format(
                format,
                asset.Office.Name,
                asset.Type,
                asset.Brand,
                asset.Model,
                Math.Round(asset.Price.Amount, 2),
                asset.Price.Currency,
                priceDisplay,
                asset.PurchaseDate
            );

            writer.WriteLine(formattedLine);

            // Reset color if writing to console
            if (writer == Console.Out)
            {
                Console.ResetColor();
            }
        }
    }
}