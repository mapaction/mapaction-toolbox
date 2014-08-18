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

namespace MapAction
{
    public static class MapExport
    {
        
        #region Public method exportImage

        // Exports a given page layout or map frame to a variety of image formats, returns the image file path
        public static string exportImage(IMxDocument pMxDoc, string exportType, string dpi, string pathDocumentName, string mapFrameName)
        {
            // Define the activeView as either the page layout or the map frame
            // If the mapFrameName variable is null then the activeView is the page, otherwise it is set to the map frame name specified
            IMap pMap;
            IActiveView pActiveView = null;
            IMaps pMaps = pMxDoc.Maps;
            // Also construct output filename depending on the activeView / mapFrame input
            string pathFileName = string.Empty;

            if (mapFrameName == null)
            {
                pActiveView = pMxDoc.ActiveView;
                pathFileName = @pathDocumentName + "-" + dpi.ToString() + "dpi." + exportType;
            }
            else if (mapFrameName != null && PageLayoutProperties.detectMapFrame(pMxDoc, mapFrameName))
            {
                for (int i = 0; i <= pMaps.Count - 1; i++)
                {
                    pMap = pMaps.get_Item(i);
                    if (pMap.Name == mapFrameName)
                    {
                        pActiveView = pMap as IActiveView;
                    }
                }
                pathFileName = @pathDocumentName + "-mapframe-" + dpi.ToString() + "dpi." + exportType;
            }
            else
            {
                return null;
            }

            //Declare the export variable and set the file and path from the parameters
            IExport docExport;
            //parameter check
            if (pActiveView == null)
            {
                return null;
            }
            // The Export*Class() type initializes a new export class of the desired type.
            if (exportType == "pdf")
            {
                docExport = new ExportPDFClass();
            }
            else if (exportType == "eps")
            {
                docExport = new ExportPSClass();
            }
            else if (exportType == "ai")
            {
                docExport = new ExportAIClass();
            }
            else if (exportType == "bmp")
            {
                docExport = new ExportBMPClass();
            }
            else if (exportType == "tiff")
            {
                docExport = new ExportTIFFClass();
            }
            else if (exportType == "svg")
            {
                docExport = new ExportSVGClass();
            }
            else if (exportType == "png")
            {
                docExport = new ExportPNGClass();
            }
            else if (exportType == "gif")
            {
                docExport = new ExportGIFClass();
            }
            else if (exportType == "emf")
            {
                docExport = new ExportEMFClass();
            }
            else if (exportType == "jpeg")
            {
                IExportJPEG m_export;
                docExport = new ExportJPEGClass();
                if (docExport is IExportJPEG)
                {
                    m_export = (IExportJPEG)docExport;
                    m_export.ProgressiveMode = false;   //hardcoded progressive mode value here
                    m_export.Quality = 80;              //hardcoded quality value here
                    docExport = (IExport)m_export;
                }
            }
            else
            {
                return pathFileName;
            }

            docExport.ExportFileName = pathFileName;

            // Because we are exporting to a resolution that differs from screen 
            // resolution, we should assign the two values to variables for use 
            // in our sizing calculations
            System.Int32 screenResolution = 96;
            System.Int32 outputResolution = Convert.ToInt32(dpi);

            docExport.Resolution = outputResolution;

            // If input type is map frame calculate the export rectangle

            tagRECT exportRECT; // This is a structure
            exportRECT.left = 0;
            exportRECT.top = 0;
            exportRECT.right = pActiveView.ExportFrame.right * (outputResolution / screenResolution);
            exportRECT.bottom = pActiveView.ExportFrame.bottom * (outputResolution / screenResolution);
            

            // Set up the PixelBounds envelope to match the exportRECT
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            docExport.PixelBounds = envelope;

            try
            {
                System.Int32 hDC = docExport.StartExporting();
                pActiveView.Output(hDC, (System.Int16)docExport.Resolution, ref exportRECT, null, null); // Explicit Cast and 'ref' keyword needed 
                docExport.FinishExporting();
                docExport.Cleanup();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error writing to file, probably a file permissions error.  Check the exception message below for details.");
                Debug.WriteLine(e.Message);
            }

            //Return the path of the file
            return pathFileName;
        }
        #endregion

        #region Public method exportMapFrameKmlAsRaster
        // Export map frame kml as raster
        public static void exportMapFrameKmlAsRaster(IMxDocument pMxDoc, string dataFrame, string filePathName, string scale)
        {          
            IGeoProcessor2 gp = new GeoProcessorClass();
            IVariantArray parameters = new VarArrayClass();
            
            // Get the mxd path to pass as the first variable
            IDocumentInfo2 docInfo = pMxDoc as IDocumentInfo2;
            string path = docInfo.Path;

            // Get the bounding box of the map frame
            //############### This function needs to be updated to incorporate projected map frames, it currently only works if the frame is wgs84 #############
            var dict = Utilities.getMapFrameWgs84BoundingBox(pMxDoc, dataFrame);
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

            // Execute the tool
            try
            {
                Debug.WriteLine("Starting KML output..");
                gp.Execute("MapToKML_conversion", parameters, null);
                Debug.WriteLine("Finished KML output");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
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
        //Create a zip file of input file paths
        public static Boolean createZip(Dictionary<string,string> dictPaths)
        {
            //set the output filename and directory from the input files
            string fileName = System.IO.Path.GetFileNameWithoutExtension(dictPaths["xml"]);
            string zipFileName = fileName + ".zip";
            string savePath = @System.IO.Path.GetDirectoryName(dictPaths["xml"]) + @"\" + zipFileName;
            Debug.WriteLine("save path: " + savePath);
            
            //try
            //{
            //    using (ZipFile zip = new ZipFile())
            //    {
            //        foreach (var i in dictPaths)
            //        {
            //            zip.AddFile(@i.Value, @"\");
            //        }
            //        zip.Save(@savePath);
            //    }
            //}
            //catch (Exception e_zip)
            //{
            //    Debug.WriteLine("Error writing zip file");
            //    Debug.WriteLine(e_zip.Message);
            //    return false;
            //}
 
            ////////////////////////////////////
            // 7zip version
            ////////////////////////////////////
            try
            {
                string zipExePath = get7zipExePath();

                if (!String.IsNullOrEmpty(zipExePath))
                {

                    Process zipProc = new Process();
                    // Configure the process using the StartInfo properties.
                    zipProc.StartInfo.FileName = zipExePath;
                    zipProc.StartInfo.Arguments = String.Format("a -y -tzip {0} {1} {2} {3}", savePath, dictPaths["xml"], dictPaths["jpeg"], dictPaths["pdf"]);
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
