using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MapAction;
using System.Windows.Forms;




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
            
            //Debug.WriteLine("returned path: " + _path);
            
            //return _path;

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
            
            var dict = File.ReadAllLines(path).Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);  
            return dict;

        }
        

    }
}
