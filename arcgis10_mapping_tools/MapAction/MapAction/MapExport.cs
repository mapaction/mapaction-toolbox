using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geodatabase;

namespace MapAction
{
    public static class MapExport
    {
        #region Public method exportMapFrameKmlAsRaster
        // Export map frame kml as raster
        public static void exportMapFrameKmlAsRaster(IMapDocument pMapDoc, string dataFrame, string filePathName, string scale, string kmlresolutiondpi)
        {
            IGeoProcessor2 gp = new GeoProcessor() as IGeoProcessor2;
            IVariantArray parameters = new VarArray();
            bool oldAddSetting = gp.AddOutputsToMap;
            gp.AddOutputsToMap = false;
            // Get the mxd path to pass as the first variable
            string path = pMapDoc.DocumentFilename;
            
            // Get the bounding box of the map frame
            //############### This function needs to be updated to incorporate projected map frames, it currently only works if the frame is wgs84 #############
            var dict = Utilities.getMapFrameWgs84BoundingBox(pMapDoc, dataFrame);
            string boundingBox = dict["xMin"] + " " + dict["yMin"] + " " + dict["xMax"] + " " + dict["yMax"];
            Debug.WriteLine("Bounding box: " + boundingBox);

            Debug.WriteLine("mxd path: " + path);
            // Add parameters 
            parameters.Add(@path);
            parameters.Add(dataFrame);
            parameters.Add(@filePathName);
            parameters.Add(scale);
            parameters.Add("COMPOSITE");
            parameters.Add(false);
            parameters.Add(boundingBox);
            parameters.Add(""); // Image Size
            parameters.Add(kmlresolutiondpi);
            // Previous attempt to Hardcode the resolution pending fix to get it from the form properties
            // parameters.Add("300");
            // This key is not in dictionary. dict contains values pertaining to bounding box only.
            // It isn't the dictionary from frmExportMain.getExportToollValues!!
            // Suggestion 1: Use dict.TryGetValue
            // Suggestion 2: Don't use magic strings; define a class to hold the form properties, 
            // then we'd know from intellisense that this wouldn't work...
            //parameters.Add(dict["kmlresolutiondpi"]);
            // Execute the tool
            try
            {
                // Add a whole load of debugging info here;
                // Trying to detirmine why the KML doesn't get exported when unit tested.
                System.Console.WriteLine("Starting KML output..");
                //String settingsFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), @"geoprocessing_settingsfile.xml");
                //System.Console.WriteLine(String.Format("saving settingsFile to {0}", settingsFile));
                //gp.SaveSettings(settingsFile);
                IGeoProcessorResult geoProcessorResult = gp.Execute("MapToKML_conversion", parameters, null);
                IGPMessages results = geoProcessorResult.GetResultMessages();
                IGPMessage message;
                for (int i=0; i<results.Count; i++){
                    message = results.GetMessage(i);
                    System.Console.WriteLine(String.Format("ErrorCode: {0}\t  Type: {1}\t  Description: {2}", message.ErrorCode, message.Type, message.Description));
                }
                System.Console.WriteLine("Finished KML output");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                System.Console.WriteLine(e);
            }
            // does changing it affect general ArcMap environment? probably not but 
            // I can't remember so put it back how it was to be on the safe side
            gp.AddOutputsToMap = oldAddSetting;
        }
        #endregion

        private static string get7zipExePath()
        {
            string[] possiblePaths = new[] {@"C:\Program Files\7-Zip\7z.exe", @"C:\Program Files (x86)\7-Zip\7z.exe"};
            string rtnVal;
            rtnVal = String.Empty;
            
            System.IO.FileInfo possibleFI;

            foreach (string p in possiblePaths)
            {
                possibleFI = new System.IO.FileInfo(p);
                if (possibleFI.Exists)
                {
                    rtnVal = p;
                }
            }
            return rtnVal;
        }

        #region Public method createZip
        /// <summary>
        /// Create a zip file of the three exported files (xml, jpeg, pdf) required for a MA export
        /// </summary>
        /// <param name="dictPaths">
        /// Dictionary with string keys 'xml', 'jpeg', and 'pdf' and string values giving the file path 
        /// of the respective exported file, to be added to the zip.
        /// (TODO: make a proper struct or class to represent and enforce these three items of a MA export)
        /// (TODO: Currently this does not do anything with the emf file.... is this deliberate? the client code
        /// seems to expect that it would do!)
        /// </param>
        /// <returns>
        /// Boolean, false if an exception occurred and true otherwise. 
        /// </returns>
        /// <remarks>
        /// The zipping is run by an external 7zip process 
        /// (TODO figure out why, what was wrong with zipfile?) and so a True return doesn't necessarily indicate 
        /// that 7zip completed successfully!
        ///</remarks>
        public static Boolean createZip(Dictionary<string,string> dictPaths)
        {
            //set the output filename and directory from the input files
            string fileName = System.IO.Path.GetFileNameWithoutExtension(dictPaths["xml"]);
            string zipFileName = fileName + ".zip";
            string savePath = @System.IO.Path.GetDirectoryName(dictPaths["xml"]) + @"\" + zipFileName;
            Debug.WriteLine("save path: " + savePath);
 
            ////////////////////////////////////
            // 7zip version
            ////////////////////////////////////
            try
            {
                string zipExePath = get7zipExePath();
                // or see http://www.dotnetperls.com/7-zip for embedding it

                if (!String.IsNullOrEmpty(zipExePath))
                {
                    Process zipProc = new Process();
                    // Configure the process using the StartInfo properties.
                    zipProc.StartInfo.FileName = zipExePath;
                    string args = String.Format("a -y -tzip {0} {1} {2} {3} {4}",
                        quotePath(savePath), 
                        quotePath(dictPaths["xml"]), 
                        quotePath(dictPaths.FirstOrDefault(kvp=>kvp.Key == "jpeg").Value), // ["jpeg"], 
                        quotePath(dictPaths.FirstOrDefault(kvp => kvp.Key == "pdf").Value), //["pdf"]);
                        quotePath(dictPaths.FirstOrDefault(kvp=>kvp.Key == "png_thumbnail_zip").Value))
                    ;
                    zipProc.StartInfo.Arguments = args;
                    zipProc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    zipProc.Start();
                    zipProc.WaitForExit();// Waits here for the process to exit.
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error writing zip file");
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// Adds quotes to a file path if required.
        /// 
        /// If the `path` contains either a " " (space char) or a wildcard `*` or `?` then quotes 
        /// are added.
        /// 
        /// TODO:
        /// At present it doesn't handle the case were there are already quotes arround the name.
        /// </summary>
        /// <param name="mypath"></param>
        /// <returns></returns>
        private static string quotePath(string mypath){
            // (path.Contains(" ")) ? "\"" + path + "\"" : path
            return (mypath.IndexOfAny(" *".ToCharArray()) >= 0) ? '"' + mypath + '"' : mypath;
        }

        #region Public method openExplorerDirectory
        //Open windows explorer to the directory passed
        public static void openExplorerDirectory(string path)
        {
            try
            {
                if (Directory.Exists(@path))
                {
                    Process.Start("explorer.exe", @path);
                }
                else
                {
                    MessageBox.Show("Path doesn't exist", "Invalid path");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Cannot open export folder in explorer");
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
        #endregion
    }
}
