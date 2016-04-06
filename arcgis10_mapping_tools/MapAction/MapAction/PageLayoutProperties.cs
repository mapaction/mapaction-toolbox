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
            try
            {
                IMxDocument pMxDoc;
                pMxDoc = (IMxDocument)pMapDoc;
                return detectMapFrame(pMxDoc, pFrameName);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }
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
                                dict.Add(pElementProp.Name, pTextElement.Text);
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
                    return null;
                }
            }
            else
            {
                return null;
            }


        }
#endregion

        #region Public method checkLayoutTextElementsForDuplicates
        //Checks the element names of the page layout 
        //returns boolean true if duplicates exist
        public static Boolean checkLayoutTextElementsForDuplicates(IMxDocument pMxDoc, string pFrameName)
        {
            //create list to store name of text elements
            List<string> lst = new List<string>();
            Boolean duplicates; 
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

            //Find if duplicates exist
            int duplicateCount = lst.Count() - lst.Distinct().Count();

            if (duplicateCount != 0)
            {
                duplicates = true;
                return duplicates;
            }
            else
            {
                duplicates = false;
                return duplicates;
            }

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

        // psuedo methods 

        // map frame count

        // map frame name

    }
}
