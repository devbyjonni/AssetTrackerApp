using System;

namespace AssetTrackerApp.Models
{
    /// <summary>
    /// Represents a smartphone asset with predefined type information.
    /// </summary>
    public class Smartphone : Asset
    {
        /// <summary>
        /// Initializes a new smartphone asset with the given details.
        /// Sets the asset type to "Smartphone".
        /// </summary>
        public Smartphone(Price price, DateTime purchaseDate, string brand, string model, Office office)
            : base(brand, model, purchaseDate, price, office)
        {
            Type = "Smartphone";
        }
    }
}
