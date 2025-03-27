namespace AssetTrackerApp.Models
{
    /// <summary>
    /// Represents a monetary value with currency.
    /// </summary>
    public class Price
    {
        /// <summary>
        /// The numeric amount of the price.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The currency associated with the amount.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Creates a new price with the given amount and currency.
        /// </summary>
        public Price(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// Returns the price as a formatted string (e.g., "100 USD").
        /// </summary>
        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
