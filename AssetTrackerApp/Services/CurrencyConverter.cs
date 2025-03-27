using System;
using System.Xml;
using System.Xml.Serialization;
using AssetTrackerApp.Services.ECB;
using AssetTrackerApp.Models;

namespace AssetTrackerApp.Services
{
    /// CurrencyConverter is responsible for converting EUR to other currencies using exchange rates
    /// from the European Central Bank (ECB). Rates are fetched and deserialized from XML once at startup.
    public static class CurrencyConverter
    {
        // ECB endpoint for daily exchange rates in XML
        private static readonly string xmlUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        // Holds the deserialized ECB data (top-level XML structure)
        private static Envelope exchangeRates = Update();

        /// Converts a value in EUR to the specified target currency.
        /// If conversion fails, the original EUR value is returned.
        public static decimal ConvertTo(decimal value, Currency targetCurrency, out decimal convertedValue)
        {
            convertedValue = value;

            if (targetCurrency == Currency.EUR)
                return convertedValue;

            try
            {
                var rate = GetRateForCurrency(targetCurrency);
                convertedValue = value * rate;
                return convertedValue;
            }
            catch
            {
                // Fallback: return original value if conversion fails
                return value;
            }
        }

        /// Fetches and deserializes the latest ECB exchange rates from XML.
        public static Envelope Update()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
                using var reader = XmlReader.Create(xmlUrl);
                var result = serializer.Deserialize(reader) as Envelope;

                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Failed to deserialize currency data.");
                    Console.ResetColor();
                    return new Envelope(); // fallback
                }

                exchangeRates = result;
                return exchangeRates;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Failed to fetch currency rates:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return new Envelope(); // fallback
            }
        }

        /// Finds the exchange rate for the given currency in the ECB data.
        /// If the rate is not found, a warning is printed and 1 is returned, 
        /// which means the original EUR value will be used without conversion.
        private static decimal GetRateForCurrency(Currency currency)
        {
            var cubeList = exchangeRates?.CubeContainer?.CubeDataSet?.Rates;

            if (cubeList == null || cubeList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️ No exchange rates available. Defaulting to EUR value.");
                Console.ResetColor();
                return 1m;
            }

            CurrencyRate? match = cubeList.Find(c => c.CurrencyCode == currency.ToString());

            if (match == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️ Exchange rate for '{currency}' not found. Defaulting to EUR value.");
                Console.ResetColor();
                return 1m;
            }

            return match.Rate;
        }

        public static Envelope UpdateRates()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
            using var reader = XmlReader.Create(xmlUrl);
            var updatedRates = (Envelope?)serializer.Deserialize(reader);

            if (updatedRates == null)
            {
                throw new InvalidOperationException("Deserialization returned null.");
            }

            exchangeRates = updatedRates;
            return exchangeRates;
        }
    }
}
