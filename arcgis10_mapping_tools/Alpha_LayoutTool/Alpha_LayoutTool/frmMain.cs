﻿using System;
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
using Alpha_ConfigTool;


namespace Alpha_LayoutTool
{
    public partial class frmMain : Form
    {

        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            //IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, "Main map");
            
                tbxScale.Text = tbxScale.Text = updateScale();
                tbxSpatialReference.Text = getSpatialReference();
                tbxMapDocument.Text = tbxMapDocument.Text = MapAction.PageLayoutProperties.getMxdTitle(ArcMap.Application);
                tbxGlideNumber.Text = getGlideNo();

        }

        private void btnMapDocument_Click(object sender, EventArgs e)
        {
            tbxMapDocument.Text = MapAction.PageLayoutProperties.getMxdTitle(ArcMap.Application);
        }

        private void btnSpatialReference_Click(object sender, EventArgs e)
        {
            tbxSpatialReference.Text = getSpatialReference();
        }

        private void btnUpdateScale_Click(object sender, EventArgs e)
        {
            tbxScale.Text = updateScale();
        }

        private void btnGlideNo_Click(object sender, EventArgs e)
        {
            tbxGlideNumber.Text = getGlideNo();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            //Check to see if the config file exists, if not abort and send the user a message
            string path = Alpha_ConfigTool.Properties.Settings.Default.crash_move_folder_path;
            string filePath = path + @"\operation_config.xml";
            if (!File.Exists(@filePath))
            {
                MessageBox.Show("The operation configuration file is required for this tool.  It cannot be located.",
                    "Configuration file required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Perform validation checks
            FormValidation.validateMapTitle(tbxTitle, eprMapTitle);
            FormValidation.validateMapSummary(tbxSummary, eprMapSummary);
            FormValidation.validateDataSources(tbxDataSources, eprDataSources);
            FormValidation.validateMapNumber(tbxMapNumber, eprMapNumberWarning, eprMapNumberError);
            FormValidation.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
            FormValidation.validateSpatialReference(tbxSpatialReference, eprSpatialReferenceWarning, eprSpatialReferenceError);
            FormValidation.validateScaleText(tbxScale, eprScaleTextWarning, eprScaleTextError);
            FormValidation.validateGlideNumber(tbxGlideNumber, eprGlideNumberWarning, eprSpatialReferenceError);

            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            //IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, "Main map");
            
            //Check if the various elements existist that automated update, if not disable the automation buttons.
            //If they are present then update the text boxes with the value from the dictionary 
            if (!dict.ContainsKey("mxd_name") || !dict.ContainsKey("scale") || !dict.ContainsKey("scale") || !dict.ContainsKey("spatial_reference"))
            {
                btnUpdateAll.Enabled = false;
            }

            if (dict.ContainsKey("title") == true) { tbxTitle.Text = dict["title"]; } else { tbxTitle.Text = "Element not present"; tbxTitle.ReadOnly = true; };
            if (dict.ContainsKey("summary") == true) { tbxSummary.Text = dict["summary"]; } else { tbxSummary.Text = "Element not present"; tbxSummary.ReadOnly = true; };
            if (dict.ContainsKey("data_sources") == true) { tbxDataSources.Text = dict["data_sources"]; } else { tbxDataSources.Text = "Element not present"; tbxDataSources.ReadOnly = true; };
            if (dict.ContainsKey("map_no") == true) { tbxMapNumber.Text = dict["map_no"]; } else { tbxMapNumber.Text = "Element not present"; tbxMapNumber.ReadOnly = true; };
            if (dict.ContainsKey("mxd_name") == true) { tbxMapDocument.Text = dict["mxd_name"]; } else { tbxMapDocument.Text = "Element not present"; tbxMapDocument.ReadOnly = true; btnMapDocument.Enabled = false; };
            if (dict.ContainsKey("scale") == true) { tbxScale.Text = dict["scale"]; } else { tbxScale.Text = "Element not present"; tbxScale.ReadOnly = true; btnUpdateScale.Enabled = false; };
            if (dict.ContainsKey("spatial_reference") == true) { tbxSpatialReference.Text = dict["spatial_reference"]; } else { tbxSpatialReference.Text = "Element not present"; tbxSpatialReference.ReadOnly = true; btnSpatialReference.Enabled = false; };
            if (dict.ContainsKey("glide_no") == true) { tbxGlideNumber.Text = dict["glide_no"]; } else { tbxGlideNumber.Text = "Element not present"; tbxGlideNumber.ReadOnly = true; btnGlideNo.Enabled = false; };
        }

        private void tspBtnClearForm_Click(object sender, EventArgs e)
        {
            this.tbxTitle.Text = string.Empty;
            this.tbxSummary.Text = string.Empty;
            this.tbxDataSources.Text = string.Empty;
            this.tbxMapNumber.Text = string.Empty;
            this.tbxMapDocument.Text = string.Empty;
            this.tbxScale.Text = string.Empty;
            this.tbxSpatialReference.Text = string.Empty;
            this.tbxGlideNumber.Text = string.Empty;
        }

        private void disposeAllErrorProviders()
        {
            FormValidation.disposeErrorProvider(eprDataSources);
            FormValidation.disposeErrorProvider(eprGlideNumberWarning);
            FormValidation.disposeErrorProvider(eprMapDocumentWarning);
            FormValidation.disposeErrorProvider(eprMapNumberError);
            FormValidation.disposeErrorProvider(eprMapSummary);
            FormValidation.disposeErrorProvider(eprMapTitle);
            FormValidation.disposeErrorProvider(eprScaleTextError);
            FormValidation.disposeErrorProvider(eprSpatialReferenceWarning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.disposeAllErrorProviders();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("title", this.tbxTitle.Text);
            dict.Add("summary", this.tbxSummary.Text);
            dict.Add("data_sources", this.tbxDataSources.Text);
            dict.Add("map_no", this.tbxMapNumber.Text);
            dict.Add("mxd_name", this.tbxMapDocument.Text);
            dict.Add("scale", this.tbxScale.Text);
            dict.Add("spatial_reference", this.tbxSpatialReference.Text);
            dict.Add("glide_no", this.tbxGlideNumber.Text);

            setAllElements(dict);
            this.disposeAllErrorProviders();
            this.Close();
        }

        private void tspBtnCheckElements_Click(object sender, EventArgs e)
        {
            frmCheckElements dlg = new frmCheckElements();
            dlg.ShowDialog();
        }

        public static string getSpatialReference()
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

        public static string getGlideNo()
        {
            string GlideNo = string.Empty;
            string path = Alpha_ConfigTool.Properties.Settings.Default.crash_move_folder_path + @"\operation_config.xml";

            if (MapAction.Utilities.detectOperationConfig())
            {
                Dictionary<string, string> dictConfig = MapAction.Utilities.getOperationConfigValues(path);
                if (dictConfig.ContainsKey("GlideNo")) { GlideNo = dictConfig["GlideNo"]; } 
            }

            return GlideNo;
        }

        public static void setAllElements(Dictionary<string, string> dict)
        {
            IPageLayout pLayout = _pMxDoc.PageLayout;
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
                        else if (pElementProp.Name == "data_sources")
                        {
                            pTextElement.Text = dict["data_sources"];
                        }
                        else if (pElementProp.Name == "map_no")
                        {
                            pTextElement.Text = dict["map_no"];
                        }
                        else if (pElementProp.Name == "mxd_name")
                        {
                            pTextElement.Text = dict["mxd_name"];
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

            IActiveView activeView = _pMxDoc.ActivatedView as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }


        public static string updateScale()
        {
            string scale = MapAction.PageLayoutProperties.getScale(ArcMap.Application.Document as IMxDocument, "Main map");
            string pageSize = MapAction.PageLayoutProperties.getPageSize(ArcMap.Application.Document as IMxDocument, "Main map");
            string scaleString = scale + " (At " + pageSize + ")";
            return scaleString;
        }

        //Perform validation checks on text change in each form element
        private void tbxMapNumber_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateMapNumber(tbxMapNumber, eprMapNumberWarning, eprMapNumberError);
        }

        private void tbxTitle_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateMapTitle(tbxTitle, eprMapTitle);
        }

        private void tbxSummary_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateMapSummary(tbxSummary, eprMapSummary);
        }

        private void tbxDataSources_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateDataSources(tbxDataSources, eprDataSources);
        }

        private void tbxSpatialReference_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateSpatialReference(tbxSpatialReference, eprSpatialReferenceWarning, eprSpatialReferenceError); 
        }

        private void tbxMapDocument_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
        }

        private void tbxScale_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateScaleText(tbxScale, eprScaleTextWarning, eprScaleTextError);
        }

        private void tbxGlideNumber_TextChanged(object sender, EventArgs e)
        {
            FormValidation.validateGlideNumber(tbxGlideNumber, eprGlideNumberWarning, eprGlideNumberError);
        }


    }
}
