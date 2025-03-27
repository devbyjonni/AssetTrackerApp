using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AssetTrackerApp.Services.ECB
{
    /// <summary>
    /// Root element of the ECB exchange rate XML.
    /// Contains the main currency rate container.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public class Envelope
    {
        private const string EcbNamespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

        [XmlElement(ElementName = "Cube", Namespace = EcbNamespace)]
        public CubeContainer CubeContainer { get; set; } = new CubeContainer();
    }

    /// <summary>
    /// Wraps the time-specific currency data container.
    /// </summary>
    public class CubeContainer
    {
        [XmlElement(ElementName = "Cube")]
        public CubeDataSet CubeDataSet { get; set; } = new CubeDataSet();
    }

    /// <summary>
    /// Contains the actual currency exchange rates as a list of Cube entries.
    /// </summary>
    public class CubeDataSet
    {
        [XmlElement(ElementName = "Cube")]
        public List<CurrencyRate> Rates { get; set; } = new List<CurrencyRate>();
    }

    /// <summary>
    /// Represents a single currency and its exchange rate.
    /// </summary>
    public class CurrencyRate
    {
        [XmlAttribute(AttributeName = "currency")]
        public string CurrencyCode { get; set; } = null!;

        [XmlAttribute(AttributeName = "rate")]
        public decimal Rate { get; set; }
    }
}
