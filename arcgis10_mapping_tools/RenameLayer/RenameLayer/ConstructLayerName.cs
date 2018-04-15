using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MapAction;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;

namespace RenameLayer
{
    public class ConstructLayerName
    {
        public string LoopThroughNameElements(Array arr)
        {
            string _tempLayerName = string.Empty;

            foreach (string s in arr)
            {
                if (s != string.Empty)
                {
                    _tempLayerName += s + "_";
                }
            }
            //trim the final "_" from the string
            _tempLayerName = _tempLayerName.TrimEnd('_');

            return _tempLayerName;
        }

        public static string pathToLookupCSV()
        {
            string _rawPath;
            string _path;

            // check crash move folder first - if it doesn't exist then look on C drive - PJR 18/08/2016
            _rawPath = MapAction.Utilities.getCrashMoveFolderPath();
            _path = _rawPath + @"\GIS\2_Active_Data\200_data_name_lookup\";
                        
            if (Directory.Exists(_path))
            {
                // data name lookup folder exists in crash move folder
                //MessageBox.Show("Crash Move Folder found");
                return _path;
            }
            else
            {
                _rawPath = @"C:\MapAction";
                _path = _rawPath + @"\200_data_name_lookup\";

                if (Directory.Exists(_path))
                {
                    // data name lookup folder not in crash move folder, but found on C drive
                    //MessageBox.Show(_path + " found");
                    return _path;
                }
                else
                {
                    // data name lookup folder not found either in crash move folder nor on C drive
                    //MessageBox.Show("csv files not found");
                    _path = "XXX";
                    return _path;
                }
            }
        }

        public static Boolean checkPathToLookupCSV()
        {
            string _path = ConstructLayerName.pathToLookupCSV() + @"\01_geoextent.csv"; //added 01 PJR 18/08/2016

            // Checks to see if the 'geoextent.csv' file exist in the config tool directory
            if (File.Exists(_path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    public static class RenameLayerToolValues
    {

        public static Dictionary<string, string> csvToDictionary(string @path)
        {
            
            // original treatment - single clever line
            //var dict = File.ReadAllLines(path).Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);  
            //return dict;

            // new treatment takes account of fields that contain commas, and removes accents (diacritics) from characters
            // example use of TextFieldParser from 
            // http://stackoverflow.com/questions/2081418/parsing-csv-files-in-c-with-header
            // PJR 20/10/2016
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // this encoding ensures that accented characters are read correctly and displayed correctly in the drop downs
            // no need to remove or replace accented characters now
            // code page 1252 is "windows-1252" a superset of ISO-8859-1
            // use TextFieldParser to read in in fields from csv file, one row at a time
            using (TextFieldParser parser = new TextFieldParser(@path,System.Text.Encoding.GetEncoding (1252)) )
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                // read first row
                string[] fields = parser.ReadFields();
                bool isGeoextent = false;
                bool isScale = false;
                if (fields.Length == 3)
                {
                    if (fields[2] == "Geography") { isGeoextent = true; } else if (fields[2] == "Scale_range") { isScale = true; }
                }

                // first elements in combo box should be blank
                dict.Add("", "");
                string desc = "";

                // now process remaining rows
                while (!parser.EndOfData)
                {
                    //Process row
                    fields = parser.ReadFields();
                    if (!dict.ContainsKey(fields[0]))
                    {
                        if(isScale)
                        {
                            desc = string.Format("{0,-35} {1,25}", fields[1], "(" + fields[2].Replace("?", ">=") + ")");
                            dict.Add(fields[0], desc);
                        }
                        else if (isGeoextent)
                        {
                            desc = string.Format("{0,-45} {1,15}", fields[1], "(" + fields[2]+ ")");
                            dict.Add(fields[0],desc);
                        }
                        else
                        {
                            dict.Add(fields[0], fields[1]);
                        }   
                    }                           
                }
            }
            return dict;
        }

        public static Dictionary<string, string> csvToDictionary(string @path, string @selCategory)
        {
            // for theme.csv, need to filter out only values of theme applicable to currently selected value of category
            
            Dictionary<string,string> dict=new Dictionary<string,string>();
            // set up first entry in dictionary (blank, blank) for better display and avoids
            // non graceful error if no themes found for selected category
            dict.Add("", "");
            string desc = "";

            // use TextFieldParser to read in in fields from csv file, one row at a time
            using (TextFieldParser parser = new TextFieldParser(@path, System.Text.Encoding.GetEncoding(1252)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    if (fields != null && fields.Length >=3 && fields[2] == selCategory) 
                    {
                        if (!dict.ContainsKey(fields[0]))
                        {
                            desc = string.Format("{0,-50} {1,10}", fields[1], "(" + fields[2] + ")");
                            dict.Add(fields[0], desc);       
                        } 
                    }
                }
            }           
            // if dict only has one member (the blank, blank that was initalised here), then change
            // to display suitable warning

            if (dict.Count == 1)
            {
                dict[""] = "No themes found for selected category - use custom value";
            }
            return dict;
        }
    }
}