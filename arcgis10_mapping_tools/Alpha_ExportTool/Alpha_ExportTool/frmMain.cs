using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace Alpha_ExportTool
{
    public partial class frmMain : Form
    {
        //Set the dataframe that you are searching for in the layouts.  This is used in many methods below.
        //Need a better solution for sorting this out
        private static string _targetMapFrame = "Main map";
        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;
        private static Dictionary<string,string>_dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, _targetMapFrame);
        
        //create a variable to hold the status of each validation check
        private string _titleValidationResult;
        private string _summaryValidationResult;
        private string _mapDocumentValidationResult;
        private string _datumValidationResult;
        private string _projectionValidationResult;
        private string _scaleValidationResult;
        private string _dateValidationResult;
        private string _timeValidationResult;
        private string _paperSizeValidationResult;
        private string _imageryDateValidationResult;
        private string _dataSourcesValidationResult;
        private string _operationIdValidationResult;
        private string _glideNumberValidationResult;
        private string _locationValidationResult;
        private string _themeValidationResult;
        private string _countriesValidationResult;
        private string _statusValidationResult;
        private string _accessValidationResult;
        private string _accessNoteValidationResult;
        private string _qualityControlValidationResult;
        private string _languageValidationResult;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnUserRight_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageExport;
        }

        private void btnUserLeft_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageLayout;
        }

        private void btnLayoutRight_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageUser;
        }

        private void btnExportLeft_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageUser;
        }

        private void btnExportZipPath_Click(object sender, EventArgs e)
        {
            //set up select folder dialog properties 
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            //set the intial path
            if (Directory.Exists(@tbxExportZipPath.Text)) 
            {
                dlg.SelectedPath = @tbxExportZipPath.Text;
            }
            else if (Directory.Exists(@MapAction.Utilities.getCrashMoveFolderPath()))
            {
                dlg.SelectedPath = @MapAction.Utilities.getCrashMoveFolderPath();
            }
            else
            {
                dlg.SelectedPath = @"C:\";
            }



            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxExportZipPath.Text = dlg.SelectedPath;
            }
            else
            {
                return;
            }
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Form validation methods
            _titleValidationResult = FormValidation.validateMapTitle(tbxMapTitle, eprMaptitleWarning, eprMapTitleError);
            _summaryValidationResult = FormValidation.validateMapSummary(tbxMapSummary, eprMapSummaryWarning, eprMapSummaryError);
            _mapDocumentValidationResult = FormValidation.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
            _datumValidationResult = FormValidation.validateDatum(tbxDatum, eprDatumWarning, eprDatumError);
            _projectionValidationResult = FormValidation.validateProjection(tbxProjection, eprProjectionWarning, eprProjectionError);
            _scaleValidationResult = FormValidation.validateScale(tbxScale, eprScaleWarning, eprScaleError);
            _dateValidationResult = FormValidation.validateDate(tbxDate, eprDateWarning, eprDateError);
            _timeValidationResult = FormValidation.validateTime(tbxTime, eprTimeWarning, eprTimeError);
            _paperSizeValidationResult = FormValidation.validatePaperSize(tbxPaperSize, eprPaperWarning, eprPaperError);
            _imageryDateValidationResult = FormValidation.validateImageryDate(tbxImageDate, eprImageryDate);
            _dataSourcesValidationResult = FormValidation.validateDataSources(tbxDataSources, eprDataSourcesWarning, eprDataSourcesError);
            _operationIdValidationResult = FormValidation.validateOperationId(tbxOperationId, eprOperationIdWarning, eprOperationIdError);
            _glideNumberValidationResult = FormValidation.validateGlideNumber(tbxGlideNo, eprGlideNumberWarning, eprGlideNumberError);
            _locationValidationResult = FormValidation.validateLocation(tbxImageLocation, eprLocationWarning);
            _themeValidationResult = FormValidation.validateTheme(cboTheme, eprThemeWarning);
            _countriesValidationResult = FormValidation.validateCountries(tbxCountries, eprCountriesWarning);
            _statusValidationResult = FormValidation.validateStatus(cboStatus, eprStatusWarning);
            _accessValidationResult = FormValidation.validateAccess(cboAccess, eprAccessWarning);
            _accessNoteValidationResult = FormValidation.validateAccessNote(tbxImageAccessNotes, eprAccessNoteWarning);
            _qualityControlValidationResult = FormValidation.validateQualityControl(cboQualityControl, eprQualityControlWarning);
            _languageValidationResult = FormValidation.validateLanguage(tbxLanguage, eprLanguageWarning, eprLanguageError);

            var dict = new Dictionary<string, string>();
            dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, _targetMapFrame);

            //Update form text boxes with values from the map
            if (dict.ContainsKey("title")) { tbxMapTitle.Text = dict["title"]; }
            if (dict.ContainsKey("summary")) { tbxMapSummary.Text = dict["summary"]; }
            if (dict.ContainsKey("mxd_name")) { tbxMapDocument.Text = dict["mxd_name"]; }
            if (dict.ContainsKey("data_sources")) { tbxDataSources.Text = dict["data_sources"]; }

            // Update form values from the config xml
            var dictXML = new Dictionary<string, string>();
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = path + @"\operation_config.xml";
            dictXML = MapAction.Utilities.getOperationConfigValues(filePath);
            if (dictXML.ContainsKey("GlideNo")) { tbxGlideNo.Text = dictXML["GlideNo"]; }
            if (dictXML.ContainsKey("Language")) { tbxLanguage.Text = dictXML["Language"]; }
            if (dictXML.ContainsKey("Country")) { tbxCountries.Text = dictXML["Country"]; }
            string operational_id = dictXML["OperationId"];
            Debug.WriteLine("Op ID: " + operational_id);
            if (dictXML.ContainsKey("OperationId")) { tbxOperationId.Text = dictXML["OperationId"]; }
            if (dictXML.ContainsKey("DefaultPathToExportDir")) { tbxExportZipPath.Text = dictXML["DefaultPathToExportDir"]; }
            if (dictXML.ContainsKey("DefaultJpegResDPI")) { nudJpegResolution.Value = Convert.ToDecimal(dictXML["DefaultJpegResDPI"]); }
            if (dictXML.ContainsKey("DefaultPdfResDPI")) { nudPdfResolution.Value = Convert.ToDecimal(dictXML["DefaultPdfResDPI"]); }
            if (dictXML.ContainsKey("DefaultEmfResDPI")) { nudEmfResolution.Value = Convert.ToDecimal(dictXML["DefaultPdfResDPI"]); }

            // Set the spatial reference information on load
            var dictSpatialRef = new Dictionary<string, string>();
            dictSpatialRef  = MapAction.Utilities.getDataFrameSpatialReference(_pMxDoc, _targetMapFrame);
            tbxDatum.Text = dictSpatialRef["datum"];
            tbxProjection.Text = dictSpatialRef["projection"];

            // Set the 'metadata' tab elements
            var date = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var time = System.DateTime.Now.ToString("HH:mm");
            tbxDate.Text = date;
            tbxTime.Text = time;
            tbxPaperSize.Text = MapAction.PageLayoutProperties.getPageSize(_pMxDoc, _targetMapFrame);
            tbxScale.Text = MapAction.PageLayoutProperties.getScale(_pMxDoc, _targetMapFrame);
            
        }

        private void btnCreateZip_Click(object sender, EventArgs e)
        {

            // Create and start a stopwatch to measure the function performance
            //### Remove at a later time ###
            Stopwatch sw = Stopwatch.StartNew();

            // Start checks before running the the acutal create elements
            if (tbxMapDocument.Text == string.Empty)
            {
                MessageBox.Show("A document name is required. It is used as a part of the output file names.",
                    "Update document name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabExportTool.SelectedTab = tabPageLayout;
                tbxMapDocument.Focus();
                return;
            }
            
            //!!!!!!!!!!Need a method to check to see if the user has write access to the set path !!!!!!!!!!!!!!//
            var path = tbxExportZipPath.Text;
            //check the path exists and ideally check for write permissions
            if (!Directory.Exists(@path))
            {
                Debug.WriteLine("Exiting createZip function as path is not valid");
                //Show message on invalid export path
                MessageBox.Show("The export path is not valid. Please choose another path.",
                    "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxExportZipPath.Focus();
                return;
            }
            Debug.WriteLine("checks on export complete");

            // Get the path and file name to pass to the various functions
            string exportPathFileName = getExportPathFileName(tbxExportZipPath.Text, tbxMapDocument.Text);

            // Disable the button after the export checks are complete to prevent multiple clicks
            this.Enabled = false;

            // this method doesn't correctly in checking for permissions, requires updating.  
            // MapAction.Utilities.detectWriteAccessToPath(path);

            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IActiveView pActiveView = pMxDoc.ActiveView;

            // Call to export the images and return a dictionary of the file sizes
            Dictionary<string, string> dictFilePaths = exportAllImages();

            // Create a dictionary to store the image file sizes to add to the output xml
            Dictionary<string, long> dictImageFileSizes = new Dictionary<string, long>();
            // Calculate the file size of each image and add it to the dictionary
            dictImageFileSizes.Add("pdf", MapAction.Utilities.getFileSize(dictFilePaths["pdf"]));
            System.Windows.Forms.Application.DoEvents();
            dictImageFileSizes.Add("jpeg", MapAction.Utilities.getFileSize(dictFilePaths["jpeg"]));
            dictImageFileSizes.Add("emf", MapAction.Utilities.getFileSize(dictFilePaths["emf"]));

            // Create a dictionary to get and store the map frame extents to pass to the output xml
            Dictionary<string, string> dictFrameExtents = MapAction.PageLayoutProperties.getDataframeProperties(pMxDoc, "Main map");

            // Export KML
            string kmzPathFileName = exportPathFileName + ".kmz";
            string kmzScale;
            if (dictFrameExtents.ContainsKey("scale")) {kmzScale = dictFrameExtents["scale"];} else {kmzScale = null;};
            MapAction.MapExport.exportMapFrameKmlAsRaster(pMxDoc, "Main map", @kmzPathFileName, kmzScale);

            // Get the mxd filename
            string mxdName = ArcMap.Application.Document.Title;
            System.Windows.Forms.Application.DoEvents();
            // Create the output xml file & return the xml path           
            string xmlPath = string.Empty;
            try
            {
                Dictionary<string, string> dict = getExportToolValues(dictImageFileSizes, dictFilePaths, dictFrameExtents, mxdName);
                xmlPath = MapAction.Utilities.createXML(dict, "mapdata", path, tbxMapDocument.Text, 2);
            }
            catch (Exception xml_e)
            {
                Debug.WriteLine("Error writing out xml file.");
                Debug.WriteLine(xml_e.Message);
                return;
            }

            // Add the xml path to the dictFilePaths, which is the input into the creatZip method
            dictFilePaths.Add("xml", xmlPath);

            // Create zip
            MapAction.MapExport.createZip(dictFilePaths);
            // close the wait dialog
            // dlg.lblWaitMainMessage.Text = "Export complete";
            // int milliseconds = 1250;
            // Thread.Sleep(milliseconds);
            this.Close();

            // the output filepath

            MessageBox.Show("Files successfully output to: " + Environment.NewLine + path,
                "Export complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // If open explorer checkbox is ticked, open windows explorer to the directory 
            if (chkOpenExplorer.Checked)
            {
                MapAction.MapExport.openExplorerDirectory(tbxExportZipPath.Text);
            }

            sw.Stop();
            string timeTaken = Math.Round((sw.Elapsed.TotalMilliseconds / 1000),2).ToString();
            Debug.WriteLine("Time taken: ", timeTaken);

        }

        private Dictionary<string, string> getExportToolValues(Dictionary<string, long> dictImageFileSizes, 
            Dictionary<string, string> dictFilePaths, Dictionary<string, string> dictFrameExtents, string mxdName)
        {
            // Create a dictionary and add values from Export form
            var dict = new Dictionary<string, string>()
            {
                {"operationID",     tbxOperationId.Text},
                {"sourceorg",       "MapAction"}, //this is hard coded in the existing applicaton
                {"title",           tbxMapTitle.Text},
                {"ref",             tbxMapDocument.Text},
                {"language",        tbxLanguage.Text},
                {"countries",       tbxCountries.Text},
                {"createdate",      tbxDate.Text},
                {"createtime",      tbxTime.Text},
                {"status",          cboStatus.Text},
                {"xmax",            dictFrameExtents["xMax"]},
                {"xmin",            dictFrameExtents["xMin"]},
                {"ymax",            dictFrameExtents["yMax"]},
                {"ymin",            dictFrameExtents["yMin"]},
                {"proj",            tbxProjection.Text},
                {"datum",           tbxDatum.Text},
                {"jpgfilename",     System.IO.Path.GetFileName(dictFilePaths["jpeg"])},
                {"pdffilename",     System.IO.Path.GetFileName(dictFilePaths["pdf"])},
                {"qclevel",         cboQualityControl.Text},
                {"qcname",          ""},
                {"access",          cboAccess.Text},
                {"glideno",         tbxGlideNo.Text},
                {"summary",         tbxMapSummary.Text},
                {"imagerydate",     tbxImageDate.Text},
                {"datasource",      tbxDataSources.Text},
                {"location",        tbxImageLocation.Text},
                {"theme",           cboTheme.Text},
                {"scale",           tbxScale.Text},
                {"papersize",       tbxPaperSize.Text},
                {"jpgfilesize",     dictImageFileSizes["jpeg"].ToString()},
                {"jpgresolutiondpi",nudJpegResolution.Value.ToString()},
                {"pdffilesize",     dictImageFileSizes["pdf"].ToString()},
                {"pdfresolutiondpi",nudPdfResolution.Value.ToString()},
                {"mxdfilename",     mxdName},
                {"paperxmax",       ""},
                {"paperxmin",       ""},
                {"paperymax",       ""},
                {"paperymin",       ""},
                {"kmzfilename",     ""},
                {"accessnotes",     tbxImageAccessNotes.Text}
            };
            return dict;
            //string filename = Path.GetFileName(path);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            // Start validation procedures
            // List<string> lstEmptyFields = validation.getEmptyRequiredFieldList(getExportToolValues());

            //lblEmptyXmlFieldCount.Text = lstEmptyFields.Count.ToString();
            //string returnString = string.Join(Environment.NewLine, lstEmptyFields.ToArray());
            //lblEmptyFields.Text = returnString;
        }

        private Dictionary<string, string> exportAllImages()
        {
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            //IActiveView pActiveView = pMxDoc.ActiveView;
            var dict = new Dictionary<string, string>();

            // Get the path and file name to pass to the various functions
            string exportPathFileName = getExportPathFileName(tbxExportZipPath.Text, tbxMapDocument.Text);

            //check to see variable exists
            if (!Directory.Exists(@tbxExportZipPath.Text) || tbxMapDocument.Text == "" || tbxMapDocument.Text == string.Empty)
            {
                Debug.WriteLine("Image export variables not valid.");
                return dict;
            }
            else
            {
                //Output 3 image formats pdf, jpeg & emf
                dict.Add("pdf", MapAction.MapExport.exportImage(pMxDoc, "pdf", nudPdfResolution.Value.ToString(), exportPathFileName, null));
                dict.Add("jpeg", MapAction.MapExport.exportImage(pMxDoc, "jpeg", nudJpegResolution.Value.ToString(), exportPathFileName, null));
                dict.Add("emf", MapAction.MapExport.exportImage(pMxDoc, "emf", nudEmfResolution.Value.ToString(), exportPathFileName, null));
                MapAction.MapExport.exportImage(pMxDoc, "emf", nudEmfResolution.Value.ToString(), exportPathFileName, "Main map");
                MapAction.MapExport.exportImage(pMxDoc, "jpeg", nudEmfResolution.Value.ToString(), exportPathFileName, "Main map");

            }

            return dict;
        }

        private void chkEditAllFields_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAllFields.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("Changing these values will create a discrepancy between the page layout and the metadata xml file. Do you want to continue?", 
                    "Edit values?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    tbxMapTitle.ReadOnly = false;
                    tbxMapSummary.ReadOnly = false;
                    tbxMapDocument.ReadOnly = false;
                    tbxDatum.ReadOnly = false;
                    tbxProjection.ReadOnly = false;
                    tbxDate.ReadOnly = false;
                    tbxTime.ReadOnly = false;
                    tbxPaperSize.ReadOnly = false;
                    tbxScale.ReadOnly = false;
                    tbxOperationId.ReadOnly = false;
                    tbxGlideNo.ReadOnly = false;
                    tbxDataSources.ReadOnly = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    chkEditAllFields.Checked = false;
                    return;
                }
            }
            else
            {
                tbxMapTitle.ReadOnly = true;
                tbxMapSummary.ReadOnly = true;
                tbxMapDocument.ReadOnly = true;
                tbxDatum.ReadOnly = true;
                tbxProjection.ReadOnly = true;
                tbxDate.ReadOnly = true;
                tbxTime.ReadOnly = true;
                tbxPaperSize.ReadOnly = true;
                tbxScale.ReadOnly = true;
                tbxOperationId.ReadOnly = true;
                tbxGlideNo.ReadOnly = true;
                tbxDataSources.ReadOnly = true;
            }
        }

        private string getExportPathFileName(string path, string documentName)
        {
            // Concatenate the 
            string pathFileName = @path + "\\" + documentName;
            return pathFileName; 
        
        }



        //### Methods to work with form validation #### Copied directly from the Alpha export tool
        //### At a later time they should be consolidated in the MapAction class library ###

        public static string updateScale()
        {
            string scale = MapAction.PageLayoutProperties.getScale(ArcMap.Application.Document as IMxDocument, "Main map");
            string pageSize = MapAction.PageLayoutProperties.getPageSize(ArcMap.Application.Document as IMxDocument, "Main map");
            string scaleString = scale + " (At " + pageSize + ")";
            return scaleString;
        }


        public static string getGlideNo()
        {
            string GlideNo = string.Empty;
            string path = MapAction.Utilities.getOperationConfigFilePath();

            if (MapAction.Utilities.detectOperationConfig())
            {
                Dictionary<string, string> dictConfig = MapAction.Utilities.getOperationConfigValues(path);
                if (dictConfig.ContainsKey("GlideNo")) { GlideNo = dictConfig["GlideNo"]; }
            }

            return GlideNo;
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

        private void tbxMapTitle_TextChanged(object sender, EventArgs e)
        {
            _titleValidationResult = FormValidation.validateMapTitle(tbxMapTitle, eprMaptitleWarning, eprMapTitleError);
        }

        private void tbxMapSummary_TextChanged(object sender, EventArgs e)
        {
            _summaryValidationResult = FormValidation.validateMapSummary(tbxMapSummary, eprMapSummaryWarning, eprMapSummaryError);
        }

        private void tbxImageDate_TextChanged(object sender, EventArgs e)
        {
            _imageryDateValidationResult = FormValidation.validateImageryDate(tbxImageDate, eprImageryDate);
        }

        private void tbxDataSources_TextChanged(object sender, EventArgs e)
        {
            _dataSourcesValidationResult = FormValidation.validateDataSources(tbxDataSources, eprDataSourcesWarning, eprDataSourcesError);
        }

        private void tbxPaperSize_TextChanged(object sender, EventArgs e)
        {
            _paperSizeValidationResult = FormValidation.validatePaperSize(tbxPaperSize, eprPaperWarning, eprPaperError);
        }

        private void tbxTime_TextChanged(object sender, EventArgs e)
        {
            _timeValidationResult = FormValidation.validateTime(tbxTime, eprTimeWarning, eprTimeError);
        }

        private void tbxDate_TextChanged(object sender, EventArgs e)
        {
            _dateValidationResult = FormValidation.validateDate(tbxDate, eprDateWarning, eprDateError);
        }

        private void tbxScale_TextChanged(object sender, EventArgs e)
        {
            _scaleValidationResult = FormValidation.validateScale(tbxScale, eprScaleWarning, eprScaleError);
        }

        private void tbxProjection_TextChanged(object sender, EventArgs e)
        {
            _projectionValidationResult = FormValidation.validateProjection(tbxProjection, eprProjectionWarning, eprProjectionError);
        }

        private void tbxDatum_TextChanged(object sender, EventArgs e)
        {
            _datumValidationResult = FormValidation.validateDatum(tbxDatum, eprDatumWarning, eprDatumError);
        }

        private void tbxMapDocument_TextChanged(object sender, EventArgs e)
        {
            _mapDocumentValidationResult = FormValidation.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
        }

        private void tbxOperationId_TextChanged(object sender, EventArgs e)
        {
            _operationIdValidationResult = FormValidation.validateOperationId(tbxOperationId, eprOperationIdWarning, eprOperationIdError);
        }

        private void tbxGlideNo_TextChanged(object sender, EventArgs e)
        {
            _glideNumberValidationResult = FormValidation.validateGlideNumber(tbxGlideNo, eprGlideNumberWarning, eprGlideNumberError);
        }

        private void tbxImageLocation_TextChanged(object sender, EventArgs e)
        {
            _locationValidationResult = FormValidation.validateLocation(tbxImageLocation, eprLocationWarning);
        }

        private void cboTheme_TextChanged(object sender, EventArgs e)
        {
            _themeValidationResult = FormValidation.validateTheme(cboTheme, eprThemeWarning);
        }

        private void tbxCountries_TextChanged(object sender, EventArgs e)
        {
            _countriesValidationResult = FormValidation.validateCountries(tbxCountries, eprCountriesWarning);
        }

        private void cboStatus_TextChanged(object sender, EventArgs e)
        {
            _statusValidationResult = FormValidation.validateStatus(cboStatus, eprStatusWarning);
        }

        private void cboAccess_TextChanged(object sender, EventArgs e)
        {
            _accessValidationResult = FormValidation.validateAccess(cboAccess, eprAccessWarning);
        }

        private void tbxImageAccessNotes_TextChanged(object sender, EventArgs e)
        {
            _accessNoteValidationResult = FormValidation.validateAccessNote(tbxImageAccessNotes, eprAccessNoteWarning);
        }

        private void cboQualityControl_TextChanged(object sender, EventArgs e)
        {
            _qualityControlValidationResult = FormValidation.validateQualityControl(cboQualityControl, eprQualityControlWarning);
        }

        private void tbxLanguage_TextChanged(object sender, EventArgs e)
        {
            _languageValidationResult = FormValidation.validateLanguage(tbxLanguage, eprLanguageWarning, eprLanguageError);
        }

        private void tabExportTool_Selected(object sender, TabControlEventArgs e)
        {
            //Update the validation summary section images based on the validation status
            FormValidation.validationCheck(_titleValidationResult, imgTitleStatus);
            FormValidation.validationCheck(_summaryValidationResult, imgSummaryStatus);
            FormValidation.validationCheck(_mapDocumentValidationResult, imgDocumentStatus);
            FormValidation.validationCheck(_datumValidationResult, imgDatumStatus);
            FormValidation.validationCheck(_projectionValidationResult, imgProjectionStatus);
            FormValidation.validationCheck(_scaleValidationResult, imgScaleStatus);
            FormValidation.validationCheck(_dateValidationResult, imgDateStatus);
            FormValidation.validationCheck(_timeValidationResult, imgTimeStatus);
            FormValidation.validationCheck(_paperSizeValidationResult, imgPaperSizeStatus);
            FormValidation.validationCheck(_imageryDateValidationResult, imgImageDateStatus);
            FormValidation.validationCheck(_dataSourcesValidationResult, imgDataSourcesStatus);
            FormValidation.validationCheck(_operationIdValidationResult, imgOperationIdStatus);
            FormValidation.validationCheck(_glideNumberValidationResult, imgGlideNumberStatus);
            FormValidation.validationCheck(_locationValidationResult, imgLocationStatus);
            FormValidation.validationCheck(_themeValidationResult, imgThemeStatus);
            FormValidation.validationCheck(_countriesValidationResult, imgCountriesStatus);
            FormValidation.validationCheck(_statusValidationResult, imgStatusStatus);
            FormValidation.validationCheck(_accessValidationResult, imgAccessStatus);
            FormValidation.validationCheck(_accessNoteValidationResult, imgAccessNoteStatus);
            FormValidation.validationCheck(_qualityControlValidationResult, imgQualityControlStatus);
            FormValidation.validationCheck(_languageValidationResult, imgLanguageStatus);

        }

    }
}
