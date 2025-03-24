namespace AssetTrackerApp.Models
{
    public class Smartphone : Asset
    {
        public Smartphone(Price price, DateTime purchaseDate, string brand, string model, Office office)
            : base(brand, model, purchaseDate, price, office)
        {
            Type = "Smartphone";
        }
    }
}
