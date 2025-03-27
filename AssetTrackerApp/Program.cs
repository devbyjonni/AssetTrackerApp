using System;
using AssetTrackerApp.Models;
using AssetTrackerApp.Services;
using AssetTrackerApp.Data;
using AssetTrackerApp.ConsoleUI;

namespace AssetTrackerApp
{
    /// <summary>
    /// Entry point of the application.
    /// Initializes core data and starts the console UI.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Attempt to fetch latest exchange rates
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

            // Create default office instances
            var usa = new Office("USA", Currency.USD);
            var sweden = new Office("Sweden", Currency.SEK);
            var germany = new Office("Germany", Currency.EUR);

            // Initialize in-memory asset repository
            var assetRepository = new AssetRepository();

            // Load sample seed data
            SeedData.AddDefaultAssets(assetRepository, usa, sweden, germany);

            // Launch main console menu
            Menu.Start(assetRepository);
        }
    }
}
