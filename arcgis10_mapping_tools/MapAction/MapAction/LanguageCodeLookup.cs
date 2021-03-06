﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MapAction
{
    public enum LanguageCodeFields { Alpha2, Alpha3b, Alpha3t, Alpha3h, Language };

    [Serializable]
    [XmlRoot("codes")]
    public class LanguageCodeLookup
    {
        [XmlArray("code"), XmlArrayItem(typeof(LanguageCode), ElementName = "code")]

        public List<LanguageCode> listOfLanguageCodes;
        public LanguageCodeLookup()
        {
            listOfLanguageCodes = new List<LanguageCode>();
        }
        public void add(LanguageCode languageCode)
        {
            listOfLanguageCodes.Add(languageCode);
        }
        public string[] languages()
        {
            string[] array = new string[listOfLanguageCodes.Count];
            for (int i = 0; i < listOfLanguageCodes.Count; i++)
            {
                array[i] = listOfLanguageCodes[i].lang;
            }
            return array;
        }
        public string lookup(string language, LanguageCodeFields field)
        {
            string result = "Undetermined";

            for (int i = 0; i < listOfLanguageCodes.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine(lang.ToUpper() + " ------- " + listOfLanguageCodes[i].lang.ToUpper());
                if (language.ToUpper() == listOfLanguageCodes[i].lang.ToUpper())
                {
                    switch (field)
                    {
                        case LanguageCodeFields.Alpha2:
                            result = listOfLanguageCodes[i].a2;
                            break;
                        case LanguageCodeFields.Alpha3b:
                            result = listOfLanguageCodes[i].a3b;
                            break;
                        case LanguageCodeFields.Alpha3h:
                            result = listOfLanguageCodes[i].a3h;
                            break;
                        case LanguageCodeFields.Alpha3t:
                            result = listOfLanguageCodes[i].a3t;
                            break;
                        case LanguageCodeFields.Language:
                            result = listOfLanguageCodes[i].lang;
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }
        public string lookupA2LanguageCode(string languageCode, LanguageCodeFields field)
        {
            string result = "Undetermined";

            if (languageCode != null && languageCode != String.Empty)
            {
                for (int i = 0; i < listOfLanguageCodes.Count; i++)
                {
                    if (listOfLanguageCodes[i].a2.ToUpper() == languageCode.ToUpper())
                    {
                        switch (field)
                        {
                            case LanguageCodeFields.Alpha2:
                                result = listOfLanguageCodes[i].a2;
                                break;
                            case LanguageCodeFields.Alpha3b:
                                result = listOfLanguageCodes[i].a3b;
                                break;
                            case LanguageCodeFields.Alpha3h:
                                result = listOfLanguageCodes[i].a3h;
                                break;
                            case LanguageCodeFields.Alpha3t:
                                result = listOfLanguageCodes[i].a3t;
                                break;
                            case LanguageCodeFields.Language:
                                result = listOfLanguageCodes[i].lang;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}