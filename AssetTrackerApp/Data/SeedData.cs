using AssetTrackerApp.Models;
using AssetTrackerApp.Data;
using System;

namespace AssetTrackerApp.Data
{
    public static class SeedData
    {
        public static void AddDefaultAssets(IAssetRepository repository, Office usa, Office sweden, Office germany)
        {
            repository.AddAsset(new Smartphone(new Price(200, Currency.USD), DateTime.Now.AddMonths(-32), "Motorola", "X3", usa));
            repository.AddAsset(new Smartphone(new Price(400, Currency.USD), DateTime.Now.AddMonths(-31), "Motorola", "X3", usa));
            repository.AddAsset(new Smartphone(new Price(400, Currency.USD), DateTime.Now.AddMonths(-26), "Motorola", "X2", usa));
            repository.AddAsset(new Smartphone(new Price(4500, Currency.SEK), DateTime.Now.AddMonths(-30), "Samsung", "Galaxy 10", sweden));
            repository.AddAsset(new Smartphone(new Price(4500, Currency.SEK), DateTime.Now.AddMonths(-29), "Samsung", "Galaxy 10", sweden));
            repository.AddAsset(new Smartphone(new Price(3000, Currency.SEK), DateTime.Now.AddMonths(-32), "Sony", "Xperia 7", sweden));
            repository.AddAsset(new Smartphone(new Price(3000, Currency.SEK), DateTime.Now.AddMonths(-31), "Sony", "Xperia 7", sweden));
            repository.AddAsset(new Smartphone(new Price(220, Currency.EUR), DateTime.Now.AddMonths(-24), "Siemens", "Brick", germany));

            repository.AddAsset(new Computer(new Price(100, Currency.USD), DateTime.Now.AddMonths(-38), "Dell", "Desktop 900", usa));
            repository.AddAsset(new Computer(new Price(100, Currency.USD), DateTime.Now.AddMonths(-37), "Dell", "Desktop 900", usa));
            repository.AddAsset(new Computer(new Price(300, Currency.USD), DateTime.Now.AddMonths(-35), "Lenovo", "X100", usa));
            repository.AddAsset(new Computer(new Price(300, Currency.USD), DateTime.Now.AddMonths(-32), "Lenovo", "X200", usa));
            repository.AddAsset(new Computer(new Price(500, Currency.USD), DateTime.Now.AddMonths(-27), "Lenovo", "X300", usa));
            repository.AddAsset(new Computer(new Price(1500, Currency.SEK), DateTime.Now.AddMonths(-29), "Dell", "Optiplex 100", sweden));
            repository.AddAsset(new Computer(new Price(1400, Currency.SEK), DateTime.Now.AddMonths(-28), "Dell", "Optiplex 200", sweden));
            repository.AddAsset(new Computer(new Price(1300, Currency.SEK), DateTime.Now.AddMonths(-27), "Dell", "Optiplex 300", sweden));
            repository.AddAsset(new Computer(new Price(1600, Currency.EUR), DateTime.Now.AddMonths(-22), "Asus", "ROG 600", germany));
            repository.AddAsset(new Computer(new Price(1200, Currency.EUR), DateTime.Now.AddMonths(-32), "Asus", "ROG 500", germany));
            repository.AddAsset(new Computer(new Price(1200, Currency.EUR), DateTime.Now.AddMonths(-33), "Asus", "ROG 500", germany));
            repository.AddAsset(new Computer(new Price(1300, Currency.EUR), DateTime.Now.AddMonths(-34), "Asus", "ROG 500", germany));
        }
    }
}
