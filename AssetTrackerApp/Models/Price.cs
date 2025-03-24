namespace AssetTrackerApp.Models
{
    public class Price
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public Price(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
