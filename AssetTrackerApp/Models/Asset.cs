using System;

namespace AssetTrackerApp.Models
{
    public abstract class Asset
    {
        public string Type { get; set; } = null!;
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Price Price { get; set; }
        public Office Office { get; set; }

        protected Asset(string brand, string model, DateTime purchaseDate, Price price, Office office)
        {
            Brand = brand;
            Model = model;
            PurchaseDate = purchaseDate;
            Price = price;
            Office = office;
        }
    }
}
