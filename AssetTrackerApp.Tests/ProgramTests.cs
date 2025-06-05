using System;
using Xunit;
using AssetTrackerApp.Models;
using AssetTrackerApp.Data;

namespace AssetTrackerApp.Tests
{
    public class ProgramTests
    {
        /// <summary>
        /// Verifies that the seed data successfully adds assets to the repository.
        /// </summary>
        [Fact]
        public void SeedData_Should_Add_Assets()
        {
            // Arrange
            var usa = new Office("USA", Currency.USD);
            var sweden = new Office("Sweden", Currency.SEK);
            var germany = new Office("Germany", Currency.EUR);
            var repository = new AssetRepository();

            // Act
            SeedData.AddDefaultAssets(repository, usa, sweden, germany);
            var assets = repository.GetAllAssets();

            // Assert
            Assert.NotEmpty(assets);
        }

        /// <summary>
        /// Ensures that assets are correctly sorted by type (alphabetically) and purchase date.
        /// For example, "Computer" should appear before "Smartphone".
        /// </summary>
        [Fact]
        public void GetSortedAssetsByTypeAndDate_Should_Return_Sorted_Assets()
        {
            // Arrange
            var usa = new Office("USA", Currency.USD);
            var repository = new AssetRepository();
            var price = new Price(100, Currency.USD);
            var oldComputer = new Computer(price, new DateTime(2019, 1, 1), "Dell", "Optiplex", usa);
            var newComputer = new Computer(price, new DateTime(2020, 1, 1), "Dell", "Optiplex", usa);

            var oldPhone = new Smartphone(price, new DateTime(2021, 1, 1), "Samsung", "Galaxy", usa);
            var newPhone = new Smartphone(price, new DateTime(2022, 1, 1), "Samsung", "Galaxy", usa);

            // Add in unsorted order to verify sorting logic
            repository.AddAsset(newPhone);
            repository.AddAsset(newComputer);
            repository.AddAsset(oldComputer);
            repository.AddAsset(oldPhone);

            // Act
            var sortedAssets = repository.GetSortedAssetsByTypeAndDate();

            // Assert: first by type then by purchase date within each type
            Assert.Equal(4, sortedAssets.Count);

            Assert.Equal("Computer", sortedAssets[0].Type);
            Assert.Equal(new DateTime(2019, 1, 1), sortedAssets[0].PurchaseDate);

            Assert.Equal("Computer", sortedAssets[1].Type);
            Assert.Equal(new DateTime(2020, 1, 1), sortedAssets[1].PurchaseDate);

            Assert.Equal("Smartphone", sortedAssets[2].Type);
            Assert.Equal(new DateTime(2021, 1, 1), sortedAssets[2].PurchaseDate);

            Assert.Equal("Smartphone", sortedAssets[3].Type);
            Assert.Equal(new DateTime(2022, 1, 1), sortedAssets[3].PurchaseDate);
        }

        /// <summary>
        /// Verifies that assets are sorted by office name (alphabetically) and purchase date.
        /// </summary>
        [Fact]
        public void GetSortedAssetsByOfficeAndDate_Should_Return_Sorted_Assets()
        {
            // Arrange
            var officeA = new Office("A-Office", Currency.USD);
            var officeB = new Office("B-Office", Currency.USD);
            var repository = new AssetRepository();
            var price = new Price(100, Currency.USD);
            var assetA = new Computer(price, new DateTime(2020, 1, 1), "Dell", "Optiplex", officeA);
            var assetB = new Smartphone(price, new DateTime(2021, 1, 1), "Apple", "iPhone", officeB);

            repository.AddAsset(assetB);
            repository.AddAsset(assetA);

            // Act
            var sortedAssets = repository.GetSortedAssetsByOfficeAndDate();

            // Assert: "A-Office" should appear before "B-Office".
            Assert.Equal("A-Office", sortedAssets[0].Office.Name);
            Assert.Equal("B-Office", sortedAssets[1].Office.Name);
        }
    }
}
