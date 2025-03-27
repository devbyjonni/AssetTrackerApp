using System;

namespace AssetTrackerApp.Models
{
    /// <summary>
    /// Represents a base class for any type of asset.
    /// Contains shared properties like brand, model, purchase date, price, and office.
    /// </summary>
    public abstract class Asset
    {
        /// <summary>
        /// The type of the asset (e.g., "Computer" or "Smartphone").
        /// </summary>
        public string Type { get; protected set; } = null!; // Set in subclass constructor

        // Developer note:
        // Using `null!` to satisfy the C# compiler.
        // This property will always be set by the subclass (e.g., Computer or Smartphone).
        // I don't want to allow nulls (so not using string?),
        // and I can't initialize it here in the base class — this is the cleanest option.


        /// <summary>
        /// The brand or manufacturer of the asset.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The model name or number.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The purchase date of the asset.
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// The price of the asset (amount + currency).
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// The office/location the asset belongs to.
        /// </summary>
        public Office Office { get; set; }

        /// <summary>
        /// Constructs an asset with common properties.
        /// </summary>
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
