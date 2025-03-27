using AssetTrackerApp.Models;
using AssetTrackerApp.Data;
using System;

namespace AssetTrackerApp.ConsoleUI
{
    public static class Menu
    {
        public static void Start(AssetRepository tracker)
        {
            // 🆕 We'll keep office references here
            var offices = tracker.GetAllAssets()
                                 .Select(a => a.Office)
                                 .Distinct()
                                 .ToList();

            while (true)
            {
                //Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("📦 Asset Tracker");
                Console.ResetColor();

                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Show all assets sorted by Type & Purchase Date");
                Console.WriteLine("2. Show all assets sorted by Office & Purchase Date");
                Console.WriteLine("3. Add new asset");
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter choice: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                switch (input)
                {
                    case "1":
                        ShowAssetsByType(tracker);
                        break;
                    case "2":
                        ShowAssetsByOffice(tracker);
                        break;
                    case "3":
                        CreateAsset(tracker, offices);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void ShowAssetsByType(AssetRepository tracker)
        {
            Console.Clear();
            Console.WriteLine("Assets sorted by Type and Purchase Date:\n");

            var assets = tracker.GetSortedAssetsByTypeAndDate();

            foreach (var asset in assets)
            {
                ConsoleFormatter.PrintAsset(asset);
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void ShowAssetsByOffice(AssetRepository tracker)
        {
            Console.Clear();
            Console.WriteLine("Assets sorted by Office and Purchase Date:\n");

            var assets = tracker.GetSortedAssetsByOfficeAndDate();

            foreach (var asset in assets)
            {
                ConsoleFormatter.PrintAsset(asset);
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void CreateAsset(AssetRepository tracker, List<Office> offices)
        {
            Console.Clear();
            Console.WriteLine("Add New Asset\n");

            // --- Type ---
            Console.Write("Type (Computer/Smartphone): ");
            string? type = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(type) || (type != "computer" && type != "smartphone"))
            {
                Console.WriteLine("⚠️ Invalid asset type.");
                return;
            }

            // --- Brand ---
            Console.Write("Brand: ");
            string? brand = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(brand))
            {
                Console.WriteLine("⚠️ Brand is required.");
                return;
            }

            // --- Model ---
            Console.Write("Model: ");
            string? model = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(model))
            {
                Console.WriteLine("⚠️ Model is required.");
                return;
            }

            // --- Price ---
            Console.Write("Price amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("⚠️ Invalid price amount.");
                return;
            }

            // --- Currency ---
            Console.Write("Currency (USD, EUR, SEK): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out Currency currency))
            {
                Console.WriteLine("⚠️ Invalid currency.");
                return;
            }

            // --- Purchase Date ---
            Console.Write("Purchase date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime purchaseDate))
            {
                Console.WriteLine("⚠️ Invalid date format.");
                return;
            }

            // --- Office ---
            Console.Write("Office (USA, Sweden, Germany): ");
            string? officeName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(officeName))
            {
                Console.WriteLine("⚠️ Office name is required.");
                return;
            }

            var office = new Office(officeName, currency);
            if (!offices.Any(o => o.Name.Equals(office.Name, StringComparison.OrdinalIgnoreCase)))
            {
                offices.Add(office);
            }

            // --- Create Price ---
            var price = new Price(amount, currency);

            // --- Create Asset ---
            Asset asset = type switch
            {
                "computer" => new Computer(price, purchaseDate, brand, model, office),
                "smartphone" => new Smartphone(price, purchaseDate, brand, model, office),
                _ => null! // won't be reached due to earlier validation
            };

            // --- Add to Tracker ---
            tracker.AddAsset(asset);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Asset added successfully!");
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }


    }
}
