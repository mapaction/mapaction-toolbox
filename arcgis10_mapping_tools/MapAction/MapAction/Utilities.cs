using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MapAction
{
    public class Utilities
    {

        #region Public method createXML
        //Creates an xml given a dictionary of tags and values.  Also pass in the root element, file path and filename.
        public static string createXML(Dictionary<string, string> usDict, string rootElement, string path, string fileName, int numRootElements)
        {
            //set output path and filename
            string pathFileName;
            //check to see if the path is the root of the directory, if so remove additional \
            string root = System.IO.Path.GetPathRoot(path);
            if (root != path)
            {
                pathFileName = @path + "\\" + fileName + ".xml";
                Debug.WriteLine(pathFileName);
            }
            else
            {
                pathFileName = @path + fileName + ".xml";
                Debug.WriteLine(pathFileName);
            }

            // Check that the dictionary we are writing has valid XML values (ie local paths are converted to URIs etc)
            //Dictionary<string, string> sDict = usDict;
            Dictionary<string, string> sDict = sDictFromUsDict(usDict);
            
            String themesStr = "themes";
            //Create and add the root element
            var xml = new XDocument();
            
            //This is a quirk because the ArcGIS 9x tool output xml has 2 root elements
            //This needs to be replicated while the MA website reads the xml in the current way

            XElement topElem = null;  // May not need this.
            if (numRootElements == 2)
            {
                topElem = new XElement("mapdoc");
                xml.Add(topElem);
            }
            var rootElem = new XElement(rootElement);
            if (numRootElements == 2)
            {
                topElem.Add(rootElem);
            }
            else
            {
                xml.Add(rootElem);
            }
            try
            {
                //Add each value pair in the passed dictionary as the elements of the xml doc
                foreach (KeyValuePair<String, String> row in sDict)
                {
                    var element = new XElement(row.Key, row.Value);
                    if (row.Key.Equals(themesStr) == true)
                    {
                        var themesElem = new XElement(themesStr);
                        rootElem.Add(themesElem);
                        string[] themes = row.Value.Split('|');
                        foreach (string theme in themes)
                        {
                            var themeElement = new XElement("theme", theme);
                            themesElem.Add(themeElement);
                        }
                    }
                    else
                    {
                        rootElem.Add(element);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            xml.Save(pathFileName);
            return pathFileName;
        }

        #endregion

        #region Public method createXML
        //Creates an xml given a dictionary of tags and values.  Also pass in the root element, file path and filename.
        public static string createXML(MapAction.OperationConfig operationConfig, string path, string fileName)
        {
            //set output path and filename
            string pathFileName;
            //check to see if the path is the root of the directory, if so remove additional \
            string root = System.IO.Path.GetPathRoot(path);
            if (root != path)
            {
                pathFileName = @path + "\\" + fileName + ".xml";
                Debug.WriteLine(pathFileName);
            }
            else
            {
                pathFileName = @path + fileName + ".xml";
                Debug.WriteLine(pathFileName);
            }

            using (StringWriter stringwriter = new System.IO.StringWriter())
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(operationConfig.GetType());
                System.IO.StreamWriter file = new System.IO.StreamWriter(pathFileName);
                writer.Serialize(file, operationConfig);
                file.Close();
            }
            return pathFileName;
        }

        #endregion

        #region Public method getDataFrameSpatialReference
        //Gets the spatial reference details of a given data frame. Returns a dictionary with the "type", "datum" & "projection".
        //these keys return empty if the dataframe doesn't exist, therefore test "type" != string.empty; on the other end 
        public static Dictionary<string, string> getDataFrameSpatialReference(IMxDocument pMxDoc, string pDataFrameName)
        {
            IMapDocument _mapDoc = (IMapDocument)pMxDoc;
            return getDataFrameSpatialReference(_mapDoc, pDataFrameName);
        }

        public static Dictionary<string, string> getDataFrameSpatialReference(IMapDocument pMapDoc, string pDataFrameName)
        {
            IMap pMap;
            IMap pDataFrame = null;
            string type = string.Empty;
            string datum = string.Empty;
            string projection = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            try
            {
                //Search through the dataframes in the document 
                for (int i = 0; i <= pMapDoc.MapCount - 1; i++)
                {
                    pMap = pMapDoc.Map[i];
                    if (pMap.Name == pDataFrameName)
                    {
                        //assign pDataFrame map where the passed dataframe name exists
                        pDataFrame = pMap;
                        //Determine which ISpatialReference type the map frame is & assign type, datum & projection
                        //variables accordingly
                        ISpatialReference spatialRef = pDataFrame.SpatialReference;
                        if (spatialRef is IGeographicCoordinateSystem)
                        {
                            type = "Geographic";
                            IGeographicCoordinateSystem geoCoord = spatialRef as IGeographicCoordinateSystem;
                            datum = geoCoord.Datum.Name.Remove(0, 2);
                            datum = datum.Replace("_", " ");
                        }
                        else if (spatialRef is IProjectedCoordinateSystem)
                        {
                            type = "Projected";
                            IProjectedCoordinateSystem prjCoord = spatialRef as IProjectedCoordinateSystem;
                            projection = prjCoord.Name;
                            projection = projection.Replace("_", " ");
                            datum = prjCoord.GeographicCoordinateSystem.Name.Remove(0, 4);
                            datum = datum.Replace("_", " ");
                        }
                        else
                        {
                            type = "Unknown";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            //write out the variables to the dictionary, if no frame was found they will be each string.empty.
            dict.Add("type", type);
            dict.Add("datum", datum);
            dict.Add("projection", projection);
            //Return the dictionary
            return dict;
        }

        #endregion

        #region Public method getOperationConfigValues
        public static OperationConfig getOperationConfigValues(string path = null)
        {
            string opCfgFilePath;
            Uri cmfURI;
            var operationConfig = new OperationConfig();
            //Create a dictionary to store the values from the xml
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (path == null)
            {
                //Get the currently set filepath from the ConfigTool settings file
                opCfgFilePath = Properties.Settings.Default.crash_move_folder_path;
            }
            else
            {
                opCfgFilePath = @path;
            }

            cmfURI = new Uri(System.IO.Path.GetDirectoryName(opCfgFilePath), UriKind.Absolute);

            try
            {
                if (File.Exists(@opCfgFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(OperationConfig));
                    System.IO.FileStream fileStream = new System.IO.FileStream(@opCfgFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    operationConfig = (OperationConfig)serializer.Deserialize(fileStream);
                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return operationConfig;
        }
        #endregion

        #region Public method hasWriteAccessToPath
        //Returns a dictionary of the operation_config.xml elements and values
        /// <summary>
        /// ###This method does not correctly check access permissions.  Not sure how best to approach this.###
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool detectWriteAccessToPath(string path)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(@path);
                Debug.WriteLine("Access to path granted.");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("Access to path denied.");
                return false;    
            }
        }
        #endregion

        #region Public method getFileSize
        //Get filesize given a path, return the file size in bytes
        public static long getFileSize(string @fileNamePath)
        {
            if (File.Exists(@fileNamePath))
            {
            FileInfo f_info = new FileInfo(@fileNamePath);
            long fileSize = f_info.Length;
            //change bytes to kilobytes
            //fileSize = fileSize / 1000; removed by gv 08/08/2014
            return fileSize;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Public method getMapFrame
        // Returns IMap object of the map frame with the specified name in the method call
        public static IMap getMapFrame(IMxDocument pMxDoc, string pMapFrameName)
        {
            IMap pSelectedMap = null;
            IMap pMap;
            IMaps pMaps = pMxDoc.Maps;
            // Get the data frame
            //Search through the dataframes in the document 
            for (int i = 0; i <= pMaps.Count - 1; i++)
            {
                pMap = pMaps.get_Item(i);
                if (pMap.Name == pMapFrameName) 
                {
                    //assign pDataFrame map where the passed dataframe name exists
                    pSelectedMap = pMap;
                }
            }

            return pSelectedMap;

        }

        public static IMap getMapFrame(IMapDocument pMapDoc, string pMapFrameName)
        {
            IMap pSelectedMap = null;
            IMap pMap;
            // Get the data frame
            //Search through the dataframes in the document 
            for (int i = 0; i <= pMapDoc.MapCount - 1; i++)
            {
                pMap = pMapDoc.Map[i];
                if (pMap.Name == pMapFrameName)
                {
                    //assign pDataFrame map where the passed dataframe name exists
                    pSelectedMap = pMap;
                }
            }

            return pSelectedMap;

        }
        #endregion

        #region Public method getMapFrameBoundingBox
        // Get the bounding box of the map frame in wgs84 unprojected
        // ### Not not yet implemented, all returned values are wgs84 -> Return the values either in 'native' i.e. incoming coordinate system or convert them to wgs84
        public static Dictionary<string, string> getMapFrameWgs84BoundingBox(IMapDocument pMapDoc, string mapFrameName)
        {
            // Declare and initalise variables
            Dictionary<string, string> dict = new Dictionary<string, string>(); 
            IMap pMap = getMapFrame(pMapDoc, mapFrameName);
            IActiveView pActiveView = pMap as IActiveView;
            IEnvelope2 pEnvelope = pActiveView.Extent as IEnvelope2;

            // Get the spatial reference of the map frame
            // If not Geographic / WGS 84, convert it
            var spatialRefDict = getDataFrameSpatialReference(pMapDoc, mapFrameName);

            if (spatialRefDict["type"] != "Geographic" && spatialRefDict["datum"] != "WGS 1984")
            {
           
                //Convert active view to wgs 84                
                Debug.WriteLine("Reprojecting to wgs84");
                ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference wgs84;
                //GCS to project from 
                IGeographicCoordinateSystem gcs = srFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                wgs84 = gcs;
                wgs84.SetFalseOriginAndUnits(-180, -90, 1000000);
                pEnvelope.Project(wgs84);
            }

            dict.Add("xMin", pEnvelope.XMin.ToString());
            dict.Add("yMin", pEnvelope.YMin.ToString());
            dict.Add("xMax", pEnvelope.XMax.ToString());
            dict.Add("yMax", pEnvelope.YMax.ToString());
            
            return dict;

        }
        #endregion

        #region Public method detectOperationConfig
        //Returns a dictionary of the operation_config.xml elements and values
        public static Boolean detectOperationConfig()
        {
            string path = Properties.Settings.Default.crash_move_folder_path;
            string filepath = path + @"\operation_config.xml";
            //If the file exists in the filepath, add each element and value of the xml file 
            //to the dictionary as key value pairs 
            if (File.Exists(@filepath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Public method getCrashMoveFolderPath()

        public static string getCrashMoveFolderPath()
        {
            return Properties.Settings.Default.crash_move_folder_path;
        }

        #endregion

        #region Public method setCrashMoveFolderPath()

        public static string setCrashMovePathTest(string path)
        {

            if (Directory.Exists(path))
            {
                MapAction.Properties.Settings.Default.crash_move_folder_path = path;
                Properties.Settings.Default.Save();
                return Properties.Settings.Default.crash_move_folder_path;
            }
            else
            {
                return string.Empty;
            }
            
        }

        #endregion

        #region Public method getOperationConfigFilePath()

        public static string getOperationConfigFilePath()
        {
            string fullPath;
            if (detectOperationConfig())
            {
                fullPath = Properties.Settings.Default.crash_move_folder_path + @"\operation_config.xml";
                return fullPath;
            }
            else
            {
                return string.Empty;
            }
            
        }

        #endregion

        /*
         * Creates a new copy of dirtyDict, ensuring that any filepaths in the values are converted
         * to XML safe file:/// type URLs.
         */
        private static Dictionary<string, string> sDictFromUsDict(Dictionary<string, string> dirtyDict)
        {
            //Create a dictionary to store the clean values
            Dictionary<string, string> cleanURLsdict = new Dictionary<string, string>();

            //Add each value pair in the passed dictionary as the elements of the xml doc
            foreach (KeyValuePair<String, String> dirtyRow in dirtyDict)
            {
                if (dirtyRow.Key.Equals("DefaultPathToExportDir", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Uri absURI = new Uri(@dirtyRow.Value, UriKind.Absolute);
                    //cleanURLsdict.Add(dirtyRow.Key, absURI.AbsoluteUri);

                    //Uri absExportURI = new Uri(usEle.Value.ToString(), UriKind.Absolute);
                    ////absExportURI.MakeRelativeUri(cmfURI)
                    //dict.Add(usEle.Name.ToString(), cmfURI.MakeRelativeUri(absExportURI).ToString());

                    cleanURLsdict.Add(dirtyRow.Key, relPathFromAbs(dirtyRow.Value));
                }
                else
                {
                    cleanURLsdict.Add(dirtyRow.Key, dirtyRow.Value);
                }
            }
            return cleanURLsdict;
        }

        /*
         * Returns a URI path relative to getOperationConfigFilePath() 
         */
        public static string relPathFromAbs(string absPath)
        {
            //MessageBox.Show("crash_move_folder_path = " + Properties.Settings.Default.crash_move_folder_path);
            Uri cmfURI = new Uri(@Properties.Settings.Default.crash_move_folder_path + @"\", UriKind.Absolute);
            Uri absURI = new Uri(@absPath, UriKind.Absolute);
            //MessageBox.Show("cmfURI = " + cmfURI.ToString());
            //MessageBox.Show("absURI = " + absURI.ToString());

            return cmfURI.MakeRelativeUri(absURI).ToString();
        }

        /*
        * Returns an absolute URI path by prefixing getOperationConfigFilePath() to the argument
        */
        public static string absPathFromRel(string relPath)
        {
            Uri cmfURI = new Uri(@Properties.Settings.Default.crash_move_folder_path + @"\", UriKind.Absolute);
            Uri relURI = new Uri(@relPath, UriKind.Relative);

            return Uri.UnescapeDataString(new Uri(cmfURI, relURI).LocalPath);
        }

        public static void ResizeImageFile(string imageFile, string outputFile, double scaleFactor)
        {
            // from http://stackoverflow.com/a/11138086
            using (var srcImage = Image.FromFile(imageFile))
            {
                var newWidth = (int)(srcImage.Width * scaleFactor);
                var newHeight = (int)(srcImage.Height * scaleFactor);
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    // this will save to png by default.
                    newImage.Save(outputFile);
                }
            }
        }

        /// <summary>
        /// Allows a GP toolbox to be added to the output of the VS project. Using this method it can 
        /// be referenced using a reletive path from the .dll.
        /// </summary>
        /// <returns>The runtime path to the mapbook_export_tools GeoProcessing Toolbox</returns>
        public static String getExportGPToolboxPath()
        {
            String assemblyPath, assemblyDir;
            String tbxStub = @"mapbook_export_tool\mapbook_export_tools.tbx";
            String tbxPath;

            assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            assemblyDir = System.IO.Path.GetDirectoryName(assemblyPath);
            tbxPath = System.IO.Path.Combine(assemblyDir, tbxStub);
            System.Console.WriteLine(tbxPath);
            return tbxPath;
        }

        #region Public method getLanguageConfigFilePath()

        public static string getLanguageConfigFilePath()
        {
            string fullPath;
            if (detectLanguageConfig())
            {
                fullPath = Properties.Settings.Default.crash_move_folder_path + @"\language_config.xml";
                return fullPath;
            }
            else
            {
                return string.Empty;
            }

        }

        #endregion

        #region Public method detectLanguageConfig
        //Returns a dictionary of the operation_config.xml elements and values
        public static Boolean detectLanguageConfig()
        {
            string path = Properties.Settings.Default.crash_move_folder_path;
            string filepath = path + @"\language_config.xml";
            //If the file exists in the filepath, add each element and value of the xml file 
            //to the dictionary as key value pairs 
            if (File.Exists(@filepath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Public method getLanguageConfigValues
        //Returns a List of the language_config.xml elements and values
        public static List<MapAction.LanguageConfig> getLanguageConfigValues(string path = null)
        {
            bool languageDictionaryInitialised = false;
            string opCfgFilePath;
            Uri cmfURI;

            List<MapAction.LanguageConfig> languageDictionary = new List<MapAction.LanguageConfig>();

            //Create a dictionary to store the values from the xml
            if (path == null)
            {
                //Get the currently set filepath from the ConfigTool settings file
                opCfgFilePath = Properties.Settings.Default.crash_move_folder_path;
            }
            else
            {
                opCfgFilePath = @path;
            }

            cmfURI = new Uri(System.IO.Path.GetDirectoryName(opCfgFilePath), UriKind.Absolute);

            //If the file exists in the filepath, add each element and value of the xml file 
            //to the dictionary as key value pairs 

            try
            {
                if (File.Exists(@opCfgFilePath))
                {
                    XmlDocument doc = new XmlDocument();

                    doc.Load(@opCfgFilePath);

                    XmlNodeList languages = doc.GetElementsByTagName("language");
                    string languageName = "";
                    for (int i = 0; i < languages.Count; i++)
                    {
                        Dictionary<string, string> languageDict = new Dictionary<string, string>();
                        XmlNode rootNode = languages[i];
                        //Debug.WriteLine(languages[i].Name.ToString()); // "language"
                        for (int a = 0; a < rootNode.Attributes.Count; a++)
                        {
                            languageName = rootNode.Attributes[a].Value.ToString();
                            //Debug.WriteLine(rootNode.Attributes[a].Value.ToString()); // English
                        }
                        if (rootNode.HasChildNodes)
                        {
                            for (int n = 0; n < rootNode.ChildNodes.Count; n++)
                            {
                                languageDict.Add(rootNode.ChildNodes[n].Name.ToString(), rootNode.ChildNodes[n].InnerText.ToString());
                                Debug.WriteLine(rootNode.ChildNodes[n].Name + " = " + rootNode.ChildNodes[n].InnerText); // This is the content of the labels ("Created", etc).
                            }
                        }
                        if (languageDictionaryInitialised == false)
                        {
                            languageDictionaryInitialised = true;
                        }
                        LanguageConfig languageConfig = new LanguageConfig(languageName, languageDict);
                        languageDictionary.Add(languageConfig);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            if (languageDictionaryInitialised == false)
            {
                string defaultLanguageName = "English";
                Dictionary<string, string> defaultLanguageDict = new Dictionary<string, string>
                {
                     { "create_date_time_label",  "Created" },
                     { "data_sources_label",      "<bol>Data Sources</bol>" },
                     { "mxd_name_label",          "Map Document" },
                     { "spatial_reference_label", "Projection & Datum" },
                     { "glide_no_label",          "GLIDE Number" },
                     { "map_producer",            "Produced by MapAction - www.mapaction.org\n<ita>country@mapaction.org</ita>" },
                     { "donor_credit",            "Supported by <bol>UK Department for International Development</bol> and the <bol>Ministry of Foreign Affairs of the Netherlands</bol>" },
                     { "disclaimer",              "The depiction and use of boundaries, names and associated data shown here do not imply endorsement or acceptance by MapAction." }

                };
                LanguageConfig languageConfig = new LanguageConfig(defaultLanguageName, defaultLanguageDict);
                languageDictionary.Add(languageConfig);
            }
            return languageDictionary;
        }
        #endregion
    }
}