namespace AssetTrackerApp.Models
{
    /// <summary>
    /// Represents an office location and its local currency.
    /// </summary>
    public class Office
    {
        /// <summary>
        /// The name of the office (e.g., "USA", "Sweden").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The local currency used by this office.
        /// </summary>
        public Currency LocalCurrency { get; set; }

        /// <summary>
        /// Creates a new office with a name and local currency.
        /// </summary>
        public Office(string name, Currency localCurrency)
        {
            Name = name;
            LocalCurrency = localCurrency;
        }

        /// <summary>
        /// Returns the office name as a string.
        /// </summary>
        public override string ToString() => Name;
    }
}
