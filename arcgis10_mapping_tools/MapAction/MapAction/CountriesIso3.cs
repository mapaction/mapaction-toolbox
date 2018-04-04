using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MapAction
{
    [Serializable]
    public class CountriesIso3
    {
        [XmlElement("country-iso3")]
        public List<string> CountryIso3 { get; set; }
    }
}
