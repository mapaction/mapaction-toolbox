using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MapAction;




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

            _rawPath = MapAction.Utilities.getCrashMoveFolderPath();
            _path = _rawPath + @"\GIS\2_Active_Data\200_data_name_lookup\";
            
            Debug.WriteLine("returned path: " + _path);
            
            return _path;

        }

        public static Boolean checkPathToLookupCSV()
        {
            
            string _path = ConstructLayerName.pathToLookupCSV() + @"\geoextent.csv";

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
