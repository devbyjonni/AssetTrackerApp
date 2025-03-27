using System;
using AssetTrackerApp.Models;
using AssetTrackerApp.Services;
using AssetTrackerApp.Data;
using AssetTrackerApp.ConsoleUI;

namespace AssetTrackerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Attempt to fetch currency rates
            try
            {
                var latestRates = CurrencyConverter.UpdateRates();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Currency rates successfully updated.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Failed to update currency rates:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            // Create offices
            var usa = new Office("USA", Currency.USD);
            var sweden = new Office("Sweden", Currency.SEK);
            var germany = new Office("Germany", Currency.EUR);

            // Initialize asses epository
            var assetRepository = new AssetRepository();

            // Load sample data
            SeedData.AddDefaultAssets(assetRepository, usa, sweden, germany);

            // Start the console menu
            Menu.Start(assetRepository);
        }
    }
}
