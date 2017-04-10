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
using System.Xml.Linq;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using MapAction;

namespace MapActionToolbars
{
    public partial class frmExportMain : Form
    {
        //Set the dataframe that you are searching for in the layouts.  This is used in many methods below.
        //Need a better solution for sorting this out
        private static string _targetMapFrame = "Main map";
        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;
        
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
        private string _mapNumber;
        private const string _statusNew = "New";
        private const string _statusUpdate = "Update";
        private const string _statusCorrection = "Correction";
        private const int _initialVersionNumber = 1;



        public frmExportMain()
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
            tabExportTool.SelectedTab = tabPageLayout;
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
            _titleValidationResult = FormValidationExport.validateMapTitle(tbxMapTitle, eprMaptitleWarning, eprMapTitleError);
            _summaryValidationResult = FormValidationExport.validateMapSummary(tbxMapSummary, eprMapSummaryWarning, eprMapSummaryError);
            _mapDocumentValidationResult = FormValidationExport.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
            _datumValidationResult = FormValidationExport.validateDatum(tbxDatum, eprDatumWarning, eprDatumError);
            _projectionValidationResult = FormValidationExport.validateProjection(tbxProjection, eprProjectionWarning, eprProjectionError);
            _scaleValidationResult = FormValidationExport.validateScale(tbxScale, eprScaleWarning, eprScaleError);
            _dateValidationResult = FormValidationExport.validateDate(tbxDate, eprDateWarning, eprDateError);
            _timeValidationResult = FormValidationExport.validateTime(tbxTime, eprTimeWarning, eprTimeError);
            _paperSizeValidationResult = FormValidationExport.validatePaperSize(tbxPaperSize, eprPaperWarning, eprPaperError);
            _imageryDateValidationResult = FormValidationExport.validateImageryDate(tbxImageDate, eprImageryDate);
            _dataSourcesValidationResult = FormValidationExport.validateDataSources(tbxDataSources, eprDataSourcesWarning, eprDataSourcesError);
            _operationIdValidationResult = FormValidationExport.validateOperationId(tbxOperationId, eprOperationIdWarning, eprOperationIdError);
            _glideNumberValidationResult = FormValidationExport.validateGlideNumber(tbxGlideNo, eprGlideNumberWarning, eprGlideNumberError);
            _locationValidationResult = FormValidationExport.validateLocation(tbxImageLocation, eprLocationWarning);
            _themeValidationResult = FormValidationExport.validateTheme(checkedListBoxThemes, eprThemeWarning);
            _countriesValidationResult = FormValidationExport.validateCountries(tbxCountries, eprCountriesWarning);
            _statusValidationResult = FormValidationExport.validateStatus(cboStatus, eprStatusWarning);
            _accessValidationResult = FormValidationExport.validateAccess(cboAccess, eprAccessWarning);
            _accessNoteValidationResult = FormValidationExport.validateAccessNote(tbxImageAccessNotes, eprAccessNoteWarning);
            _qualityControlValidationResult = FormValidationExport.validateQualityControl(cboQualityControl, eprQualityControlWarning);
            _languageValidationResult = FormValidationExport.validateLanguage(tbxLanguage, eprLanguageWarning, eprLanguageError);

            var dict = new Dictionary<string, string>();
            // added extra parameter to say that in this case all of the ESRI markup should be stripped from the label values
            dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, _targetMapFrame, true);

            //Update form text boxes with values from the map
            if (dict.ContainsKey("title")) { tbxMapTitle.Text = dict["title"]; }
            if (dict.ContainsKey("summary")) { tbxMapSummary.Text = dict["summary"]; }
            if (dict.ContainsKey("mxd_name")) { tbxMapDocument.Text = dict["mxd_name"]; }
            if (dict.ContainsKey("data_sources")) { tbxDataSources.Text = dict["data_sources"]; }
            if (dict.ContainsKey("map_no")) 
            { 
                _mapNumber = dict["map_no"];
                tbxMapNumber.Text = _mapNumber;  
            }

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

            // Set the status value and the version number from the existing XML if it exists:
            setValuesFromExistingXML();

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

            // Check if Data Driven Page and enable dropdown accordingly
            IMapDocument mapDoc;
            mapDoc = (_pMxDoc as MxDocument) as IMapDocument;
            tbxMapbookMode.Enabled = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);
            
        }

        // Set the "Status" value and the VersionNumber:

        private void setValuesFromExistingXML()
        {
            // Presume Initial Version
            nudVersionNumber.Value = _initialVersionNumber;
            cboStatus.Text = _statusNew;

            string xmlPath = tbxExportZipPath.Text + "\\" + tbxMapDocument.Text + ".xml";

            // Does the xmlPath already exist?
            if (File.Exists(xmlPath))
            {
                // If it does, get the values from the current XML.
                XDocument doc = XDocument.Load(xmlPath);
                foreach (XElement usEle in doc.Root.Descendants())
                {
                    if (usEle.Name.ToString().Equals("versionNumber"))
                    {
                        nudVersionNumber.Value = Convert.ToDecimal(usEle.Value.ToString());
                    }
                    else if (usEle.Name.ToString().Equals("status"))
                    {
                        cboStatus.Text = usEle.Value.ToString();
                    }
                    // In "older" XML, only a single theme was possible.  Read it in:
                    else if (usEle.Name.ToString().Equals("theme"))
                    {
                        for (int i = 0; i < checkedListBoxThemes.Items.Count; i++)
                        {
                            if (checkedListBoxThemes.Items[i].ToString().Equals(usEle.Value.ToString()))
                            {
                                checkedListBoxThemes.SetItemChecked(i, true);
                            }
                        }        
                    }
                    // Read in multiple themes
                    else if (usEle.Name.ToString().Equals("themes"))
                    {
                        for (int i = 0; i < checkedListBoxThemes.Items.Count; i++)
                        {
                            if (checkedListBoxThemes.Items[i].ToString().Equals(usEle.Value.ToString()))
                            {
                                checkedListBoxThemes.SetItemChecked(i, true);
                            }
                        }        
                    }
                }
            }
            if (nudVersionNumber.Value == _initialVersionNumber)
            {
                cboStatus.Text = _statusNew;
            }
            else
            {
                // if the version number is > 1 and the status is "New" or not in the expected range..
                if ((cboStatus.Text.Equals(_statusNew)) && 
                    ((!(cboStatus.Text.Equals(_statusCorrection)) && (!(cboStatus.Text.Equals(_statusUpdate))))))
                {
                    // Set to 'Update'
                    cboStatus.Text = _statusUpdate;
                }
            }
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

            // TODO:
            // APS Is there a good reasons for retreving the reference to the IMxDocument
            // via the ArcMap Application? Why not use the `frmExportMain._pMxDoc` member instead?
            // Alternatively is the `frmExportMain._pMxDoc` member used or required?
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IActiveView pActiveView = pMxDoc.ActiveView;
            // Ssetup dictionaries for metadata XML
            Dictionary<string, string> dictFilePaths;
            // Create a dictionary to store the image file sizes to add to the output xml
            Dictionary<string, long> dictImageFileSizes = new Dictionary<string, long>();

            // Create a dictionary to get and store the map frame extents to pass to the output xml
            // TODO: Get extent of dpp index dataset instead of data frame. 
            Dictionary<string, string> dictFrameExtents = MapAction.PageLayoutProperties.getDataframeProperties(pMxDoc, "Main map");

            IMapDocument mapDoc;
            mapDoc = (pMxDoc as MxDocument) as IMapDocument;
            bool isDDP = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);

            if (!isDDP) // Need a way to do this - the form elements are all disabled before export - see ^^
            {
                // Call to export the images and return a dictionary of the file names
                dictFilePaths = exportAllImages();

                // Calculate the file size of each image and add it to the dictionary
                // Don't use dict.add because a) it's another place to keep track of magic strings
                // values, and b) if we accidentally call it twice with same key we get an exception
                foreach (var kvp in dictFilePaths)
                {
                    dictImageFileSizes[kvp.Key] = MapAction.Utilities.getFileSize(kvp.Value);
                }
                System.Windows.Forms.Application.DoEvents();

                // Export KML
                IMapDocument pMapDoc = (IMapDocument)pMxDoc;            
                string kmzPathFileName = exportPathFileName + ".kmz";
                string kmzScale;
                if (dictFrameExtents.ContainsKey("scale")) {kmzScale = dictFrameExtents["scale"];} else {kmzScale = null;};
            
                // TODO move this to the MapImageExporter class too, for now it is still in the static MapExport class
                MapAction.MapExport.exportMapFrameKmlAsRaster(pMapDoc, "Main map", @kmzPathFileName, kmzScale, nudKmlResolution.Value.ToString());
                // Add the xml path to the dictFilePaths, which is the input into the creatZip method
                dictFilePaths["kmz"] = kmzPathFileName;
            }
            else
            {
                //// Data driven pages
                IMapDocument pMapDoc = (IMapDocument)pMxDoc;
                MapImageExporter mie = new MapImageExporter(pMapDoc, exportPathFileName, "Main map");
                mie.exportDataDrivenPagesImages();

                dictFilePaths = new Dictionary<string,string>();
                // TODO: this is a bit of a hack to work with multiple page pdf.
                // This will add all PDF files which match teh glob. There is the potenital that this
                // could include some that were not exported by this export event.
                dictFilePaths["pdf"] = exportPathFileName + "*.pdf";
                // just add in extra values here for the metadata export to work.
                // calculating the actual size for DDP is too hard and 
                // not worth while.
                dictFilePaths["jpeg"] = String.Empty;
                dictFilePaths["kmz"] = String.Empty;
                dictFilePaths["png_thumbnail_zip"] = String.Empty;
                dictImageFileSizes["jpeg"] = 0;
                dictImageFileSizes["pdf"] = 0;
                dictImageFileSizes["kmz"] = 0;
            }
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
            dictFilePaths["xml"] = xmlPath;

            // Create zip
            // TODO Note that currently the createZip will zip the xml, jpeg, and pdf. Not the emf! 
            // So why are we making it??
            MapAction.MapExport.createZip(dictFilePaths);
            
            try
            {
                // now that it's been zipped, delete the copy of the thumbnail called thumbnail.png to avoid confusion
                string zippedThumbFile = dictFilePaths[MapActionExportTypes.png_thumbnail_zip.ToString()];
                if (zippedThumbFile != String.Empty)
                {
                    System.IO.File.Delete(zippedThumbFile);
                }
            }
            catch (Exception ex)
            {
                // don't crash if the thumbnail export failed or the intermediate file can't be deleted for some reason
                if (!(ex is ArgumentNullException || ex is UnauthorizedAccessException || ex is KeyNotFoundException))
                {
                    throw;
                }
            }
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

        private Dictionary<string, string> getExportToolValues(
            Dictionary<string, long> dictImageFileSizes, 
            Dictionary<string, string> dictFilePaths,
            Dictionary<string, string> dictFrameExtents, 
            string mxdName)
        {
            
            //tidy up the map title
            string mapTitle1 = tbxMapTitle.Text.Replace(System.Environment.NewLine, " ");
            mapTitle1 = mapTitle1.Replace("  ", " ");

            System.Text.StringBuilder themes = new System.Text.StringBuilder();

            int item = 0;
            foreach (object itemChecked in checkedListBoxThemes.CheckedItems)
            {
                if (item > 0)
                {
                    themes.Append("|");
                }
                // Use the IndexOf method to get the index of an item.
                themes.Append(itemChecked.ToString());
                item++;
            }
            System.Console.WriteLine(themes.ToString());

            // Create a dictionary and add values from Export form
            var dict = new Dictionary<string, string>()
            {
                {"versionNumber",   nudVersionNumber.Value.ToString()},
                {"mapNumber",       tbxMapNumber.Text},
                {"operationID",     tbxOperationId.Text},
                {"sourceorg",       "MapAction"}, //this is hard coded in the existing applicaton
                {"title",           mapTitle1},
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
                //{"jpgfilename",     System.IO.Path.GetFileName(dictFilePaths["jpeg"])},
                //{"pdffilename",     System.IO.Path.GetFileName(dictFilePaths["pdf"])},
                {"qclevel",         cboQualityControl.Text},
                {"qcname",          ""},
                {"access",          cboAccess.Text},
                {"glideno",         tbxGlideNo.Text},
                {"summary",         tbxMapSummary.Text},
                {"imagerydate",     tbxImageDate.Text},
                {"datasource",      tbxDataSources.Text},
                {"location",        tbxImageLocation.Text},
                {"themes",          themes.ToString()},
                {"scale",           tbxScale.Text},
                {"papersize",       tbxPaperSize.Text},
                //{"jpgfilesize",     dictImageFileSizes["jpeg"].ToString()},
                {"jpgresolutiondpi",nudJpegResolution.Value.ToString()},
                //{"pdffilesize",     dictImageFileSizes["pdf"].ToString()},
                {"pdfresolutiondpi",nudPdfResolution.Value.ToString()},
                {"kmlresolutiondpi",nudKmlResolution.Value.ToString()},
                {"mxdfilename",     mxdName},
                {"paperxmax",       ""},
                {"paperxmin",       ""},
                {"paperymax",       ""},
                {"paperymin",       ""},
                //{"kmzfilename",     System.IO.Path.GetFileName(dictFilePaths["kmz"])},
                {"accessnotes",     tbxImageAccessNotes.Text}
            };

            dict["jpgfilename"] = System.IO.Path.GetFileName(dictFilePaths["jpeg"]);
            dict["pdffilename"] = System.IO.Path.GetFileName(dictFilePaths["pdf"]);
            dict["jpgfilesize"] = dictImageFileSizes["jpeg"].ToString();
            dict["pdffilesize"] = dictImageFileSizes["pdf"].ToString();
            dict["kmzfilename"] = System.IO.Path.GetFileName(dictFilePaths["kmz"]);

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
        
        /// <summary>
        /// Exports the active view of the current map to pdf, jpeg, and emf formats, and the 'Main map' view of the map to emf and jpeg.
        /// </summary>
        /// <returns>
        /// Dictionary of type string/string with keys 'pdf', 'jpeg', and 'emf', each having as value the respective exported file exported from 
        /// the active view. 
        /// </returns>
        /// <remarks>
        /// Note that currently the images exported from the 'main map' frame are NOT included in this dictionary, unsure if this is deliberate?
        /// </remarks
        private Dictionary<string, string> exportAllImages()
        {
            IMapDocument pMapDoc = ArcMap.Application.Document as IMapDocument;
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
                // refactored export code into non-static class which handles thumbnail filename and pixel size limits 
                MapImageExporter layoutexporter = new MapImageExporter(pMapDoc, exportPathFileName, null);
                // the ones added to the dictionary will be the ones that get added to the zip file
                dict[MapActionExportTypes.pdf.ToString()] =  
                    layoutexporter.exportImage(MapActionExportTypes.pdf, Convert.ToUInt16(nudPdfResolution.Value));
                dict[MapActionExportTypes.jpeg.ToString()] =  
                    layoutexporter.exportImage(MapActionExportTypes.jpeg, Convert.ToUInt16(nudJpegResolution.Value));
                dict[MapActionExportTypes.emf.ToString()] =  
                    layoutexporter.exportImage(MapActionExportTypes.emf, Convert.ToUInt16(nudEmfResolution.Value));
                // export the thumbnail, using the new functionality of specifying a pixel size rather than a dpi
                XYDimensions thumbSize = new XYDimensions()
                    {
                        Width = MapAction.Properties.Settings.Default.thumbnail_width_px,
                        Height = null // export will be constrained by width only
                    };
                dict[MapActionExportTypes.png_thumbnail_zip.ToString()] =  
                    layoutexporter.exportImage(MapActionExportTypes.png_thumbnail_zip, thumbSize);
                
                // export a local-only copy of the thumbnail which will have a more useful filename so it isn't 
                // overwritten when there's more than one map exported to the same folder
                layoutexporter.exportImage(MapActionExportTypes.png_thumbnail_local, thumbSize);

                // What are these for? we don't zip them.
                MapImageExporter dfExporter = new MapImageExporter(pMapDoc, exportPathFileName, "Main map");
                dfExporter.exportImage(MapActionExportTypes.emf, Convert.ToUInt16(nudEmfResolution.Value));
                dfExporter.exportImage(MapActionExportTypes.jpeg, Convert.ToUInt16(nudEmfResolution.Value));
               
                //Output 3 image formats pdf, jpeg & emf
                //dict.Add("pdf", 
                //    MapAction.MapExport.exportImage(pMapDoc, "pdf", nudPdfResolution.Value.ToString(), exportPathFileName, null));
                //dict.Add("jpeg", 
                //    MapAction.MapExport.exportImage(pMapDoc, "jpeg", nudJpegResolution.Value.ToString(), exportPathFileName, null));
                //dict.Add("emf", 
                //    MapAction.MapExport.exportImage(pMapDoc, "emf", nudEmfResolution.Value.ToString(), exportPathFileName, null));
                //// what are these for?
                //MapAction.MapExport.exportImage(pMapDoc, "emf", nudEmfResolution.Value.ToString(), exportPathFileName, "Main map");
                //MapAction.MapExport.exportImage(pMapDoc, "jpeg", nudEmfResolution.Value.ToString(), exportPathFileName, "Main map");

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

        public string getStatus()
        {
            return cboStatus.Text;
        }

        private void tbxMapTitle_TextChanged(object sender, EventArgs e)
        {
            _titleValidationResult = FormValidationExport.validateMapTitle(tbxMapTitle, eprMaptitleWarning, eprMapTitleError);
        }

        private void tbxMapSummary_TextChanged(object sender, EventArgs e)
        {
            _summaryValidationResult = FormValidationExport.validateMapSummary(tbxMapSummary, eprMapSummaryWarning, eprMapSummaryError);
        }

        private void tbxImageDate_TextChanged(object sender, EventArgs e)
        {
            _imageryDateValidationResult = FormValidationExport.validateImageryDate(tbxImageDate, eprImageryDate);
        }

        private void tbxDataSources_TextChanged(object sender, EventArgs e)
        {
            _dataSourcesValidationResult = FormValidationExport.validateDataSources(tbxDataSources, eprDataSourcesWarning, eprDataSourcesError);
        }

        private void tbxPaperSize_TextChanged(object sender, EventArgs e)
        {
            _paperSizeValidationResult = FormValidationExport.validatePaperSize(tbxPaperSize, eprPaperWarning, eprPaperError);
        }

        private void tbxTime_TextChanged(object sender, EventArgs e)
        {
            _timeValidationResult = FormValidationExport.validateTime(tbxTime, eprTimeWarning, eprTimeError);
        }

        private void tbxDate_TextChanged(object sender, EventArgs e)
        {
            _dateValidationResult = FormValidationExport.validateDate(tbxDate, eprDateWarning, eprDateError);
        }

        private void tbxScale_TextChanged(object sender, EventArgs e)
        {
            _scaleValidationResult = FormValidationExport.validateScale(tbxScale, eprScaleWarning, eprScaleError);
        }

        private void tbxProjection_TextChanged(object sender, EventArgs e)
        {
            _projectionValidationResult = FormValidationExport.validateProjection(tbxProjection, eprProjectionWarning, eprProjectionError);
        }

        private void tbxDatum_TextChanged(object sender, EventArgs e)
        {
            _datumValidationResult = FormValidationExport.validateDatum(tbxDatum, eprDatumWarning, eprDatumError);
        }

        private void tbxMapDocument_TextChanged(object sender, EventArgs e)
        {
            _mapDocumentValidationResult = FormValidationExport.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
        }

        private void tbxOperationId_TextChanged(object sender, EventArgs e)
        {
            _operationIdValidationResult = FormValidationExport.validateOperationId(tbxOperationId, eprOperationIdWarning, eprOperationIdError);
        }

        private void tbxGlideNo_TextChanged(object sender, EventArgs e)
        {
            _glideNumberValidationResult = FormValidationExport.validateGlideNumber(tbxGlideNo, eprGlideNumberWarning, eprGlideNumberError);
        }

        private void tbxImageLocation_TextChanged(object sender, EventArgs e)
        {
            _locationValidationResult = FormValidationExport.validateLocation(tbxImageLocation, eprLocationWarning);
        }

        private void tbxCountries_TextChanged(object sender, EventArgs e)
        {
            _countriesValidationResult = FormValidationExport.validateCountries(tbxCountries, eprCountriesWarning);
        }

        private void cboStatus_TextChanged(object sender, EventArgs e)
        {
            _statusValidationResult = FormValidationExport.validateStatus(cboStatus, eprStatusWarning);
        }

        private void cboAccess_TextChanged(object sender, EventArgs e)
        {
            _accessValidationResult = FormValidationExport.validateAccess(cboAccess, eprAccessWarning);
        }

        private void tbxImageAccessNotes_TextChanged(object sender, EventArgs e)
        {
            _accessNoteValidationResult = FormValidationExport.validateAccessNote(tbxImageAccessNotes, eprAccessNoteWarning);
        }

        private void cboQualityControl_TextChanged(object sender, EventArgs e)
        {
            _qualityControlValidationResult = FormValidationExport.validateQualityControl(cboQualityControl, eprQualityControlWarning);
        }

        private void tbxLanguage_TextChanged(object sender, EventArgs e)
        {
            _languageValidationResult = FormValidationExport.validateLanguage(tbxLanguage, eprLanguageWarning, eprLanguageError);
        }

        private void tabExportTool_Selected(object sender, TabControlEventArgs e)
        {
            //Update the validation summary section images based on the validation status
            FormValidationExport.validationCheck(_titleValidationResult, imgTitleStatus);
            FormValidationExport.validationCheck(_summaryValidationResult, imgSummaryStatus);
            FormValidationExport.validationCheck(_mapDocumentValidationResult, imgDocumentStatus);
            FormValidationExport.validationCheck(_datumValidationResult, imgDatumStatus);
            FormValidationExport.validationCheck(_projectionValidationResult, imgProjectionStatus);
            FormValidationExport.validationCheck(_scaleValidationResult, imgScaleStatus);
            FormValidationExport.validationCheck(_dateValidationResult, imgDateStatus);
            FormValidationExport.validationCheck(_timeValidationResult, imgTimeStatus);
            FormValidationExport.validationCheck(_paperSizeValidationResult, imgPaperSizeStatus);
            FormValidationExport.validationCheck(_imageryDateValidationResult, imgImageDateStatus);
            FormValidationExport.validationCheck(_dataSourcesValidationResult, imgDataSourcesStatus);
            FormValidationExport.validationCheck(_operationIdValidationResult, imgOperationIdStatus);
            FormValidationExport.validationCheck(_glideNumberValidationResult, imgGlideNumberStatus);
            FormValidationExport.validationCheck(_locationValidationResult, imgLocationStatus);
            FormValidationExport.validationCheck(_themeValidationResult, imgThemeStatus);
            FormValidationExport.validationCheck(_countriesValidationResult, imgCountriesStatus);
            FormValidationExport.validationCheck(_statusValidationResult, imgStatusStatus);
            FormValidationExport.validationCheck(_accessValidationResult, imgAccessStatus);
            FormValidationExport.validationCheck(_accessNoteValidationResult, imgAccessNoteStatus);
            FormValidationExport.validationCheck(_qualityControlValidationResult, imgQualityControlStatus);
            FormValidationExport.validationCheck(_languageValidationResult, imgLanguageStatus);
        }

        private void nudVersionNumber_ValueChanged(object sender, EventArgs e)
        {
            if (nudVersionNumber.Value == 1)
            {
                cboStatus.Text = "New";
            }
            else
            {
                cboStatus.Text = "Updated";
            }
        }

        private void btnLayoutRight_Click_1(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageThemes;
        }

        private void btnUserLeft_Click_1(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageThemes;
        }

        private void btnUserRight_Click_1(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageExport;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageLayout;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _themeValidationResult = FormValidationExport.validateTheme(checkedListBoxThemes, eprThemeWarning);
        }


    }
}
