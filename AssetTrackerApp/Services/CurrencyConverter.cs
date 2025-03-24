using System;
using System.Xml;
using System.Xml.Serialization;
using AssetTrackerApp.Services.ECB; // Namespace for ECBEnvelope, Cube, etc.
using AssetTrackerApp.Models;

namespace AssetTrackerApp.Services
{
    public static class CurrencyConverter
    {
        private static readonly string xmlUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        private static Envelope envelope = Update();

        public static decimal ConvertTo(decimal value, Currency targetCurrency, out decimal convertedValue)
        {
            // Default: input is in EUR
            convertedValue = value;

            // Handle direct EUR case
            if (targetCurrency == Currency.EUR)
                return convertedValue;

            try
            {
                // Convert EUR → target currency
                var rate = envelope?.Cube?.Cube1?.Cube?.Find(c => c.currency == targetCurrency.ToString())?.rate ?? 1;
                convertedValue = value * rate;
                return convertedValue;
            }
            catch
            {
                // In case of failure, just return input as fallback
                return value;
            }
        }

        public static Envelope Update()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
                using var reader = XmlReader.Create(xmlUrl);
                envelope = (Envelope)serializer.Deserialize(reader);
                return envelope;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to fetch currency rates:");
                Console.WriteLine(ex.Message);
                return new Envelope();
            }
        }
    }
}
