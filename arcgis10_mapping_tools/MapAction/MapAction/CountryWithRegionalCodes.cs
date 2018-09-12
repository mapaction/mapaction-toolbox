using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MapAction
{
    public class CountryWithRegionalCodes
    {
        public string name { get; set; }
        public string alpha2 { get; set; }
        public string alpha3 { get; set; }
        public string countryCode { get; set; }
        public string iso31662 { get; set; }
        public string region { get; set; }
        public string subRegion { get; set; }
        public string regionCode { get; set; }
        public string subRegionCode { get; set; }
        public CountryWithRegionalCodes(string name,
                                       string alpha2,
                                       string alpha3,
                                       string countryCode,
                                       string iso31662,
                                       string region,
                                       string subRegion,
                                       string regionCode,
                                       string subRegionCode)
        {
            this.name = name;
            this.alpha2 = alpha2;
            this.alpha3 = alpha3;
            this.countryCode = countryCode;
            this.iso31662 = iso31662;
            this.region = region;
            this.subRegion = subRegion;
            this.regionCode = regionCode;
            this.subRegionCode = subRegionCode;
        }
    }
}
