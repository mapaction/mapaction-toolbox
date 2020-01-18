using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapActionToolbar_Core
{
    public enum CountryFields { Alpha2Code, Alpha3Code };

    public class RegionalBloc
    {
        [JsonProperty("acronym")]
        public string Acronym { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("otherAcronyms")]
        public List<string> OtherAcronyms { get; set; }

        [JsonProperty("otherNames")]
        public List<string> OtherNames { get; set; }
    }

    public class Currency
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Namee { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }

    public class Language
    {
        [JsonProperty("iso639_1")]
        public string ISO639_1 { get; set; }

        [JsonProperty("iso639_2")]
        public string ISO639_2 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }
    }

    public class Country
    {
        public Country()
        {
            TopLevelDomain = new List<string>();
            CallingCodes = new List<string>();
            AltSpellings = new List<string>();
            Timezones = new List<string>();
            Borders = new List<string>();
            Currencies = new List<Currency>();
            Languages = new List<Language>();
            Translations = new Dictionary<string, string>();
            RegionalBlocs = new List<RegionalBloc>();
            Latlng = new long[2] { 0, 0 };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topLevelDomain")]
        public List<string> TopLevelDomain { get; set; }

        [JsonProperty("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonProperty("alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty("callingCodes")]
        public List<string> CallingCodes { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        public string Subregion { get; set; }

        [JsonProperty("population")]
        public int population { get; set; }

        [JsonProperty("latlng")]
        public long[] Latlng { get; set; }

        [JsonProperty("demonym")]
        public string Demonym { get; set; }

        [JsonProperty("area")]
        public ulong Area { get; set; }  // Square km

        [JsonProperty("gini")]
        public long Gini { get; set; } // Gini https://en.wikipedia.org/wiki/Gini_coefficient

        [JsonProperty("timezones")]
        public List<string> Timezones { get; set; }

        [JsonProperty("borders")]
        public List<string> Borders { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("numericCode")]
        public string NumericCode { get; set; }

        [JsonProperty("currencies")]
        public List<Currency> Currencies { get; set; }

        [JsonProperty("languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty("translations")]
        public Dictionary<string, string> Translations { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("regionalBlocs")]
        public List<RegionalBloc> RegionalBlocs { get; set; }

        [JsonProperty("cioc")]
        public string Cioc { get; set; }
    }

    [JsonArray]
    public class Countries
    {
        public List<Country> countries;

        public Countries()
        {
            countries = new List<Country>();
        }

        public Countries(List<Country> listOfCountries)
        {
            countries = listOfCountries;
        }

        public void set(List<Country> listOfCountries)
        {
            countries = listOfCountries;
        }

        public List<string> countryNames()
        {
            List<string> names = new List<string>();
            foreach (var country in countries)
            {
                names.Add(country.Name);
            }
            return names;
        }

        public List<string> timeZones(string countryName)
        {
            List<string> timeZones = new List<string>();
            foreach (var country in countries)
            {
                if (country.Name == countryName)
                {
                    foreach (var timeZone in country.Timezones)
                    {
                        timeZones.Add(timeZone);
                    }
                    break;
                }
            }
            return timeZones;
        }

        public List<string> languages(string countryName)
        {
            List<string> languages = new List<string>();
            foreach (var country in countries)
            {
                if (country.Name == countryName)
                {
                    foreach (var language in country.Languages)
                    {
                        languages.Add(language.Name);
                    }
                    break;
                }
            }
            return languages;
        }


        public string alpha3Code(string countryName)
        {
            string alpha3Code = "";
            foreach (var country in countries)
            {
                if (country.Name == countryName)
                {
                    alpha3Code = country.Alpha3Code;
                    break;
                }
            }
            return alpha3Code;
        }

        public string nameFromAlpha3Code(string alpha3Code)
        {
            string name = "";
            foreach (var country in countries)
            {
                if (country.Alpha3Code == alpha3Code)
                {
                    name = country.Name;
                    break;
                }
            }
            return name;
        }
    }
}
