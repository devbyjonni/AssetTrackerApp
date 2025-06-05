using AssetTrackerApp.Models;
using AssetTrackerApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace AssetTrackerApp.ConsoleUI
{
    /// <summary>
    /// Entry point for the console UI. Handles main menu navigation and asset creation.
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Starts the main menu loop using injected input and output streams.
        /// </summary>
        /// <param name="assetRepository">Asset repository instance used for data operations.</param>
        /// <param name="input">TextReader for reading user input.</param>
        /// <param name="output">TextWriter for writing output.</param>
        public static void Start(AssetRepository assetRepository, TextReader input, TextWriter output)
        {
            // Keep office references for reuse.
            var offices = assetRepository.GetAllAssets()
                                         .Select(a => a.Office)
                                         .Distinct()
                                         .ToList();

            bool exitRequested = false;
            while (!exitRequested)
            {
                output.WriteLine("Asset Tracker");
                output.WriteLine();
                output.WriteLine("Choose an option:");
                output.WriteLine("1. Show all assets sorted by Type & Purchase Date");
                output.WriteLine("2. Show all assets sorted by Office & Purchase Date");
                output.WriteLine("3. Add new asset");
                output.WriteLine("0. Exit");
                output.Write("Enter choice: ");

                string inputLine = input.ReadLine()?.Trim() ?? "";
                switch (inputLine)
                {
                    case "1":
                        ShowAssetsByType(assetRepository, input, output);
                        break;
                    case "2":
                        ShowAssetsByOffice(assetRepository, input, output);
                        break;
                    case "3":
                        CreateAsset(assetRepository, offices, input, output);
                        break;
                    case "0":
                        exitRequested = true;
                        break;
                    default:
                        output.WriteLine("Invalid choice. Press Enter to try again...");
                        input.ReadLine();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays assets sorted by type and purchase date, including a header row.
        /// </summary>
        private static void ShowAssetsByType(AssetRepository assetRepository, TextReader input, TextWriter output)
        {
            output.WriteLine();
            output.WriteLine("Assets sorted by Type and Purchase Date:");
            output.WriteLine();

            // Print column headers.
            AssetTableHeader.PrintHeader(output);

            var assets = assetRepository.GetSortedAssetsByTypeAndDate();
            foreach (var asset in assets)
            {
                // Use the ConsoleFormatter to print the asset with proper formatting.
                ConsoleFormatter.PrintAsset(asset, output);
            }

            output.WriteLine();
            output.WriteLine("Press Enter to return to menu...");
            input.ReadLine();
        }

        /// <summary>
        /// Displays assets sorted by office and purchase date, including a header row.
        /// </summary>
        private static void ShowAssetsByOffice(AssetRepository assetRepository, TextReader input, TextWriter output)
        {
            output.WriteLine();
            output.WriteLine("Assets sorted by Office and Purchase Date:");
            output.WriteLine();

            // Print column headers.
            AssetTableHeader.PrintHeader(output);

            var assets = assetRepository.GetSortedAssetsByOfficeAndDate();
            foreach (var asset in assets)
            {
                // Use the ConsoleFormatter to print the asset with proper formatting.
                ConsoleFormatter.PrintAsset(asset, output);
            }

            output.WriteLine();
            output.WriteLine("Press Enter to return to menu...");
            input.ReadLine();
        }

        /// <summary>
        /// Collects asset input from the user and adds a new asset to the repository.
        /// </summary>
        private static void CreateAsset(AssetRepository assetRepository, List<Office> offices, TextReader input, TextWriter output)
        {
            output.WriteLine("Add New Asset");

            // --- Type ---
            output.Write("Type (Computer/Smartphone): ");
            string? type = input.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(type) || (type != "computer" && type != "smartphone"))
            {
                output.WriteLine("Invalid asset type.");
                return;
            }

            // --- Brand ---
            output.Write("Brand: ");
            string? brand = input.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(brand))
            {
                output.WriteLine("Brand is required.");
                return;
            }

            // --- Model ---
            output.Write("Model: ");
            string? model = input.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(model))
            {
                output.WriteLine("Model is required.");
                return;
            }

            // --- Price ---
            output.Write("Price amount: ");
            if (!decimal.TryParse(
                    input.ReadLine(),
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal amount) || amount <= 0)
            {
                output.WriteLine("Invalid price amount.");
                return;
            }

            // --- Currency ---
            output.Write("Currency (USD, EUR, SEK): ");
            string? currencyInput = input.ReadLine()?.Trim();
            if (!Enum.TryParse(currencyInput, true, out Currency currency))
            {
                output.WriteLine("Invalid currency.");
                return;
            }

            // --- Purchase Date ---
            output.Write("Purchase date (yyyy-MM-dd): ");
            if (!DateTime.TryParseExact(
                    input.ReadLine(),
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime purchaseDate))
            {
                output.WriteLine("Invalid date format.");
                return;
            }

            // --- Office ---
            output.Write("Office (USA, Sweden, Germany): ");
            string? officeName = input.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(officeName))
            {
                output.WriteLine("Office name is required.");
                return;
            }

            // Reuse existing office if available; otherwise create a new one and prompt for its currency
            var office = offices.FirstOrDefault(o => o.Name.Equals(officeName, StringComparison.OrdinalIgnoreCase));
            if (office == null)
            {
                output.Write("Office currency (USD, EUR, SEK): ");
                string? officeCurrencyInput = input.ReadLine()?.Trim();
                if (!Enum.TryParse(officeCurrencyInput, true, out Currency officeCurrency))
                {
                    output.WriteLine("Invalid currency.");
                    return;
                }

                office = new Office(officeName, officeCurrency);
                offices.Add(office);
            }

            var priceObj = new Price(amount, currency);
            Asset asset = type switch
            {
                "computer" => new Computer(priceObj, purchaseDate, brand, model, office),
                "smartphone" => new Smartphone(priceObj, purchaseDate, brand, model, office),
                _ => null! // Should never occur due to validation.
            };

            assetRepository.AddAsset(asset);
            output.WriteLine("Asset added successfully!");
            output.WriteLine("Press Enter to return to menu...");
            input.ReadLine();
        }
    }
}