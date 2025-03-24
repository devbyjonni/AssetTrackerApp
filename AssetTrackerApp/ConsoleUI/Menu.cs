using AssetTrackerApp.Models;
using AssetTrackerApp.Services;
using System;

namespace AssetTrackerApp.ConsoleUI
{
    public static class Menu
    {
        public static void Start(AssetTrackerService tracker)
        {
            // 🆕 We'll keep office references here
            var offices = tracker.GetAllAssets()
                                 .Select(a => a.Office)
                                 .Distinct()
                                 .ToList();

            while (true)
            {
                Console.Clear();
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


        private static void ShowAssetsByType(AssetTrackerService tracker)
        {
            Console.Clear();
            Console.WriteLine("Assets sorted by Type and Purchase Date:\n");

            var assets = tracker.GetSortedAssetsByTypeAndDate();

            foreach (var asset in assets)
            {
                Formatter.PrintAsset(asset);
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void ShowAssetsByOffice(AssetTrackerService tracker)
        {
            Console.Clear();
            Console.WriteLine("Assets sorted by Office and Purchase Date:\n");

            var assets = tracker.GetSortedAssetsByOfficeAndDate();

            foreach (var asset in assets)
            {
                Formatter.PrintAsset(asset);
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void CreateAsset(AssetTrackerService tracker, List<Office> offices)
        {
            Console.Clear();
            Console.WriteLine("Add New Asset\n");

            Console.Write("Type (Computer/Smartphone): ");
            string type = Console.ReadLine()?.Trim().ToLower();

            Console.Write("Brand: ");
            string brand = Console.ReadLine()?.Trim();

            Console.Write("Model: ");
            string model = Console.ReadLine()?.Trim();

            Console.Write("Price amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

            Console.Write("Currency (USD, EUR, SEK): ");
            Enum.TryParse(Console.ReadLine(), out Currency currency);

            Console.Write("Purchase date (yyyy-mm-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime purchaseDate);

            Console.Write("Office (USA, Sweden, Germany): ");
            string officeName = Console.ReadLine()?.Trim();

            // Try to reuse an existing office or create a new one with default currency
            Office office = offices.FirstOrDefault(o => o.Name.Equals(officeName, StringComparison.OrdinalIgnoreCase))
                            ?? new Office(officeName, currency);

            if (!offices.Contains(office))
                offices.Add(office); // Add to office list

            Price price = new Price(amount, currency);

            Asset asset = type switch
            {
                "computer" => new Computer(price, purchaseDate, brand, model, office),
                "smartphone" => new Smartphone(price, purchaseDate, brand, model, office),
                _ => null
            };

            if (asset != null)
            {
                tracker.AddAsset(asset);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAsset added successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid asset type. Only 'Computer' and 'Smartphone' are allowed.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

    }
}
