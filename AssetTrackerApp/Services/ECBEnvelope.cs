using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AssetTrackerApp.Services.ECB
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public class Envelope
    {
        [XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public OuterCube Cube { get; set; } = new OuterCube();
    }

    public class OuterCube
    {
        [XmlElement(ElementName = "Cube")]
        public InnerCube Cube1 { get; set; } = new InnerCube();
    }

    public class InnerCube
    {
        [XmlElement(ElementName = "Cube")]
        public List<CurrencyCube> Cube { get; set; } = new List<CurrencyCube>();
    }

    public class CurrencyCube
    {
        [XmlAttribute(AttributeName = "currency")]
        public string currency { get; set; }

        [XmlAttribute(AttributeName = "rate")]
        public decimal rate { get; set; }
    }
}
