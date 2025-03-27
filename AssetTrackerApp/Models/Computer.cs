using System;

namespace AssetTrackerApp.Models
{
    /// <summary>
    /// Represents a computer asset with predefined type information.
    /// </summary>
    public class Computer : Asset
    {
        /// <summary>
        /// Initializes a new computer asset with the given details.
        /// Sets the asset type to "Computer".
        /// </summary>
        public Computer(Price price, DateTime purchaseDate, string brand, string model, Office office)
            : base(brand, model, purchaseDate, price, office)
        {
            Type = "Computer";
        }
    }
}
