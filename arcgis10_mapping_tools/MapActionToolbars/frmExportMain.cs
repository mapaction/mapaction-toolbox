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
using ESRI.ArcGIS.Framework;
using MapActionToolbar_Core;

namespace MapActionToolbar_Forms
{
    public partial class frmExportMain : Form
    {
        //Set the dataframe that you are searching for in the layouts.  This is used in many methods below.
        //Need a better solution for sorting this out
        private static string _targetMapFrame = "Main map";
        //private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;
        private static IApplication _mApplication;
        
        //create a variable to hold the status of each validation check
        private string _languageISO2;
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
        private string _countryValidationResult;
        private string _statusValidationResult;
        private string _accessValidationResult;
        private string _accessNoteValidationResult;
        private string _qualityControlValidationResult;
        private string _languageValidationResult;
        private const string _statusNew = "New";
        private const string _statusUpdate = "Update";
        private const string _statusCorrection = "Correction";
        private const string _languageCodesXmlFileName = "language_codes.xml";
        private const string _eventConfigJsonFileName = "event_description.json";
        private const string _initialVersionNumber = "1";
        private const string _initialMapNumber = "MA001";

        private string _labelLanguage;
        private string _mapRootURL = "";
        private MapActionToolbar_Core.LanguageCodeLookup languageCodeLookup = null;
        private MapActionToolbarConfig mapActionToolbarConfig = null;
        private CrashMoveFolderConfig crashMoveFolder = null;

        // new constructor which takes a reference to the IApplication the form should be associated with
        public frmExportMain(IApplication arcMapApp)
        {
            _mApplication = arcMapApp;
            string path = MapActionToolbar_Core.Utilities.getCrashMoveFolderConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectCrashMoveFolderConfig())
            {
                string languageFilePath = System.IO.Path.Combine(path, _languageCodesXmlFileName);
                this.languageCodeLookup = MapActionToolbar_Core.Utilities.getLanguageCodeValues(languageFilePath);
                this.mapActionToolbarConfig = MapActionToolbar_Core.Utilities.getToolboxConfig();

                this.crashMoveFolder = MapActionToolbar_Core.Utilities.getCrashMoveFolderConfigValues(path);
                
                if (this.mapActionToolbarConfig.Tools.Count > 0)
                {
                    InitializeComponent();
                    this.checkedListBoxThemes.Items.AddRange(this.mapActionToolbarConfig.Themes().ToArray());
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Crash Move Folder must contain valid cmf_description.json file.",
                                "Configuration file required",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            else if (Directory.Exists(MapActionToolbar_Core.Utilities.getCrashMoveFolderPath()))
            {
                dlg.SelectedPath = MapActionToolbar_Core.Utilities.getCrashMoveFolderPath();
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
            IMxDocument mxDoc = _mApplication.Document as IMxDocument;
            _titleValidationResult = FormValidationExport.validateMapTitle(mxDoc, tbxMapTitle, eprMaptitleWarning, eprMapTitleError);
            _summaryValidationResult = FormValidationExport.validateMapSummary(mxDoc, tbxMapSummary, eprMapSummaryWarning, eprMapSummaryError);
            _mapDocumentValidationResult = FormValidationExport.validateMapDocument(_mApplication, tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
            _datumValidationResult = FormValidationExport.validateDatum(mxDoc, tbxDatum, eprDatumWarning, eprDatumError);
            _projectionValidationResult = FormValidationExport.validateProjection(mxDoc, tbxProjection, eprProjectionWarning, eprProjectionError);
            _scaleValidationResult = FormValidationExport.validateScale(mxDoc, tbxScale, eprScaleWarning, eprScaleError);
            _dateValidationResult = FormValidationExport.validateDate(tbxDate, eprDateWarning, eprDateError);
            _timeValidationResult = FormValidationExport.validateTime(tbxTime, eprTimeWarning, eprTimeError);
            _paperSizeValidationResult = FormValidationExport.validatePaperSize(mxDoc, tbxPaperSize, eprPaperWarning, eprPaperError);
            _imageryDateValidationResult = FormValidationExport.validateImageryDate(tbxImageDate, eprImageryDate);
            _dataSourcesValidationResult = FormValidationExport.validateDataSources(mxDoc, tbxDataSources, eprDataSourcesWarning, eprDataSourcesError);
            _operationIdValidationResult = FormValidationExport.validateOperationId(tbxOperationId, eprOperationIdWarning, eprOperationIdError);
            _glideNumberValidationResult = FormValidationExport.validateGlideNumber(mxDoc, tbxGlideNo, eprGlideNumberWarning, eprGlideNumberError);
            _locationValidationResult = FormValidationExport.validateLocation(tbxImageLocation, eprLocationWarning);
            _themeValidationResult = FormValidationExport.validateTheme(checkedListBoxThemes, eprThemeWarning);
            _countryValidationResult = FormValidationExport.validateCountry(tbxCountry, eprCountryWarning);
            _statusValidationResult = FormValidationExport.validateStatus(cboStatus, eprStatusWarning);
            _accessValidationResult = FormValidationExport.validateAccess(cboAccess, eprAccessWarning);
            _accessNoteValidationResult = FormValidationExport.validateAccessNote(tbxImageAccessNotes, eprAccessNoteWarning);
            _qualityControlValidationResult = FormValidationExport.validateQualityControl(cboQualityControl, eprQualityControlWarning);
            _languageValidationResult = FormValidationExport.validateLanguage(tbxLanguage, eprLanguageWarning, eprLanguageError);

            var dict = new Dictionary<string, string>();
            // added extra parameter to say that in this case all of the ESRI markup should be stripped from the label values
            IMxDocument doc = _mApplication.Document as IMxDocument;
            dict = MapActionToolbar_Core.PageLayoutProperties.getLayoutTextElements(doc, _targetMapFrame, true);

            //Update form text boxes with values from the map
            if (dict.ContainsKey("title")) { tbxMapTitle.Text = dict["title"]; }
            if (dict.ContainsKey("summary")) { tbxMapSummary.Text = dict["summary"]; }
            if (dict.ContainsKey("mxd_name")) { tbxMapDocument.Text = dict["mxd_name"]; }
            if (dict.ContainsKey("data_sources")) { tbxDataSources.Text = dict["data_sources"]; }
            if (dict.ContainsKey("map_no") && (!dict.ContainsKey("map_version")))
            {
                setMapNumberAndVersion(dict["map_no"]);
            }
            if (dict.ContainsKey("map_no") && (dict.ContainsKey("map_version")))
            {
                this.tbxMapNumber.Text = dict["map_no"];
                this.tbxVersionNumber.Text = dict["map_version"];
            }

            if (dict.ContainsKey("language_label"))
            {
                _labelLanguage = dict["language_label"];
            }
            else
            {
                _labelLanguage = "English";
            }

            tbxLanguage.Text = _labelLanguage;

            // Update form values from the config xml
            string path = MapActionToolbar_Core.Utilities.getCrashMoveFolderPath();
            string filePath = System.IO.Path.Combine(path, _eventConfigJsonFileName);
            EventConfig config = MapActionToolbar_Core.Utilities.getEventConfigValues(filePath);
            tbxGlideNo.Text = config.GlideNumber;
            tbxCountry.Text = MapActionToolbar_Core.Utilities.getCountries().nameFromAlpha3Code(config.AffectedCountryIso3);

            this._mapRootURL = config.DefaultPublishingBaseUrl;
            if (this._mapRootURL.Length == 0)
            {
                this._mapRootURL = MapActionToolbar_Core.Utilities.getMDRUrlRoot();
            }

            string operational_id = config.OperationId.ToLower();
            tbxOperationId.Text = config.OperationId.ToLower();
            tbxExportZipPath.Text = getExportDirectory();
            nudJpegResolution.Value = Convert.ToDecimal(config.DefaultJpegResDPI); 
            nudPdfResolution.Value = Convert.ToDecimal(config.DefaultPdfResDPI); 
            nudEmfResolution.Value = Convert.ToDecimal(config.DefaultPdfResDPI);
            _languageISO2 = config.LanguageIso2 == null ? "en" : config.LanguageIso2;
            tbxLanguage.Text = this.languageCodeLookup.lookupA2LanguageCode(_languageISO2, LanguageCodeFields.Language); ;
            
            // Set the status value and the version number from the existing XML if it exists:
            setValuesFromExistingXML();

            // Set the spatial reference information on load
            var dictSpatialRef = new Dictionary<string, string>();
            dictSpatialRef  = MapActionToolbar_Core.Utilities.getDataFrameSpatialReference(doc, _targetMapFrame);
            tbxDatum.Text = dictSpatialRef["datum"];
            tbxProjection.Text = dictSpatialRef["projection"];

            // Set the 'metadata' tab elements
            var date = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var time = System.DateTime.Now.ToString("HH:mm");
            tbxDate.Text = date;
            tbxTime.Text = time;

            // Disable these by default
            this.nudEmfResolution.Enabled = false;
            this.nudKmlResolution.Enabled = false;

            tbxPaperSize.Text = MapActionToolbar_Core.Utilities.getPageSize(doc as IMapDocument, _targetMapFrame);
            tbxScale.Text = MapActionToolbar_Core.Utilities.getScale(doc as IMapDocument, _targetMapFrame);

            // Check if Data Driven Page and enable dropdown accordingly
            IMapDocument mapDoc;
            mapDoc = (doc as MxDocument) as IMapDocument;
            tbxMapbookMode.Enabled = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);
        }

        private void setMapNumberAndVersion(string mapNumberAndVersion)
        {
            string mapNumber = _initialMapNumber;
            string mapVersion = _initialVersionNumber;

            string[] words = mapNumberAndVersion.Split(' ');
            int i = 1;
            for (i = 0; i < words.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (words[i].Length > 0)
                        {
                            mapNumber = words[i];
                        }
                        break;

                    case 1:
                        Regex digitsOnly = new Regex(@"[^\d]");
                        string part2 = digitsOnly.Replace(words[i], "");
                        if (part2.Length > 0)
                        {
                            mapVersion = part2;
                        }
                        break;

                    default:
                        break;
                }
            }
            this.tbxMapNumber.Text = mapNumber;
            this.tbxVersionNumber.Text = mapVersion;
        }

        // Set the "Status" value and the VersionNumber:
        private void setValuesFromExistingXML()
        {
            // Presume Initial Version
            cboStatus.Text = _statusNew;

            string xmlPath = System.IO.Path.Combine(tbxExportZipPath.Text, this.tbxMapNumber.Text, tbxMapDocument.Text + ".xml");

            // Does the xmlPath already exist?
            if (File.Exists(xmlPath))
            {
                // If it does, get the values from the current XML.
                XDocument doc = XDocument.Load(xmlPath);
                foreach (XElement usEle in doc.Root.Descendants())
                {
                    if (usEle.Name.ToString().Equals("status"))
                    {
                        cboStatus.Text = usEle.Value.ToString();
                    }
                    else if (usEle.Name.ToString().Equals("access"))
                    {
                        cboAccess.Text = usEle.Value.ToString();
                    }
                    else if (usEle.Name.ToString().Equals("qclevel"))
                    {
                        cboQualityControl.Text = usEle.Value.ToString();
                    }
                    else if (usEle.Name.ToString().Equals("location"))
                    {
                        tbxImageLocation.Text = usEle.Value.ToString();
                    }
                    else if (usEle.Name.ToString().Equals("accessnotes"))
                    {
                        tbxImageAccessNotes.Text = usEle.Value.ToString();
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
            if (tbxVersionNumber.Text == _initialVersionNumber)
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

            // Start checks before running the the actual create elements
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
            path = System.IO.Path.Combine(path, this.tbxMapNumber.Text);

            System.IO.Directory.CreateDirectory(path);
            Debug.WriteLine("checks on export complete");

            // Get the path and file name to pass to the various functions
            string exportPathFileName = getExportPathFileName(path, System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text));

            // Disable the button after the export checks are complete to prevent multiple clicks
            this.Enabled = false;

            // this method doesn't correctly in checking for permissions, requires updating.  
            // MapAction.Utilities.detectWriteAccessToPath(path);

            // TODO:
            // APS Is there a good reasons for retreving the reference to the IMxDocument
            // via the ArcMap Application? Why not use the `frmExportMain._pMxDoc` member instead?
            // HSG: this has been changed now, in that the application is stored as a member 
            // not the document (because when not running as an addin we don't have the 
            // automatically set reference to the application so we make our own), and we 
            // retrieve the document each time we need it. 
            IMxDocument doc = _mApplication.Document as IMxDocument;
            IActiveView pActiveView = doc.ActiveView;
            // Ssetup dictionaries for metadata XML
            Dictionary<string, string> dictFilePaths;
            // Create a dictionary to store the image file sizes to add to the output xml
            Dictionary<string, long> dictImageFileSizes = new Dictionary<string, long>();

            // Create a dictionary to get and store the map frame extents to pass to the output xml

            IMapDocument mapDoc;
            mapDoc = doc as IMapDocument;
            bool isDDP = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);
            Dictionary<string, string> dictFrameExtents = Utilities.getMapFrameWgs84BoundingBox(mapDoc, "Main map");

            // Update QR Code
            updateQRCode((doc as IDocument).Title);

            if (!isDDP) // Need a way to do this - the form elements are all disabled before export - see ^^
            {
                // Call to export the images and return a dictionary of the file names
                dictFilePaths = exportAllImages();

                // Calculate the file size of each image and add it to the dictionary
                // Don't use dict.add because a) it's another place to keep track of magic strings
                // values, and b) if we accidentally call it twice with same key we get an exception
                foreach (var kvp in dictFilePaths)
                {
                    dictImageFileSizes[kvp.Key] = MapActionToolbar_Core.Utilities.getFileSize(kvp.Value);
                }
                if (checkBoxKml.Checked)
                {
                    System.Windows.Forms.Application.DoEvents();

                    // Export KML
                    IMapDocument pMapDoc = (IMapDocument)doc;
                    string kmzPathFileName = exportPathFileName + ".kmz";
                    string kmzScale;

                    if (dictFrameExtents.ContainsKey("scale")) { kmzScale = dictFrameExtents["scale"]; } else { kmzScale = null; };

                    // TODO move this to the MapImageExporter class too, for now it is still in the static MapExport class
                    MapActionToolbar_Core.MapExport.exportMapFrameKmlAsRaster(pMapDoc, "Main map", @kmzPathFileName, kmzScale, nudKmlResolution.Value.ToString());
                    // Add the xml path to the dictFilePaths, which is the input into the creatZip method
                    dictFilePaths["kmz"] = kmzPathFileName;
                }
            }
            else
            {
                //// Data driven pages
                IMapDocument pMapDoc = (IMapDocument)doc;
                MapImageExporter mie = new MapImageExporter(pMapDoc, exportPathFileName, "Main map");
                // if exact match do a multifile export, else default to single file.
                bool isMultiplePage = (tbxMapbookMode.Text == "Multiple PDF Files");
                mie.exportDataDrivenPagesImages(isMultiplePage);

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
            string mxdName = (doc as IDocument).Title;
            System.Windows.Forms.Application.DoEvents();
            // Create the output xml file & return the xml path           
            string xmlPath = string.Empty;
            try
            {
                Dictionary<string, string> dict = getExportToolValues(dictImageFileSizes, dictFilePaths, dictFrameExtents, mxdName);
                xmlPath = MapActionToolbar_Core.Utilities.createXML(dict, "mapdata", path, System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text), 2);
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
            MapActionToolbar_Core.MapExport.createZip(dictFilePaths);
            
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
            this.Close();

            // the output filepath

            MessageBox.Show("Files successfully output to: " + Environment.NewLine + path,
                "Export complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // If open explorer checkbox is ticked, open windows explorer to the directory 
            if (chkOpenExplorer.Checked)
            {
                MapActionToolbar_Core.MapExport.openExplorerDirectory(tbxExportZipPath.Text);
            }
            sw.Stop();
            string timeTaken = Math.Round((sw.Elapsed.TotalMilliseconds / 1000),2).ToString();
            Debug.WriteLine("Time taken: ", timeTaken);
        }

        private bool updateQRCode(string mxdName)
        {
            bool qrCodeUpdated = false;
            // Update QR Code
            IMxDocument doc = _mApplication.Document as IMxDocument;
            IPageLayout pLayout = doc.PageLayout;
            IGraphicsContainer pGraphics = pLayout as IGraphicsContainer;
            pGraphics.Reset();

            IElement element = new TextElementClass();
            IElementProperties2 pElementProp;
            IPictureElement pPictureElement;
            try
            {
                element = (IElement)pGraphics.Next();
                while (element != null)
                {
                    if (element is IPictureElement)
                    {
                        pPictureElement = element as IPictureElement;
                        pElementProp = element as IElementProperties2;
                        if (pElementProp.Name == "qr_code")
                        {
                            // Now update the QR Code
                            string qrCodeImagePath = Utilities.GenerateQRCode(this._mapRootURL + tbxOperationId.Text.ToLower() +
                                                                              "-" + tbxMapNumber.Text.ToLower()
                                                                              + "?utm_source=qr_code&utm_medium=mapsheet&utm_campaign="
                                                                              + tbxOperationId.Text.ToLower() + "&utm_content=" + tbxMapNumber.Text.ToLower() + "-v"
                                                                              + tbxVersionNumber.Text);
                            pPictureElement.ImportPictureFromFile(qrCodeImagePath);
                            qrCodeUpdated = true;
                        }
                    }
                    element = (IElement)pGraphics.Next();
                }
            }
            catch (Exception eh)
            {
                System.Diagnostics.Debug.WriteLine("Error updating qr_code element");
                System.Diagnostics.Debug.WriteLine(eh);
            }
            return qrCodeUpdated;
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
                {"versionNumber",   tbxVersionNumber.Text},
                {"mapNumber",       tbxMapNumber.Text},
                {"operationID",     tbxOperationId.Text},
                {"sourceorg",       "MapAction"}, //this is hard coded in the existing applicaton
                {"title",           mapTitle1},
                {"ref",             tbxMapDocument.Text},
                {"language",        tbxLanguage.Text},
                {"countries",       tbxCountry.Text},
                {"createdate",      tbxDate.Text},
                {"createtime",      tbxTime.Text},
                {"status",          cboStatus.Text},
                {"xmax",            dictFrameExtents["xMax"]},
                {"xmin",            dictFrameExtents["xMin"]},
                {"ymax",            dictFrameExtents["yMax"]},
                {"ymin",            dictFrameExtents["yMin"]},
                {"proj",            tbxProjection.Text},
                {"datum",           tbxDatum.Text},
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
                {"jpgresolutiondpi",nudJpegResolution.Value.ToString()},
                {"pdfresolutiondpi",nudPdfResolution.Value.ToString()},
                {"kmlresolutiondpi",nudKmlResolution.Value.ToString()},
                {"mxdfilename",     mxdName},
                {"paperxmax",       ""},
                {"paperxmin",       ""},
                {"paperymax",       ""},
                {"paperymin",       ""},
                {"accessnotes",     tbxImageAccessNotes.Text},
                {"product-type",    tbxMapbookMode.Enabled ? "atlas" : "mapsheet"},
                {"language-iso2",   _languageISO2}
            };

            dict["jpgfilename"] = System.IO.Path.GetFileName(dictFilePaths["jpeg"]);
            dict["pdffilename"] = System.IO.Path.GetFileName(dictFilePaths["pdf"]);
            dict["jpgfilesize"] = dictImageFileSizes["jpeg"].ToString();
            dict["pdffilesize"] = dictImageFileSizes["pdf"].ToString();

            if (checkBoxKml.Checked == true)
            {
                dict["kmzfilename"] = System.IO.Path.GetFileName(dictFilePaths["kmz"]);
            }
            return dict;
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
            IMxDocument doc = _mApplication.Document as IMxDocument;
            IMapDocument pMapDoc = doc as IMapDocument;
            //IActiveView pActiveView = pMxDoc.ActiveView;
            var dict = new Dictionary<string, string>();

            string path = System.IO.Path.Combine(tbxExportZipPath.Text, this.tbxMapNumber.Text);
            // Get the path and file name to pass to the various functions
            string exportPathFileName = getExportPathFileName(path, System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text));

            //check to see variable exists
            if (!Directory.Exists(@path) || tbxMapDocument.Text == "" || tbxMapDocument.Text == string.Empty)
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
                if (checkBoxEmf.Checked == true)
                {
                    dict[MapActionExportTypes.emf.ToString()] =
                        layoutexporter.exportImage(MapActionExportTypes.emf, Convert.ToUInt16(nudEmfResolution.Value));
                }
                // export the thumbnail, using the new functionality of specifying a pixel size rather than a dpi
                XYDimensions thumbSize = new XYDimensions()
                    {
                        Width = MapActionToolbar_Core.Properties.Settings.Default.thumbnail_width_px,
                        Height = null // export will be constrained by width only
                    };
                dict[MapActionExportTypes.png_thumbnail_zip.ToString()] =  
                    layoutexporter.exportImage(MapActionExportTypes.png_thumbnail_zip, thumbSize);
                
                // export a local-only copy of the thumbnail which will have a more useful filename so it isn't 
                // overwritten when there's more than one map exported to the same folder
                layoutexporter.exportImage(MapActionExportTypes.png_thumbnail_local, thumbSize);

                // What are these for? we don't zip them.
                MapImageExporter dfExporter = new MapImageExporter(pMapDoc, exportPathFileName, "Main map");
                if (checkBoxEmf.Checked == true)
                {
                    dfExporter.exportImage(MapActionExportTypes.emf, Convert.ToUInt16(nudEmfResolution.Value));
                }
                dfExporter.exportImage(MapActionExportTypes.jpeg, Convert.ToUInt16(nudJpegResolution.Value));
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
            return System.IO.Path.Combine(@path, documentName);
        }

        //### Methods to work with form validation #### Copied directly from the Alpha export tool
        //### At a later time they should be consolidated in the MapAction class library ###

        public static string updateScale()
        
        {
            IMapDocument doc = _mApplication.Document as IMapDocument;
            string pageSize = MapActionToolbar_Core.Utilities.getPageSize(doc, "Main map");
            string scale = MapActionToolbar_Core.Utilities.getScale(doc, "Main map");
            string scaleString = scale + " (At " + pageSize + ")";
            return scaleString;
        }

        public static string getGlideNo()
        {
            string GlideNo = string.Empty;
            string path = MapActionToolbar_Core.Utilities.getEventConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                EventConfig config = MapActionToolbar_Core.Utilities.getEventConfigValues(path);
                GlideNo = config.GlideNumber;
            }
            return GlideNo;
        }


        public static string getExportDirectory()
        {
            string exportDirectory = string.Empty;
            string path = MapActionToolbar_Core.Utilities.getCrashMoveFolderConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectCrashMoveFolderConfig())
            {
                CrashMoveFolderConfig config = MapActionToolbar_Core.Utilities.getCrashMoveFolderConfigValues(path);
                
                exportDirectory = System.IO.Path.Combine(MapActionToolbar_Core.Utilities.getCrashMoveFolderPath(), config.ExportDirectory);           
            }
            exportDirectory = exportDirectory.Replace('/', '\\');
            return exportDirectory;
        }


        public static string getSpatialReference()
        {
            Dictionary<string, string> dictSpatialRef = MapActionToolbar_Core.Utilities.getDataFrameSpatialReference(_mApplication.Document as IMxDocument, "Main map");
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
            _titleValidationResult = FormValidationExport.validateMapTitle(_mApplication.Document as IMxDocument, 
                tbxMapTitle, eprMaptitleWarning, eprMapTitleError);
        }

        private void tbxMapSummary_TextChanged(object sender, EventArgs e)
        {
            _summaryValidationResult = FormValidationExport.validateMapSummary(_mApplication.Document as IMxDocument, 
                tbxMapSummary, eprMapSummaryWarning, eprMapSummaryError);
        }

        private void tbxImageDate_TextChanged(object sender, EventArgs e)
        {
            _imageryDateValidationResult = FormValidationExport.validateImageryDate(tbxImageDate, eprImageryDate);
        }

        private void tbxDataSources_TextChanged(object sender, EventArgs e)
        {
            _dataSourcesValidationResult = FormValidationExport.validateDataSources(_mApplication.Document as IMxDocument, 
                tbxDataSources, eprDataSourcesWarning, eprDataSourcesError);
        }

        private void tbxPaperSize_TextChanged(object sender, EventArgs e)
        {
            _paperSizeValidationResult = FormValidationExport.validatePaperSize(_mApplication.Document as IMxDocument, 
                tbxPaperSize, eprPaperWarning, eprPaperError);
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
            _scaleValidationResult = FormValidationExport.validateScale(_mApplication.Document as IMxDocument, 
                tbxScale, eprScaleWarning, eprScaleError);
        }

        private void tbxProjection_TextChanged(object sender, EventArgs e)
        {
            _projectionValidationResult = FormValidationExport.validateProjection(_mApplication.Document as IMxDocument, 
                tbxProjection, eprProjectionWarning, eprProjectionError);
        }

        private void tbxDatum_TextChanged(object sender, EventArgs e)
        {
            _datumValidationResult = FormValidationExport.validateDatum(_mApplication.Document as IMxDocument, 
                tbxDatum, eprDatumWarning, eprDatumError);
        }

        private void tbxMapDocument_TextChanged(object sender, EventArgs e)
        {
            _mapDocumentValidationResult = FormValidationExport.validateMapDocument(_mApplication, 
                tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
        }

        private void tbxOperationId_TextChanged(object sender, EventArgs e)
        {
            _operationIdValidationResult = FormValidationExport.validateOperationId(tbxOperationId, eprOperationIdWarning, eprOperationIdError);
        }

        private void tbxGlideNo_TextChanged(object sender, EventArgs e)
        {
            _glideNumberValidationResult = FormValidationExport.validateGlideNumber(_mApplication.Document as IMxDocument, 
                tbxGlideNo, eprGlideNumberWarning, eprGlideNumberError);
        }

        private void tbxImageLocation_TextChanged(object sender, EventArgs e)
        {
            _locationValidationResult = FormValidationExport.validateLocation(tbxImageLocation, eprLocationWarning);
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
            FormValidationExport.validationCheck(_countryValidationResult, imgCountriesStatus);
            FormValidationExport.validationCheck(_statusValidationResult, imgStatusStatus);
            FormValidationExport.validationCheck(_accessValidationResult, imgAccessStatus);
            FormValidationExport.validationCheck(_accessNoteValidationResult, imgAccessNoteStatus);
            FormValidationExport.validationCheck(_qualityControlValidationResult, imgQualityControlStatus);
            FormValidationExport.validationCheck(_languageValidationResult, imgLanguageStatus);
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

        private void tabPageLayout_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageThemes;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            tabExportTool.SelectedTab = tabPageLayout;
        }

        private void cboStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxEmf_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEmf.Checked == true)
            {
                this.nudEmfResolution.Enabled = true;
            }
            else
            {
                this.nudEmfResolution.Enabled = false;
            }
        }

        private void checkBoxKml_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKml.Checked == true)
            {
                this.nudKmlResolution.Enabled = true;
            }
            else
            {
                this.nudKmlResolution.Enabled = false;
            }
        }
    }
}
