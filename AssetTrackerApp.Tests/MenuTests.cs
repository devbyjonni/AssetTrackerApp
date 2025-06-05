using System;
using System.IO;
using System.Linq;
using Xunit;
using AssetTrackerApp.ConsoleUI;
using AssetTrackerApp.Data;
using AssetTrackerApp.Models;

namespace AssetTrackerApp.Tests
{
    public class MenuTests
    {
        /// <summary>
        /// Verifies that an invalid menu choice shows an error message.
        /// The extra newline simulates the user pressing Enter for the error prompt.
        /// </summary>
        [Fact]
        public void Menu_Start_InvalidChoice_ShowsError()
        {
            // Arrange: simulate "invalid" input, extra newline, then "0" to exit.
            var input = new StringReader("invalid\n\n0\n");
            var output = new StringWriter();
            var repository = new AssetRepository();

            // Act
            Menu.Start(repository, input, output);

            // Assert: Verify output contains a message about an invalid choice.
            string result = output.ToString();
            Assert.Contains("Invalid choice", result);
        }

        /// <summary>
        /// Verifies that selecting option "1" displays assets sorted by type and purchase date.
        /// </summary>
        [Fact]
        public void Menu_Start_ShowAssetsByType_DisplaysAssets()
        {
            // Arrange: add a sample asset.
            var repository = new AssetRepository();
            var office = new Office("TestOffice", Currency.USD);
            repository.AddAsset(new Computer(new Price(100, Currency.USD), DateTime.Now.AddMonths(-12), "Dell", "XPS", office));

            // Simulate input: "1" to display assets, extra newline for "Press Enter to return to menu...", then "0" to exit.
            var input = new StringReader("1\n\n0\n\n");
            var output = new StringWriter();

            // Act
            Menu.Start(repository, input, output);

            // Assert: Check that output contains the header for option 1.
            string result = output.ToString();
            Assert.Contains("Assets sorted by Type and Purchase Date", result);
        }

        /// <summary>
        /// Verifies that selecting option "2" displays assets sorted by office and purchase date.
        /// </summary>
        [Fact]
        public void Menu_Start_ShowAssetsByOffice_DisplaysAssets()
        {
            // Arrange: add a sample asset.
            var repository = new AssetRepository();
            var office = new Office("TestOffice", Currency.USD);
            repository.AddAsset(new Computer(new Price(100, Currency.USD), DateTime.Now.AddMonths(-12), "Dell", "XPS", office));

            // Simulate input: "2" to display assets, extra newline for "Press Enter...", then "0" to exit.
            var input = new StringReader("2\n\n0\n\n");
            var output = new StringWriter();

            // Act
            Menu.Start(repository, input, output);

            // Assert: Check that output contains the header for option 2.
            string result = output.ToString();
            Assert.Contains("Assets sorted by Office and Purchase Date", result);
        }

        /// <summary>
        /// Verifies that the asset creation flow (option "3") successfully adds a new asset with the expected details.
        /// </summary>
        [Fact]
        public void Menu_Start_CreateAsset_AddsAsset()
        {
            // Arrange: create an empty asset repository.
            var repository = new AssetRepository();

            // Simulate input for asset creation:
            // "3" selects asset creation, then:
            // Type: "computer"
            // Brand: "TestBrand"
            // Model: "TestModel"
            // Price: "123.45"
            // Currency: "USD"
            // Purchase date: "2025-03-27"
            // Office: "TestOffice"
            // Office currency supplied as "USD" since the office is new
            // Extra newline for the "Press Enter to return to menu..." prompt,
            // then "0" to exit, with an extra newline.
            string inputString =
                "3\n" +
                "computer\n" +
                "TestBrand\n" +
                "TestModel\n" +
                "123.45\n" +
                "USD\n" +
                "2025-03-27\n" +
                "TestOffice\n" +
                "USD\n" +
                "\n" +
                "0\n\n";
            var input = new StringReader(inputString);
            var output = new StringWriter();

            // Act: Run the menu with simulated input/output.
            Menu.Start(repository, input, output);

            // Assert: Verify that exactly one asset was added.
            var assets = repository.GetAllAssets();
            Assert.Single(assets);

            var asset = assets.First();
            Assert.Equal("Computer", asset.Type);
            Assert.Equal("TestBrand", asset.Brand);
            Assert.Equal("TestModel", asset.Model);
            Assert.Equal(123.45m, asset.Price.Amount);
            Assert.Equal(Currency.USD, asset.Price.Currency);
            Assert.Equal("TestOffice", asset.Office.Name);
        }
    }
}