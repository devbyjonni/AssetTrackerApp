using System;
using System.Xml;
using System.Xml.Serialization;
using AssetTrackerApp.Services.ECB;
using AssetTrackerApp.Models;

namespace AssetTrackerApp.Services
{
    /// <summary>
    /// CurrencyConverter is responsible for converting EUR to other currencies
    /// using exchange rates from the European Central Bank (ECB).
    /// </summary>
    public static class CurrencyConverter
    {
        private static readonly string xmlUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        private static Envelope exchangeRates = new Envelope();

        /// <summary>
        /// Converts an amount between two currencies.
        /// Internally converts via EUR since the ECB rates are EUR based.
        /// </summary>
        public static decimal Convert(decimal amount, Currency fromCurrency, Currency toCurrency, out decimal convertedValue)
        {
            // If currencies match no conversion is needed.
            if (fromCurrency == toCurrency)
            {
                convertedValue = amount;
                return convertedValue;
            }

            // Convert the source amount to EUR first.
            decimal amountInEur = fromCurrency == Currency.EUR
                ? amount
                : amount / GetRateForCurrency(fromCurrency);

            // Then convert EUR to the target currency.
            decimal rateToTarget = toCurrency == Currency.EUR
                ? 1m
                : GetRateForCurrency(toCurrency);

            convertedValue = amountInEur * rateToTarget;
            return convertedValue;
        }

        /// <summary>
        /// Backwards compatible wrapper for converting EUR to the specified target currency.
        /// </summary>
        public static decimal ConvertTo(decimal value, Currency targetCurrency, out decimal convertedValue)
        {
            return Convert(value, Currency.EUR, targetCurrency, out convertedValue);
        }

        /// <summary>
        /// Loads the latest exchange rates from the ECB. Throws if deserialization fails.
        /// </summary>
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

        /// <summary>
        /// Gets the exchange rate for a target currency. Returns 1 if not found.
        /// </summary>
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
    }
}
