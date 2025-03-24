namespace AssetTrackerApp.Models
{
    public class Office
    {
        public string Name { get; set; }
        public Currency LocalCurrency { get; set; }

        public Office(string name, Currency localCurrency)
        {
            Name = name;
            LocalCurrency = localCurrency;
        }

        public override string ToString() => Name;
    }
}
