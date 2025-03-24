namespace AssetTrackerApp.Models
{
    public class Computer : Asset
    {
        public Computer(Price price, DateTime purchaseDate, string brand, string model, Office office)
            : base(brand, model, purchaseDate, price, office)
        {
            Type = "Computer";
        }
    }
}
