using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MapActionToolbar_Core
{
    public class LanguageCode
    {
        // Read from https://github.com/filak/iso-language-codes/blob/master/_output/codes_lookup.xml
        public string a2 { get; set; }
        public string a3b { get; set; }
        public string a3t { get; set; }
        public string a3h { get; set; }
        public string lang { get; set; }
        public LanguageCode(string a2,
                           string a3b,
                           string a3t,
                           string a3h,
                           string lang)
        {
            this.a2 = a2;
            this.a3b = a3b;
            this.a3t = a3t;
            this.a3h = a3h;
            this.lang = lang;
        }
    }
}