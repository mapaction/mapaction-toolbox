using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapAction
{
    public class LanguageConfig
    {
        private string language;
        private Dictionary<string, string> dict;

        public LanguageConfig(string language, Dictionary<string, string> dict)
        {
            this.setLanguage(language);
            this.setDictionary(dict);
        }

        public void setLanguage(string language)
        {
            this.language = language;
        }

        public string getLanguage()
        {
            return this.language;
        }

        public void setDictionary(Dictionary<string, string> dictionary)
        {
            this.dict = dictionary;
        }

        public Dictionary<string, string> getDictionary()
        {
            return this.dict;
        }

    }
}
