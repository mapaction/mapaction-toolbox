using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.Diagnostics;
using Prototype1_ConfigTool;


namespace Prototype1_LayoutTool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.LayoutElements.getLayoutTextElements(pMxDoc, "Main map");
            
                txtScale.Text = txtScale.Text = updateScale();
                txtSpatialReference.Text = getSpatialReference();
                txtMapDocument.Text = txtMapDocument.Text = MapAction.LayoutElements.getMxdTitle(ArcMap.Application);
                txtGlideNumber.Text = getGlideNo();

        }

        private void btnMapDocument_Click(object sender, EventArgs e)
        {
            txtMapDocument.Text = MapAction.LayoutElements.getMxdTitle(ArcMap.Application);
        }

        private void btnSpatialReference_Click(object sender, EventArgs e)
        {
            txtSpatialReference.Text = getSpatialReference();
        }

        private void btnUpdateScale_Click(object sender, EventArgs e)
        {
            txtScale.Text = updateScale();
        }

        private void btnGlideNo_Click(object sender, EventArgs e)
        {
            txtGlideNumber.Text = getGlideNo();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Check to see if the config file exists, if not abort and send the user a message
            string filePath = Prototype1_ConfigTool.Properties.Settings.Default.xml_path;
            if (!File.Exists(@filePath))
            {
                MessageBox.Show("The operation configuration file is required for this tool.  It cannot be located.",
                    "Configuration file required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Perform validation checks
            ErrorCheckAndDisplay.checkElement(ttpGlideNumber, txtGlideNumber, "Glide Number");
            
            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.LayoutElements.getLayoutTextElements(pMxDoc, "Main map");
            
            //Check if the various elements existist that automated update, if not disable the automation buttons.
            //If they are present then update the text boxes with the value from the dictionary 
            if (!dict.ContainsKey("mxd_name") || !dict.ContainsKey("scale") || !dict.ContainsKey("scale") || !dict.ContainsKey("spatial_reference"))
            {
                btnUpdateAll.Enabled = false;
            }

            if (dict.ContainsKey("title") == true) { txtTitle.Text = dict["title"]; } else { txtTitle.Text = "Element not present"; txtTitle.ReadOnly = true; };
            if (dict.ContainsKey("summary") == true) { txtSummary.Text = dict["summary"]; } else { txtSummary.Text = "Element not present"; txtSummary.ReadOnly = true; };
            if (dict.ContainsKey("mxd_name") == true) { txtMapDocument.Text = dict["mxd_name"]; } else { txtMapDocument.Text = "Element not present"; txtMapDocument.ReadOnly = true; btnMapDocument.Enabled = false; };
            if (dict.ContainsKey("map_no") == true) { txtMapNumber.Text = dict["map_no"]; } else { txtMapNumber.Text = "Element not present"; txtMapNumber.ReadOnly = true; };
            if (dict.ContainsKey("scale") == true) { txtScale.Text = dict["scale"]; } else { txtScale.Text = "Element not present"; txtScale.ReadOnly = true; btnUpdateScale.Enabled = false; };
            if (dict.ContainsKey("spatial_reference") == true) { txtSpatialReference.Text = dict["spatial_reference"]; } else { txtSpatialReference.Text = "Element not present"; txtSpatialReference.ReadOnly = true; btnSpatialReference.Enabled = false; };
            if (dict.ContainsKey("glide_no") == true) { txtGlideNumber.Text = dict["glide_no"]; } else { txtGlideNumber.Text = "Element not present"; txtGlideNumber.ReadOnly = true; btnGlideNo.Enabled = false; };
        }

        private void tspBtnClearForm_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = string.Empty;
            this.txtSummary.Text = string.Empty;
            this.txtMapDocument.Text = string.Empty;
            this.txtMapNumber.Text = string.Empty;
            this.txtScale.Text = string.Empty;
            this.txtSpatialReference.Text = string.Empty;
            this.txtGlideNumber.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("title", this.txtTitle.Text);
            dict.Add("summary", this.txtSummary.Text);
            dict.Add("mxd_name", this.txtMapDocument.Text);
            dict.Add("map_no", this.txtMapNumber.Text);
            dict.Add("scale", this.txtScale.Text);
            dict.Add("spatial_reference", this.txtSpatialReference.Text);
            dict.Add("glide_no", this.txtGlideNumber.Text);

            setAllElements(dict);

            this.Close();
        }

        private void tspBtnCheckElements_Click(object sender, EventArgs e)
        {
            frmCheckElements dlg = new frmCheckElements();
            dlg.ShowDialog();
        }

        private static string getSpatialReference()
        {
            Dictionary<string, string> dictSpatialRef = MapAction.Utilities.getDataFrameSpatialReference(ArcMap.Application.Document as IMxDocument, "Main map");
            string stringSpatialRef;

            if (dictSpatialRef["type"] == "Geographic")
            {
                stringSpatialRef = dictSpatialRef["datum"];
            }
            else if (dictSpatialRef["type"] == "Projected")
            {
                stringSpatialRef = dictSpatialRef["projection"];
            }
            else
            {
                stringSpatialRef = "Unknown";
            }

            return stringSpatialRef;
        }

        private static string getGlideNo()
        {
            string glide_no = string.Empty;

            if (MapAction.Utilities.detectOperationConfig())
            {
                Dictionary<string, string> dictConfig = MapAction.Utilities.getOperationConfigValues();
                glide_no = dictConfig["glide_no"];
            }
            return glide_no;
        }

        public static void setAllElements(Dictionary<string, string> dict)
        {
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IPageLayout pLayout = pMxDoc.PageLayout;
            IGraphicsContainer pGraphics = pLayout as IGraphicsContainer;
            pGraphics.Reset();

            IElement element = new TextElementClass();
            IElementProperties2 pElementProp;
            ITextElement pTextElement;

            try
            {
                element = (IElement)pGraphics.Next();
                while (element != null)
                {
                    if (element is ITextElement)
                    {
                        pTextElement = element as ITextElement;
                        pElementProp = element as IElementProperties2;
                        if (pElementProp.Name == "title")
                        {
                            pTextElement.Text = dict["title"];
                        }
                        else if (pElementProp.Name == "summary")
                        {
                            pTextElement.Text = dict["summary"];
                        }
                        else if (pElementProp.Name == "mxd_name")
                        {
                            pTextElement.Text = dict["mxd_name"];
                        }
                        else if (pElementProp.Name == "map_no")
                        {
                            pTextElement.Text = dict["map_no"];
                        }
                        else if (pElementProp.Name == "scale")
                        {
                            pTextElement.Text = dict["scale"];
                        }
                        else if (pElementProp.Name == "spatial_reference")
                        {
                            pTextElement.Text = dict["spatial_reference"];
                        }
                        else if (pElementProp.Name == "glide_no")
                        {
                            pTextElement.Text = dict["glide_no"];
                        }

                    }
                    element = (IElement)pGraphics.Next();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error updating layout elements");
                System.Diagnostics.Debug.WriteLine(e);
            }

            IActiveView activeView = pMxDoc.ActivatedView as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }


        public static string updateScale()
        {
            string scale = MapAction.LayoutElements.getScale(ArcMap.Application.Document as IMxDocument, "Main map");
            string pageSize = MapAction.LayoutElements.getPageSize(ArcMap.Application.Document as IMxDocument, "Main map");
            string scaleString = scale + " (At " + pageSize + ")";
            return scaleString;
        }

        private void txtGlideNumber_TextChanged(object sender, EventArgs e)
        {
            //Perform validation checks
            ErrorCheckAndDisplay.checkElement(ttpGlideNumber, txtGlideNumber, "Glide Number");
        }


    }
}
