using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapAction
{
    public enum CountryFields { Name, Alpha2, Alpha3, CountryCode, ISO31662, Region, SubRegion, RegionCode, SubRegionCode };  

    public class CountryConfig
    {
        public List<CountryWithRegionalCodes> listOfCountryWithRegionalCodes;
 
        public CountryConfig()
        {
            listOfCountryWithRegionalCodes = new List<CountryWithRegionalCodes>();
        }

        public void add(CountryWithRegionalCodes countryWithRegionalCodes)
        {
            listOfCountryWithRegionalCodes.Add(countryWithRegionalCodes);
        }

        public string[] countries()
        {   
            string[] array = new string[listOfCountryWithRegionalCodes.Count];

            for (int i = 0; i < listOfCountryWithRegionalCodes.Count; i++)
            {
                array[i] = listOfCountryWithRegionalCodes[i].name;
            }
            return array;
        }

        public string lookup(string country, CountryFields field)
        {
            string result = "Undefined";

            for (int i = 0; i < listOfCountryWithRegionalCodes.Count; i++)
            {
                if (listOfCountryWithRegionalCodes[i].name.ToUpper() == country.ToUpper())
                {
                    switch (field)
                    {
                        case CountryFields.Name:
                            result = listOfCountryWithRegionalCodes[i].name;
                            break;

                        case CountryFields.Alpha2:
                            result = listOfCountryWithRegionalCodes[i].alpha2;
                            break;

                        case CountryFields.Alpha3:
                            result = listOfCountryWithRegionalCodes[i].alpha3;
                            break;

                        case CountryFields.CountryCode:
                            result = listOfCountryWithRegionalCodes[i].countryCode;
                            break;

                        case CountryFields.ISO31662:
                            result = listOfCountryWithRegionalCodes[i].iso31662;
                            break;

                        case CountryFields.Region:
                            result = listOfCountryWithRegionalCodes[i].region;
                            break;

                        case CountryFields.SubRegion:
                            result = listOfCountryWithRegionalCodes[i].subRegion;
                            break;

                        case CountryFields.RegionCode:
                            result = listOfCountryWithRegionalCodes[i].regionCode;
                            break;

                        case CountryFields.SubRegionCode:
                            result = listOfCountryWithRegionalCodes[i].subRegionCode;
                            break;
                        
                        default:
                            break;
                    }
                    break;
                }
            }
            return result;
        }
    }
}