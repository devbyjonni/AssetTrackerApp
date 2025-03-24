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
            // Initialize currency data
            CurrencyConverter.Update();

            // Create offices
            var usa = new Office("USA", Currency.USD);
            var sweden = new Office("Sweden", Currency.SEK);
            var germany = new Office("Germany", Currency.EUR);

            // Initialize tracker
            var tracker = new AssetTrackerService();

            // Load sample data
            SeedData.AddDefaultAssets(tracker, usa, sweden, germany);

            // Start the console menu
            Menu.Start(tracker);
        }
    }
}
