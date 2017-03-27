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

namespace MapAction
{
    /// <summary>
    /// This is a equiv of enum for strings.
    /// 
    /// </summary>
    public enum MapElementNames
    {
        title,
        summary,
        data_sources,
        map_no,
        mxd_name,
        spatial_reference,
        scale,
        glide_no,
        disclaimer,
        donor_credit,
        map_producer,
        timezone
    }
        
  
    public class MapActionMapTemplateException : Exception
    {
        /// <summary>
        /// The path to the MXD file which is the subject of the exception.
        /// </summary>
        public string mxdPath { get { return m_mxdPath;  } }
        private readonly string m_mxdPath;

        /// <summary>
        /// Specifies whether the named Map Frame was found in the MXD and if that is the 
        /// cause of the Exception being raised.
        /// </summary>
        public bool isMapFrameMissing { get { return m_isMapFrameMissing; } }
        private readonly bool m_isMapFrameMissing;


        /// <summary>
        /// This lists any MapElements which were not found in the MXD. Has null value if
        /// all MapElements are present.
        /// </summary>
        public List<MapElementNames> missingMapElements { get { return m_missingMapElements; } }
        private readonly List<MapElementNames> m_missingMapElements;

        /// <summary>
        /// This lists any duplicate map elements found in the MXD. Has null value if no
        /// duplicate MapElements are present
        /// </summary>
        public List<MapElementNames> duplicateMapElements { get { return m_duplicateMapElements; } }
        private readonly List<MapElementNames> m_duplicateMapElements;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The base Exception Class message.</param>
        /// <param name="inner">The base Exception Class innerException property. May be null</param>
        /// <param name="mxdPath">The path to the MXD file which is the subject of the exception</param>
        /// <param name="isMapFrameMissing">Specifies whether the named Map Frame was found in the MXD 
        /// and if that is the cause of the Exception being raised</param>
        /// <param name="missingMapElements">A List of MapElements which were not found in the MXD. Can
        /// be null if all MapElements are present</param>
        /// <param name="duplicateMapElements">A List of duplicate MapElements which were found in the 
        /// MXD. Can be null if no duplicate MapElements are present</param>
        internal MapActionMapTemplateException(
            string message, 
            Exception inner,
            string mxdPath,
            bool isMapFrameMissing, 
            List<MapElementNames> missingMapElements, 
            List<MapElementNames> duplicateMapElements)
            : base(message, inner)
        {
            this.m_mxdPath = mxdPath;
            this.m_isMapFrameMissing = isMapFrameMissing;
            this.m_missingMapElements = missingMapElements;
            this.m_duplicateMapElements = duplicateMapElements;
        }

        /// <summary>
        /// This constructor is best used when needing to specify missingMapElements and duplicateMapElements. The named MapFrame is assumed to be present.
        /// </summary>
        /// <param name="message">The base Exception Class message.</param>
        /// <param name="mxdPath">The path to the MXD file which is the subject of the exception</param>
        /// <param name="isMapFrameMissing">Specifies whether the named Map Frame was found in the MXD 
        /// and if that is the cause of the Exception being raised</param>
        internal MapActionMapTemplateException(string message, string mxdPath, bool isMapFrameMissing)
            : this(message, null, mxdPath, isMapFrameMissing, null, null)
        {}

        /// <summary>
        /// This constructor is best used when needing to specify missingMapElements and duplicateMapElements. The named MapFrame is assumed to be present.
        /// </summary>
        /// <param name="message">The base Exception Class message.</param>
        /// <param name="mxdPath">The path to the MXD file which is the subject of the exception</param>
        /// <param name="missingMapElements">>A List of MapElements which were not found in the MXD. Can
        /// be null if all MapElements are present</param>
        /// <param name="duplicateMapElements">A List of duplicate MapElements which were found in the 
        /// MXD. Can be null if no duplicate MapElements are present</param>
        internal MapActionMapTemplateException(string message, string mxdPath, List<MapElementNames> missingMapElements, List<MapElementNames> duplicateMapElements)
            : this(message, null, mxdPath, false, missingMapElements, duplicateMapElements)
        {}
    }

    public static class PageLayoutProperties
    {
        #region public method getDataframeProperties
        //Return the scale, spatial reference system and extent properties of a given data frame
        public static Dictionary<string, string> getDataframeProperties(IMxDocument pMxDoc, string pFrameName)
        {

            // Create and initialise variables 
            IPageLayout pLayout = pMxDoc.PageLayout;
            IMap pMap = Utilities.getMapFrame(pMxDoc, pFrameName);
            IActiveView pActiveView = pMap as IActiveView;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string scale = null;
            string page_size = null;
            double xMax = 0;
            double yMax = 0;
            double xMin = 0;
            double yMin = 0;
            
            //Get the scale, page size and bounding coordinates of the dataframe
            long tempScale = Convert.ToInt64(pMap.MapScale);
            scale = tempScale.ToString();
            Debug.WriteLine(scale);
            page_size = pLayout.Page.FormID.ToString();
            xMax = pActiveView.Extent.XMax; 
            yMax = pActiveView.Extent.YMax;
            xMin = pActiveView.Extent.XMin;
            yMin = pActiveView.Extent.YMin;

            //Add all properties to the dictionary
            dict.Add("scale", scale);
            dict.Add("page_size", page_size);
            dict.Add("xMax", Math.Round(xMax, 2).ToString());
            dict.Add("yMax", Math.Round(yMax, 2).ToString());
            dict.Add("xMin", Math.Round(xMin, 2).ToString());
            dict.Add("yMin", Math.Round(yMin, 2).ToString());

            //Return the dictionary - blank if the the exception handling was invoked.
            return dict;
        }
        #endregion


        #region Public method detectMapFrame
        // Determines if a given map frame exists given a name and IMapDocument reference.
        // Boolean returns true if it exists.
        public static Boolean detectMapFrame(IMapDocument pMapDoc, string pFrameName)
        {
            //try
            //{
            //    IMxDocument pMxDoc;
            //    pMxDoc = (IMxDocument)pMapDoc;
            //    return detectMapFrame(pMxDoc, pFrameName);
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Debug.WriteLine(e.Message);
            //    throw e;
            //}

            IMap pMap;
            Boolean mapFrameExists = false;

            try
            {
                for (int i = 0; i <= pMapDoc.MapCount - 1; i++)
                {
                    pMap = pMapDoc.Map[i];
                    if (pMap.Name == pFrameName)
                    {
                        mapFrameExists = true;
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return mapFrameExists;
        
        
        }

        // Determines if a given map frame exists given a name and IMxDocument reference.
        // Boolean returns true if it exists.
        public static Boolean detectMapFrame(IMxDocument pMxDoc, string pFrameName)
        {

            IMap pMap;
            IMaps pMaps = pMxDoc.Maps;
            Boolean mapFrameExists = false;

            try
            {
                for (int i = 0; i <= pMaps.Count - 1; i++)
                {
                    pMap = pMaps.get_Item(i);
                    if (pMap.Name == pFrameName)
                    {
                        mapFrameExists = true;
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return mapFrameExists;

        }
        #endregion method | detectMapFrame

        #region Public method getLayoutTextElements
        //Gets all the text elements and their values from a map frame.
        //returns a dictionary with key value pairs of these element / values
        public static Dictionary<string, string> getLayoutTextElements(IMxDocument pMxDoc, string pFrameName)
        {
            //create dictionary to story element values
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // get MXD file path in case it is needed for error reporting later
            string mxdPath = "";
            try{
                mxdPath = (pMxDoc as IMapDocument).DocumentFilename;
            } catch (InvalidCastException ice){
            }
            
            //check if the frame passed exists in the map document
            if (PageLayoutProperties.detectMapFrame(pMxDoc, pFrameName))
            {
                //Declare variables
                IPageLayout pLayout = pMxDoc.PageLayout;
                IGraphicsContainer pGraphics = pLayout as IGraphicsContainer;
                pGraphics.Reset();

                IElement element = new TextElementClass();
                IElementProperties2 pElementProp;
                ITextElement pTextElement;
                //loop through the text elements in the frame
                try
                {
                    SimpleTextParser formattingTextParser = new SimpleTextParser();
                    formattingTextParser.TextSymbol = new TextSymbolClass();
                    Boolean bHasTags = false;

                    element = (IElement)pGraphics.Next();
                    while (element != null)
                    {
                        if (element is ITextElement)
                        {
                            pTextElement = element as ITextElement;
                            pElementProp = element as IElementProperties2;
                            //where the name is not blank
                            //System.Diagnostics.Debug.WriteLine(pElementProp.Name);
                            if (pElementProp.Name != "")
                            {
                                //store the name of the elements and the values in the dictionary as pairs
                                // check if text element needs parsing
                                formattingTextParser.Text = pTextElement.Text;
                                formattingTextParser.HasTags(ref bHasTags);
                                if (bHasTags)
                                {
                                    bool continueParsing = true;
                                    List<string> parsedText = new List<string>();
                                    // Parse formatted text. The Textparser advances through each tagged part of the string.
                                    formattingTextParser.Next();
                                    parsedText.Add(formattingTextParser.TextSymbol.Text);

                                    while (continueParsing)
                                    {
                                        formattingTextParser.Next();
                                        if (formattingTextParser.TextSymbol.Text == parsedText[parsedText.Count - 1])
                                        {
                                            continueParsing = false;
                                        }
                                        else
                                        {
                                            parsedText.Add(formattingTextParser.TextSymbol.Text);
                                        }
                                    }
                                    dict.Add(pElementProp.Name, string.Join("", parsedText.ToArray()));
                                }
                                else
                                {
                                    dict.Add(pElementProp.Name, pTextElement.Text);
                                }
                                formattingTextParser.Reset();
                            }
                        }
                        element = pGraphics.Next() as IElement;
                    }
                    //return the dictionary
                    return dict;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error getting elements from the map frame");
                    System.Diagnostics.Debug.WriteLine(e);

                    string exceptionMsg = String.Format("Error getting element {0} from the map frame", element.ToString());
                    throw new MapActionMapTemplateException(exceptionMsg, e, mxdPath, false, null, null);
                }
            }
            else
            {
                string exceptionMsg = String.Format("Unable to detect MapFrame {0} in current map document", pFrameName);
                throw new MapActionMapTemplateException(exceptionMsg, mxdPath, true);
            }
        }
#endregion

        #region Public method checkLayoutTextElementsForDuplicates
        //Checks the element names of the page layout 
        //returns boolean true if duplicates exist
        public static Boolean checkLayoutTextElementsForDuplicates(IMxDocument pMxDoc, string pFrameName, out string duplicatesString)
        {
            //create list to store name of text elements
            List<string> lst = new List<string>();
            Boolean duplicates = false; 
            //check if the frame passed exists in the map document
            if (PageLayoutProperties.detectMapFrame(pMxDoc, pFrameName))
            {
                //Declare variables
                IPageLayout pLayout = pMxDoc.PageLayout;
                IGraphicsContainer pGraphics = pLayout as IGraphicsContainer;
                pGraphics.Reset();

                IElement element = new TextElementClass();
                IElementProperties2 pElementProp;
                ITextElement pTextElement;

                //loop through the text elements in the frame
                try
                {
                    element = (IElement)pGraphics.Next();
                    while (element != null)
                    {
                        if (element is ITextElement)
                        {
                            pTextElement = element as ITextElement;
                            pElementProp = element as IElementProperties2;
                            //where the name is not blank
                            //System.Diagnostics.Debug.WriteLine(pElementProp.Name);
                            if (pElementProp.Name != "")
                            {
                                //store the name of the elements and the values in the dictionary as pairs
                                lst.Add(pElementProp.Name);
                            }
                        }
                        element = pGraphics.Next() as IElement;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error detecting duplicate elements from the map frame");
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            // Find if duplicates exist
            var allList = new List<string>();
            var duplicateList = new List<string>();

            // Search for duplicates
            foreach (var s in lst)
            {
                if (!allList.Contains(s))
                {
                    allList.Add(s);
                }
                else
                {
                    duplicateList.Add(s);
                }
            }

            int dupeCount = 0;
            duplicatesString = "";
            if (duplicateList.Count() > 0)
            {
                duplicates = true;
                // Concatenate string for error message
                foreach (var s in duplicateList)
                {
                    if (dupeCount > 0)
                    {
                        // Separate with a comma
                        duplicatesString = duplicatesString + ", ";
                    }
                    duplicatesString = duplicatesString + s;
                }
            }
            return duplicates;
        }
        #endregion

        #region Public method getMxdTitle
        //Returns the map title as string given the IApplciation variable of the current mxd
        public static string getMxdTitle(IApplication pApp)
        {
            string mapTitle = null;
            try
            {
                mapTitle = pApp.Document.Title; 

                if (mapTitle.Contains(".mxd") == true)
                {
                    //string input = "OneTwoThree";
                    mapTitle = mapTitle.Substring(0, mapTitle.Length - 4);


                }
                else
                {
                    mapTitle = mapTitle;

                }

                

                //mapTitle = mapTitle.Substring(0,mapTitle.Length - 4);
                System.Diagnostics.Debug.WriteLine(mapTitle);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error with the map title");
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return mapTitle;
        }
        #endregion

        #region Public method getPageSize
        //Returns the map title as string given the IApplciation variable of the current mxd
        public static string getPageSize(IMxDocument pMxDoc, string pFrameName)
        {
            string pageSize = null;
            Dictionary<string, string> mapProps = PageLayoutProperties.getDataframeProperties(pMxDoc, pFrameName);
            string pageFormId = mapProps["page_size"];

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
        #endregion

        #region Public method getScale
        //Returns the map title as string given the IApplciation variable of the current mxd
        public static string getScale(IMxDocument pMxDoc, string pMapFrameName)
        {
            string scale;

            if (detectMapFrame(pMxDoc, pMapFrameName))
            {
                Dictionary<string, string> mapProps = PageLayoutProperties.getDataframeProperties(pMxDoc, pMapFrameName);
                long temp_scale = Convert.ToInt64(mapProps["scale"]);
                scale = "1: " + string.Format("{0:n0}", temp_scale);
                return scale;
            }
            else
            {
                return "Map frame not found";
            }
                
        }
        #endregion

        #region Public method isDataDrivenPagesEnabled
        //Returns true is DataDrivenPages is Enabled, false otherwise.
        public static bool isDataDrivenPagesEnabled(IMapDocument pMapDoc)
        {
            IPrintAndExport docPrintExport;
            int pageCnt;

            docPrintExport = new PrintAndExportClass();
            pageCnt = docPrintExport.get_PageCount((IActiveView) pMapDoc.PageLayout);
            System.Console.WriteLine(String.Format("page count {0}", pageCnt));

            // Note: If DDP is not enabled get_PageCount will return 0.
            // DDP can be enabled but with only one page in the sequence.
            // Therefore if get_PageCount==1 then we conclude that DDP 
            // *is* enabled.
            return (pageCnt > 0);
        }
        #endregion


        // psuedo methods 

        // map frame count

        // map frame name

    }
}
