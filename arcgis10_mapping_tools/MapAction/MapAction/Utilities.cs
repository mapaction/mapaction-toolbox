using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
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
using ESRI.ArcGIS.Geoprocessing;
using System.Reflection;

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
                pathFileName = System.IO.Path.Combine(@path, fileName + ".xml");
            }
            else
            {
                pathFileName = @path + fileName + ".xml";
            }
            Debug.WriteLine(pathFileName);

            // Check that the dictionary we are writing has valid XML values (ie local paths are converted to URIs etc)
            //Dictionary<string, string> sDict = usDict;
            Dictionary<string, string> sDict = sDictFromUsDict(usDict);
            
            const String themesStr = "themes";
            const String countriesStr = "countries-iso3";
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
                    XElement element  = new XElement(row.Key, row.Value);
                    
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
                    else if (row.Key.Equals(countriesStr) == true)
                    {
                        var countriesIso3Elem = new XElement(countriesStr);
                        rootElem.Add(countriesIso3Elem);
                        string[] countriesIso3 = row.Value.Split('|');
                        foreach (string countryIso3 in countriesIso3)
                        {
                            var countryIso3Element = new XElement("country-iso3", countryIso3);
                            countriesIso3Elem.Add(countryIso3Element);
                        }
                    }
                    else
                    {
                        if (element != null)
                        {
                            rootElem.Add(element);
                        }
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
                pathFileName = System.IO.Path.Combine(@path, fileName + ".xml");
            }
            else
            {
                pathFileName = @path + fileName + ".xml";
            }
            Debug.WriteLine(pathFileName);

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
        //Returns a dictionary of the operation_config.xml elements and values
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

        public static string getScale(IMapDocument pMapDoc, string mapFrameName)
        {
            string scale;
            IMap pMap = getMapFrame(pMapDoc, mapFrameName);
            IActiveView pActiveView = pMap as IActiveView;
            IEnvelope2 pEnvelope = pActiveView.Extent as IEnvelope2;

            long temp_scale = Convert.ToInt64(pMap.MapScale);

            scale = "1: " + string.Format("{0:n0}", temp_scale);

            return scale;
        }

        public static string getPageSize(IMapDocument pMapDoc, string mapFrameName)
        {
            string pageSize = null;
            IPageLayout pLayout = pMapDoc.PageLayout;
            string pageFormId = pLayout.Page.FormID.ToString();

            // Need to translate from ESRI form to readable form
            Dictionary<string, string> pageSizes = new Dictionary<string, string>();
            pageSizes.Add("esriPageFormLetter", "Letter");
            pageSizes.Add("esriPageFormLegal", "Legal");
            pageSizes.Add("esriPageFormTabloid", "Tabloid");
            pageSizes.Add("esriPageFormC", "C");
            pageSizes.Add("esriPageFormD", "D");
            pageSizes.Add("esriPageFormE", "E");
            pageSizes.Add("esriPageFormA5", "A5");
            pageSizes.Add("esriPageFormA4", "A4");
            pageSizes.Add("esriPageFormA3", "A3");
            pageSizes.Add("esriPageFormA2", "A2");
            pageSizes.Add("esriPageFormA1", "A1");
            pageSizes.Add("esriPageFormA0", "A0");
            pageSizes.Add("esriPageFormCUSTOM", "Custom");
            pageSizes.Add("esriPageFormSameAsPrinter", "Same as printer");
            foreach (var i in pageSizes)
            {
                if (pageFormId == i.Key)
                {
                    pageSize = i.Value;
                }
            }
            return pageSize;
        }

        public static Dictionary<string, string> getDataframeProperties(IMapDocument pMapDoc, string mapFrameName)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>(); 
            IMap pMap = getMapFrame(pMapDoc, mapFrameName);
            IActiveView pActiveView = pMap as IActiveView;
            IEnvelope2 pEnvelope = pActiveView.Extent as IEnvelope2;

            dict.Add("xMin", Math.Round(pEnvelope.XMin, 2).ToString());
            dict.Add("yMin", Math.Round(pEnvelope.YMin, 2).ToString());
            dict.Add("xMax", Math.Round(pEnvelope.XMax, 2).ToString());
            dict.Add("yMax", Math.Round(pEnvelope.YMax, 2).ToString());

            return dict;
        }


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

            if (spatialRefDict["type"] != "Geographic" || spatialRefDict["datum"] != "WGS 1984")
            {
           
                //Convert active view to wgs 84                
                Debug.WriteLine("Reprojecting to wgs84");
                ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironment();
                ISpatialReference wgs84;
                //GCS to project from 
                IGeographicCoordinateSystem gcs = srFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                wgs84 = gcs;
                wgs84.SetFalseOriginAndUnits(-180, -90, 1000000);
                pEnvelope.Project(wgs84);
            }
            dict.Add("xMin", Math.Round(pEnvelope.XMin, 2).ToString());
            dict.Add("yMin", Math.Round(pEnvelope.YMin, 2).ToString());
            dict.Add("xMax", Math.Round(pEnvelope.XMax, 2).ToString());
            dict.Add("yMax", Math.Round(pEnvelope.YMax, 2).ToString());

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

        // Map Data Repository (MDR URL Root)
        public static string getMDRUrlRoot()
        {
            return Properties.Settings.Default.mdr_url_root;
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
            Uri cmfURI = new Uri(@Properties.Settings.Default.crash_move_folder_path + @"\", UriKind.Absolute);
            Uri absURI = new Uri(@absPath, UriKind.Absolute);
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
            const String tbxStub = @"mapbook_export_tool\mapbook_export_tools.tbx";
            String tbxPath;

            assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            assemblyDir = System.IO.Path.GetDirectoryName(assemblyPath);
            tbxPath = System.IO.Path.Combine(assemblyDir, tbxStub);
            return tbxPath;
        }

        public static string GenerateQRCode(string url)
        {
            string qrPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
            IGeoProcessor2 gp = new GeoProcessor() as IGeoProcessor2;
            gp.AddToolbox(Utilities.getExportGPToolboxPath());
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;

            IVariantArray parameters = new VarArray();
            parameters.Add(url);
            parameters.Add(qrPath);

            object sev = null;
            IGeoProcessorResult2 pyResult = null;
            try
            {
                pyResult = (IGeoProcessorResult2)gp.Execute("generateQRCode", parameters, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string errorMsgs = gp.GetMessages(ref sev);
                Console.WriteLine(errorMsgs);
                throw;
            }
            return qrPath;
        }

        #region Public method getLanguageCodeValues
        //Returns a List of the countries_config.xml elements and values

        public static MapAction.LanguageCodeLookup getLanguageCodeValues(string path = null)
        {
            string opCfgFilePath;
            Uri cmfURI;
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
            LanguageCodeLookup languageCodeLookup = new LanguageCodeLookup();

            try
            {
                if (File.Exists(@opCfgFilePath))
                {
                    XmlReader xmlReader = XmlReader.Create(@opCfgFilePath);
                    while (xmlReader.Read())
                    {
                        if (xmlReader.Name == "code")
                        {
                            LanguageCode languageCode = new LanguageCode(xmlReader.GetAttribute("a2"),
                                                                         xmlReader.GetAttribute("a3b"),
                                                                         xmlReader.GetAttribute("a3t"),
                                                                         xmlReader.GetAttribute("a3h"),
                                                                         xmlReader.GetAttribute("lang"));
                            languageCodeLookup.add(languageCode);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return languageCodeLookup;
        }
        #endregion    


        #region Public method getToolboxConfig
        //Returns a the MapAction Toolbar Configuration object

        public static MapActionToolbarConfig getToolboxConfig(string path = null)
        {
            const string MapActionToolbarsConfigFileName = "MapActionToolbarsConfig.xml";

            MapActionToolbarConfig mapActionToolbarConfig = new MapActionToolbarConfig();

            string mapActionToolboxConfigPath;
            if (path == null)
            {
                //Get the currently set filepath from the ConfigTool settings file
                mapActionToolboxConfigPath = Properties.Settings.Default.crash_move_folder_path;
            }
            else
            {
                mapActionToolboxConfigPath = @path;
            }
            try
            {
                if (File.Exists(System.IO.Path.Combine(mapActionToolboxConfigPath, MapActionToolbarsConfigFileName))) 
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(MapActionToolbarConfig));
                    System.IO.StreamReader file = new System.IO.StreamReader(System.IO.Path.Combine(mapActionToolboxConfigPath, MapActionToolbarsConfigFileName));
                    mapActionToolbarConfig = (MapActionToolbarConfig)reader.Deserialize(file);
                    file.Close();
                }
                else
                {
                    // Use default from resource in code-base 
                    System.Reflection.Assembly assembly = Assembly.GetExecutingAssembly();
                    using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".Resources." + MapActionToolbarsConfigFileName))
                    using (StreamReader file = new StreamReader(stream))
                    {
                        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(MapActionToolbarConfig));
                        mapActionToolbarConfig = (MapActionToolbarConfig)reader.Deserialize(file);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return mapActionToolbarConfig;
        }
        #endregion

        #region Public method getLanguageConfigValues
        //Returns the Language Configuration

        public static List<MapAction.LanguageConfig> getLanguageConfigValues(string path = null)
        {
            const string LanguageConfigFileName = "language_config.xml";
            List<MapAction.LanguageConfig> languageDictionary = new List<MapAction.LanguageConfig>();
            string configPath;
            if (path == null)
            {
                //Get the currently set filepath from the ConfigTool settings file
                configPath = Properties.Settings.Default.crash_move_folder_path;
            }
            else
            {
                configPath = @path;
            }
            try
            {
                if (File.Exists(System.IO.Path.Combine(configPath, LanguageConfigFileName)))
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(MapActionToolbarConfig));
                    System.IO.StreamReader file = new System.IO.StreamReader(System.IO.Path.Combine(configPath, LanguageConfigFileName));

                    XmlDocument doc = new XmlDocument();
                    doc.Load(file);

                    XmlNodeList languages = doc.GetElementsByTagName("language");
                    string languageName = "";
                    for (int i = 0; i < languages.Count; i++)
                    {
                        Dictionary<string, string> languageDict = new Dictionary<string, string>();
                        XmlNode rootNode = languages[i];
                        for (int a = 0; a < rootNode.Attributes.Count; a++)
                        {
                            languageName = rootNode.Attributes[a].Value.ToString();
                        }
                        if (rootNode.HasChildNodes)
                        {
                            for (int n = 0; n < rootNode.ChildNodes.Count; n++)
                            {
                                languageDict.Add(rootNode.ChildNodes[n].Name.ToString(), rootNode.ChildNodes[n].InnerText.ToString());
                            }
                        }
                        LanguageConfig languageConfig = new LanguageConfig(languageName, languageDict);
                        languageDictionary.Add(languageConfig);
                    }
                    file.Close();
                }
                else
                {
                    // Use default from resource in code-base 
                    System.Reflection.Assembly assembly = Assembly.GetExecutingAssembly();
                    using (Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".Resources." + LanguageConfigFileName))
                    using (StreamReader file = new StreamReader(stream))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(file);

                        XmlNodeList languages = doc.GetElementsByTagName("language");
                        string languageName = "";
                        for (int i = 0; i < languages.Count; i++)
                        {
                            Dictionary<string, string> languageDict = new Dictionary<string, string>();
                            XmlNode rootNode = languages[i];
                            for (int a = 0; a < rootNode.Attributes.Count; a++)
                            {
                                languageName = rootNode.Attributes[a].Value.ToString();
                            }
                            if (rootNode.HasChildNodes)
                            {
                                for (int n = 0; n < rootNode.ChildNodes.Count; n++)
                                {
                                    languageDict.Add(rootNode.ChildNodes[n].Name.ToString(), rootNode.ChildNodes[n].InnerText.ToString());
                                }
                            }
                            LanguageConfig languageConfig = new LanguageConfig(languageName, languageDict);
                            languageDictionary.Add(languageConfig);
                        }
                        file.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return languageDictionary;
        }
        #endregion
    }
}