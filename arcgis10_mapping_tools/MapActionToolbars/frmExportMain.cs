﻿using System;
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
        private const string _initialVersionNumber = "01";
        private const string _initialMapNumber = "MA001";

        private string _labelLanguage;
        private string _mapRootURL = "";
        private MapAction.LanguageCodeLookup languageCodeLookup = null;
        private MapActionToolbarConfig mapActionToolbarConfig = null;
        private CrashMoveFolderConfig crashMoveFolder = null;

        public frmExportMain()
        {
            string path = MapAction.Utilities.getCrashMoveFolderConfigFilePath();

            if (MapAction.Utilities.detectCrashMoveFolderConfig())
            {
                string languageFilePath = System.IO.Path.Combine(path, _languageCodesXmlFileName);
                this.languageCodeLookup = MapAction.Utilities.getLanguageCodeValues(languageFilePath);
                this.mapActionToolbarConfig = MapAction.Utilities.getToolboxConfig();

                this.crashMoveFolder = MapAction.Utilities.getCrashMoveFolderConfigValues(path);
                
                if (this.mapActionToolbarConfig.Tools.Count > 0)
                {
                    InitializeComponent();
                    this.checkedListBoxThemes.Items.AddRange(this.mapActionToolbarConfig.Themes().ToArray());
                }
                else
                {
                    this.Close();
                    // TODO  this will cause a crash when the tool button now calls showdialog
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
            //_mapDocumentValidationResult = FormValidationExport.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
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
            _countryValidationResult = FormValidationExport.validateCountry(tbxCountry, eprCountryWarning, eprCountryError);
            _statusValidationResult = FormValidationExport.validateStatus(cboStatus, eprStatusWarning);
            _accessValidationResult = FormValidationExport.validateAccess(cboAccess, eprAccessWarning);
            _accessNoteValidationResult = FormValidationExport.validateAccessNote(tbxImageAccessNotes, eprAccessNoteWarning);
            _qualityControlValidationResult = FormValidationExport.validateQualityControl(cboQualityControl, eprQualityControlWarning);
            _languageValidationResult = FormValidationExport.validateLanguage(tbxLanguage, eprLanguageWarning, eprLanguageError);

            // added extra parameter to say that in this case all of the ESRI markup should be stripped from the label values
            var layoutElementContents = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, _targetMapFrame, true);

            //Update form text boxes with values from the map
            if (layoutElementContents.ContainsKey("title")) { tbxMapTitle.Text = layoutElementContents["title"]; }
            if (layoutElementContents.ContainsKey("summary")) { tbxMapSummary.Text = layoutElementContents["summary"]; }
            //if (layoutElementContents.ContainsKey("mxd_name")) { tbxMapDocument.Text = layoutElementContents["mxd_name"]; }
            if (layoutElementContents.ContainsKey("data_sources")) { tbxDataSources.Text = layoutElementContents["data_sources"]; }
            if (layoutElementContents.ContainsKey("map_no") && (!layoutElementContents.ContainsKey("map_version")))
            {
                // new templates have map number and version in one element separated by a space
                var n_v = parseMapNumberAndVersion(layoutElementContents["map_no"]);
                this.tbxMapNumber.Text = n_v.Item1;
                this.tbxVersionNumber.Text = n_v.Item2;
            }
            else if (layoutElementContents.ContainsKey("map_no") && (layoutElementContents.ContainsKey("map_version")))
            {
                // old templates had separate elements for number and version
                this.tbxMapNumber.Text = layoutElementContents["map_no"];
                this.tbxVersionNumber.Text = layoutElementContents["map_version"];
            }

            if (layoutElementContents.ContainsKey("language"))
            {
                _labelLanguage = layoutElementContents["language"];
            }
            else
            {
                _labelLanguage = "English";
            }
            tbxLanguage.Text = _labelLanguage;

            // Populate dialog items that are drawn from the config xml
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = System.IO.Path.Combine(path, _eventConfigJsonFileName);
            EventConfig config = MapAction.Utilities.getEventConfigValues(filePath);
            tbxGlideNo.Text = config.GlideNumber;
            tbxCountry.Text = MapAction.Utilities.getCountries().nameFromAlpha3Code(config.AffectedCountryIso3);

            this._mapRootURL = config.DefaultPublishingBaseUrl;
            if (this._mapRootURL.Length == 0)
            {
                this._mapRootURL = MapAction.Utilities.getMDRUrlRoot();
            }

            string operational_id = config.OperationId.ToLower();
            tbxOperationId.Text = config.OperationId.ToLower();
            tbxExportZipPath.Text = getExportDirectory();
            nudJpegResolution.Value = Convert.ToDecimal(config.DefaultJpegResDPI); 
            nudPdfResolution.Value = Convert.ToDecimal(config.DefaultPdfResDPI); 
            nudEmfResolution.Value = Convert.ToDecimal(config.DefaultPdfResDPI);
            _languageISO2 = config.LanguageIso2 == null ? "en" : config.LanguageIso2;
            tbxLanguage.Text = this.languageCodeLookup.lookupA2LanguageCode(_languageISO2, LanguageCodeFields.Language); ;
            
            // Populate dialog items that are drawn directly from the map's properties
            tbxMapDocument.Text = ArcMap.Application.Document.Title; // no longer drawn from a text element
            // Set the spatial reference information on load
            var dictSpatialRef = new Dictionary<string, string>();
            dictSpatialRef  = MapAction.Utilities.getDataFrameSpatialReference(_pMxDoc, _targetMapFrame);
            tbxDatum.Text = dictSpatialRef["datum"];
            tbxProjection.Text = dictSpatialRef["projection"];

            // default to public first, to save darren's clicking
            cboAccess.SelectedIndex = cboAccess.Items.IndexOf("Public");
            // (Re)set the status, version, access, location, theme etc from an earlier export's XML if it exists.
            // It will look in this version's folder first (in case the same version has been exported twice) and if 
            // not found will try the previous version's folder, if it exists. If not found then default values will remain.
            // It uses tbxMapDocument.Text so has to come after that is set
            setValuesFromExistingXML();

            // Set the 'metadata' tab elements
            var date = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var time = System.DateTime.Now.ToString("HH:mm");
            tbxDate.Text = date;
            tbxTime.Text = time;

            // Disable these by default
            this.nudEmfResolution.Enabled = false;
            this.nudKmlResolution.Enabled = false;

            tbxPaperSize.Text = MapAction.Utilities.getPageSize(_pMxDoc as IMapDocument, _targetMapFrame);
            tbxScale.Text = MapAction.Utilities.getActualScale(_pMxDoc as IMapDocument, _targetMapFrame);

            // Check if Data Driven Page and enable dropdown accordingly
            IMapDocument mapDoc;
            mapDoc = (_pMxDoc as MxDocument) as IMapDocument;
            tbxMapbookMode.Enabled = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);
        }

        private Tuple<string, string> tryParseMapNumberVersionFromFilename()
        {
            // Attempt to identify the map number and version as described in the MXD filename (which is 
            // in sync with tbxMapDocument). As the mxd filename is used to generate the output image 
            // filenames it's not good if these don't match the actual MA number and version as specified 
            // on the layout. So warn the user if this is the case.
            // Match filenames starting with MAnnn where nnn is 1 or more digits, followed 
            // by an optional hyphen, followed by vnnn where nnn is 1 ore more digits.
            var root = System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text);
            Regex maNumberVersion = new Regex(@"(?<MANUM>^MA\d+)-?(?<VER>v\d+)");
            var matches = maNumberVersion.Match(root).Groups;
            var fnMapNum = matches["MANUM"];
            var fnMapVer = matches["VER"];
            if (fnMapNum.Success && fnMapVer.Success)
            {
                return new Tuple<string, string>(fnMapNum.Value, fnMapVer.Value);
            }
            return null;
        }

        private Tuple<string,string> parseMapNumberAndVersion(string mapNumVerElemTxt)
        {
            string mapNumber = _initialMapNumber;
            string mapVersion = _initialVersionNumber;

            string[] words = mapNumVerElemTxt.Split(' ');
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
            return new Tuple<string,string>(mapNumber, mapVersion);
            //this.tbxMapNumber.Text = mapNumber;
            //this.tbxVersionNumber.Text = mapVersion;
        }

        private bool checkValueIsValid(XElement ele, ComboBox cbo)
        {
            return cbo.Items.IndexOf(ele.Value.ToString()) != -1;
        }

        private void setValuesFromExistingXML()
        {
            // re-read values from a previous export 
            // TODO look for previous versions if there isn't one of this version
            
            // Presume Initial Version
            cboStatus.Text = _statusNew;

            string vString = getPaddedVersionNumberString();
            string[] xmlPathParts = { tbxExportZipPath.Text, this.tbxMapNumber.Text, vString,
                System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text) + ".xml" };
            
            string xmlPath = System.IO.Path.Combine(xmlPathParts);

            if (!File.Exists(xmlPath))
            {
                // look at the xml for the previous version's export if an export of this version does not already exist
                vString = getPaddedVersionNumberString(-1);
                xmlPathParts[2] = vString;
                xmlPath = System.IO.Path.Combine(xmlPathParts);
            }
            // Does the xmlPath already exist?
            if (File.Exists(xmlPath))
            {
                // If it does, get the values from the current XML.
                XDocument doc = XDocument.Load(xmlPath);
                foreach (XElement usEle in doc.Root.Descendants())
                {
                    if (usEle.Value == "" | usEle.Value == string.Empty)
                    {
                        continue;
                    }
                    if (usEle.Name.ToString().Equals("status") && checkValueIsValid(usEle, cboStatus))
                    {
                        cboStatus.SelectedIndex = cboStatus.Items.IndexOf(usEle.Value.ToString());
                    }
                    else if (usEle.Name.ToString().Equals("access") && checkValueIsValid(usEle, cboAccess))
                    {
                        //cboAccess.Text = usEle.Value.ToString();
                        cboAccess.SelectedIndex = cboAccess.Items.IndexOf(usEle.Value.ToString());
                    }
                    else if (usEle.Name.ToString().Equals("qclevel") && checkValueIsValid(usEle, cboQualityControl))
                    {
                        //cboQualityControl.Text = usEle.Value.ToString();
                        cboQualityControl.SelectedIndex = cboQualityControl.Items.IndexOf(usEle.Value.ToString());
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
                cboStatus.SelectedIndex = cboStatus.Items.IndexOf(_statusNew);
            }
            else
            {
                // if the version number is > 1 and the status is "New" or not in the expected range..
                if ((cboStatus.Text.Equals(_statusNew)) && 
                    ((!(cboStatus.Text.Equals(_statusCorrection)) && (!(cboStatus.Text.Equals(_statusUpdate))))))
                {
                    // Set to 'Update'
                    cboStatus.SelectedIndex = cboStatus.Items.IndexOf(_statusUpdate);
                }
            }
        }

        private bool folderIsWritable(string folderPath)
        {
            //https://stackoverflow.com/questions/1410127/c-sharp-test-if-user-has-write-access-to-a-folder
            try
            {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                System.IO.File.Create(System.IO.Path.Combine(folderPath, "access_chk.txt")).Close();
                System.IO.File.Delete(System.IO.Path.Combine(folderPath, "access_chk.txt"));
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

       

        private int countFilesToOverwrite(string folderPath)
        {
            // Check how many files already exist in this folder that will be overwritten by the export 
            // as it's currently configured (i.e. this combination of map name and DDP type). 

            // TODO, this would be neater if it was a call out to the filename generator code in MapImageExporter.
            // Note that in the case of multi-file DDP export, we can't tell (without reading the data) what the 
            // output filenames will be as they are data dependent, so it's still possible there could be some that won't 
            // get overwritten. Therefore the DDP exporter code will delete any such files before running.
            var root = System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text);
            var mapDoc = _pMxDoc as IMapDocument;
            var xmlPath = System.IO.Path.Combine(folderPath, root) + ".xml";
            var zipPath = System.IO.Path.Combine(folderPath, root) + ".zip";
            bool isDDP = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);
            int nMatchingThisExport = 0;
            string expectedFNPattern;
            string[] matchingFiles;
            if (isDDP)
            {
                // the DDP export replaces any dots within the filename (before the last one) with underscore
                // so we can't just look for any files with the same basename as the tbxMapDocument.Text
                // Look for any exported DDP files, plus the XML and zip, all these will be overwritten, so
                // if that covers all the files in the folder, return false to indicate folder does not contain 
                // "other" files 
                bool isMultipleFileExport = (tbxMapbookMode.Text == "Multiple PDF Files");
                if (isMultipleFileExport)
                {
                    // output files are (mxd name with . replaced by _) + "-mapbook_" + {PAGENAME} + ".pdf"
                    expectedFNPattern = root.Replace('.', '_') + "-mapbook_*.pdf";
                }
                else
                {
                    // single-file DDP export
                    // output file is (mxd name with . replaced by _) + "-mapbook.pdf"
                    expectedFNPattern = root.Replace('.', '_') + "-mapbook.*";
                }
                matchingFiles = Directory.GetFiles(folderPath, expectedFNPattern);
                // xml and zip still get named as in non-ddp export
                var matchingXML = File.Exists(xmlPath) ? 1 : 0;
                var matchingZip = File.Exists(zipPath) ? 1 : 0;
                nMatchingThisExport = matchingFiles.Length + matchingXML + matchingZip;
            }
            else
            {
                //  Non-DDP export files are map filename + one of ("-nnndpi", " -mapframe-nnndpi", "-thumbnail") + extension (".jpeg", ".pdf", ".png") 
                // We'll just get anything matching map filename then exclude ones with mapbook in the name from consideration
                expectedFNPattern = root + "*";
                var nonDDPFiles = Directory.GetFiles(folderPath, expectedFNPattern).Where(fn => !fn.Contains("-mapbook"));
                nMatchingThisExport = nonDDPFiles.Count();
            }
            return nMatchingThisExport;
        }

        private bool checkIfFolderHasOtherFiles(string folderPath)
        {
            // Identify what the output filenames from this export will be, and check if any other files (which 
            // therefore won't be overwritten this time) exist in the output folder. If so, it suggests a different 
            // export has taken place into this folder which may be an error, so user will be alerted.
            var allFiles = Directory.GetFiles(folderPath);
            // If folder contains more files than the number we've identified that will be overwritten by this export,
            // return true to raise a warning dialog
            return allFiles.Length > countFilesToOverwrite(folderPath);
        }

        private String getPaddedVersionNumberString(int offset=0)
        {
            var int_version = int.Parse(this.tbxVersionNumber.Text) + offset;
            return "v" + int_version.ToString("D2");
        }

        private void btnCreateZip_Click(object sender, EventArgs e)
        {
            // The main export function

            // Create and start a stopwatch to measure the function performance
            //### Remove at a later time ###
            Stopwatch sw = Stopwatch.StartNew();

            // Start checks before running the the actual create elements
            if (tbxMapDocument.Text == string.Empty || tbxMapDocument.Text == "")
            {
                MessageBox.Show("A document name is required. It is used as a part of the output file names.",
                    "Update document name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabExportTool.SelectedTab = tabPageLayout;
                tbxMapDocument.Focus();
                return;
            }
            
            var basePath = tbxExportZipPath.Text;
            //check the path exists and check for write permissions
            if (!Directory.Exists(basePath))
            {
                Debug.WriteLine("Exiting createZip function as path is not valid");
                //Show message on invalid export path
                MessageBox.Show("The export folder path does not exist. Please choose another folder.",
                    "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxExportZipPath.Focus();
                return;
            }
            // This will only check that the base folder is writable and not that e.g. an exported file already exists and is locked
            // We will check for that separately
            if (!folderIsWritable(basePath))
            {
                MessageBox.Show("The export folder path does not appear to be writable. Please choose another folder.",
                    "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxExportZipPath.Focus();
                return;
            }

            var res = tryParseMapNumberVersionFromFilename();
            if (!(res is null) && (res.Item1 != this.tbxMapNumber.Text || res.Item2 != getPaddedVersionNumberString()))
            {
                var yesorno = MessageBox.Show(
                    "The Map number/and or version described in this MXD filename don't seem " + //Environment.NewLine +
                    "to match those specified on the map layout. As the MXD filename is used to " + //Environment.NewLine + 
                    "generate the output filenames this may lead to confusion. " + Environment.NewLine + Environment.NewLine + 
                    "You might need to re-save the MXD with a different filename, or use the Layout tool to update the layout." 
                    + Environment.NewLine + Environment.NewLine +
                    "Do you want to continue with export anyway?",
                    "Mismatched MXD filename detected",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
            }

            string[] pathParts = { basePath, this.tbxMapNumber.Text, getPaddedVersionNumberString()};
            var exportFolder = System.IO.Path.Combine(pathParts);

            Directory.CreateDirectory(exportFolder);
            
            // this will also trigger if thumbnail.png got left behind by a failed zip, but that should now be handled
            if (checkIfFolderHasOtherFiles(exportFolder))
            {
                // The ultimate name of a file depends on (the MA number, the version number)->folder, and the ("map name")->filename. 
                // Previously "map name" was drawn from the layout element so a user could change the map name without changing 
                // the MA / Version number, resulting in a different set of files in the same folder.
                // With that disabled and the map name drawn directly from the mxd filename, now a different output filename 
                // existing in the folder would imply one of three things:
                // - that two MXDs (with different file names) have the same MA and V numbers
                // - that a previous export had the opposite DDP on/off status, or the other DDP type, as normal exports are named 
                // different from DDP single file outputs are named different from DDP multi file outputs
                // - that there are some other files in the folder 
                // Either scenario should at least cause a head-scratch-prompting warning to make sure everything is as intended.
                DialogResult dr = MessageBox.Show("The output folder for this map version:" + Environment.NewLine + Environment.NewLine +
                    exportFolder + Environment.NewLine + Environment.NewLine +
                    "already contains files that won't be overwritten by this export. " + Environment.NewLine + Environment.NewLine + 
                    "This might suggest that another MXD already has the same MA number and version, or that an export has " +
                    "already run in a different Mapbook mode. "+ Environment.NewLine + Environment.NewLine + 
                    "Please be sure this is the correct folder / map number / version." +
                    Environment.NewLine + Environment.NewLine + "Continue with export?",
                    "Please check folder contents",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                    // TODO we could open the folder here as a convenience
                }
            }
            int lastTimesFiles = countFilesToOverwrite(exportFolder);
            Debug.WriteLine("checks on export complete");


            // Get the path and file name to pass to the various functions
            string exportPathFileName = System.IO.Path.Combine(@exportFolder, System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text));

            // Disable the button after the export checks are complete to prevent multiple clicks
            this.Enabled = false;

            // TODO:
            // APS Is there a good reasons for retreving the reference to the IMxDocument
            // via the ArcMap Application? Why not use the `frmExportMain._pMxDoc` member instead?
            // Alternatively is the `frmExportMain._pMxDoc` member used or required?
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IActiveView pActiveView = pMxDoc.ActiveView;
            
            // Setup dictionaries for metadata XML
            Dictionary<string, string> dictFilePaths;
            // Create a dictionary to store the image file sizes to add to the output xml
            Dictionary<string, long> dictImageFileSizes = new Dictionary<string, long>();

            // Create a dictionary to get and store the map frame extents to pass to the output xml
            IMapDocument mapDoc;
            mapDoc = (pMxDoc as MxDocument) as IMapDocument;
            Dictionary<string, string> dictFrameExtents = Utilities.getMapFrameWgs84BoundingBox(mapDoc, "Main map");

            // Update QR Code
            updateQRCode();
            bool individualPartsSuccessful = true;
            
            bool isDDP = PageLayoutProperties.isDataDrivenPagesEnabled(mapDoc);
            if (!isDDP) 
            {
                // Call to export the images and return a dictionary of the file names
                dictFilePaths = exportAllImages(exportPathFileName);
                if (dictFilePaths.ContainsValue(null))
                {
                    // the exporter class, called by exportAllImages, returns null in case of error like a caught exception
                    individualPartsSuccessful = false;
                }
                // Calculate the file size of each image and add it to the dictionary
                // Don't use dict.add because a) it's another place to keep track of magic strings
                // values, and b) if we accidentally call it twice with same key we get an exception
                foreach (var kvp in dictFilePaths)
                {
                    dictImageFileSizes[kvp.Key] = MapAction.Utilities.getFileSize(kvp.Value);
                }
                if (checkBoxKml.Checked)
                {
                    System.Windows.Forms.Application.DoEvents();

                    // Export KML
                    IMapDocument pMapDoc = (IMapDocument)pMxDoc;
                    string kmzPathFileName = exportPathFileName + ".kmz";
                    string kmzScale;

                    if (dictFrameExtents.ContainsKey("scale")) { kmzScale = dictFrameExtents["scale"]; } else { kmzScale = null; };

                    // TODO move this to the MapImageExporter class too, for now it is still in the static MapExport class
                    MapAction.MapExport.exportMapFrameKmlAsRaster(pMapDoc, "Main map", @kmzPathFileName, kmzScale, nudKmlResolution.Value.ToString());
                    // Add the xml path to the dictFilePaths, which is the input into the creatZip method
                    dictFilePaths["kmz"] = kmzPathFileName;
                }
            }
            else
            {
                // Data driven pages
                IMapDocument pMapDoc = (IMapDocument)pMxDoc;
                MapImageExporter mie = new MapImageExporter(pMapDoc, exportPathFileName, "Main map");
                // if exact match do a multifile export, else default to single file.
                bool isMultiplePage = (tbxMapbookMode.Text == "Multiple PDF Files");
                var exportType = isMultiplePage ? MapActionExportTypes.pdf_ddp_multifile : MapActionExportTypes.pdf_ddp_singlefile;
                // as for non-ddp export, the exporter now returns null if there's an error rather than just throwing an 
                // uncaught exception
                string exportedFilePattern = mie.exportDataDrivenPagesImages(exportType);
                if (exportedFilePattern is null)
                {
                    individualPartsSuccessful = false;
                }
                dictFilePaths = new Dictionary<string,string>();
                // This will add all PDF files which match the glob. There is the potential that this
                // could include some that were not exported by this export event (but this should be unlikely 
                // given the previous check and warn code, and the changes to filename generation for each type of ddp 
                // export)
                dictFilePaths["pdf"] = exportedFilePattern; // exportPathFileName + "*.pdf";
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
            if (!individualPartsSuccessful)
            {
                this.Close();
                string errorMsg;
                if (lastTimesFiles == 0)
                {
                    // some other error
                    errorMsg = "An error occurred creating one of the output image files. ";
                }
                else 
                {
                    // could have occurred in deleting/overwriting a pre-existing file
                    errorMsg = "An error occurred creating or overwriting one of the output image files. " 
                        + Environment.NewLine + Environment.NewLine +
                    "Please double check that you don't have any of them open from a previous run " +
                    "with the same output settings, and try again";
                }
                MessageBox.Show(errorMsg,
                "Export error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the mxd filename
            //string mxdName = ArcMap.Application.Document.Title; // this is now set on load into tbxMapDocument which is readonly
            string mxdName = tbxMapDocument.Text;
            System.Windows.Forms.Application.DoEvents();
            // Create the output xml file & return the xml path           
            string xmlPath = string.Empty;

            bool xmlSuccessful = true;
            try
            {
                Dictionary<string, string> dict = getExportToolValues(dictImageFileSizes, dictFilePaths, dictFrameExtents);
                xmlPath = MapAction.Utilities.createXML(dict, "mapdata", exportFolder, System.IO.Path.GetFileNameWithoutExtension(tbxMapDocument.Text), 2);
            }
            catch (Exception xml_e)
            {
                Debug.WriteLine("Error writing out xml file.");
                Debug.WriteLine(xml_e.Message);
                xmlSuccessful = false;
            }
            if (!xmlSuccessful)
            {
                this.Close();
                MessageBox.Show("An error occurred creating the XML file. Please double check that you don't have it open " +
                    "if there was a previous export, and try again.",
                "Export error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add the xml path to the dictFilePaths, which is the input into the creatZip method
            dictFilePaths["xml"] = xmlPath;

            bool zipSuccessful = MapAction.MapExport.createZip(dictFilePaths);
            if (!zipSuccessful)
            {
                this.Close();
                MessageBox.Show("Failed to create zipped output! The most likely cause is that you already have the zip open from a previous export. " + Environment.NewLine + 
                    "Please check and run the export again." + Environment.NewLine + 
                    "NOTE that any unzipped files in the export folder are now not consistent with those in any existing zip file.",
                    "Export error", MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return;
            }

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
            
            // the output filepath

            MessageBox.Show("Files successfully output to: " + Environment.NewLine + Environment.NewLine + exportFolder,
                "Export complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

            // If open explorer checkbox is ticked, open windows explorer to the directory 
            if (chkOpenExplorer.Checked)
            {
                MapAction.MapExport.openExplorerDirectory(exportFolder);
            }
            sw.Stop();
            string timeTaken = Math.Round((sw.Elapsed.TotalMilliseconds / 1000),2).ToString();
            Debug.WriteLine("Time taken: ", timeTaken);
        }

        private bool updateQRCode()
        {
            bool qrCodeUpdated = false;
            // Update QR Code
            IPageLayout pLayout = _pMxDoc.PageLayout;
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
                                                                              //+ tbxOperationId.Text.ToLower() + "&utm_content=" + tbxMapNumber.Text.ToLower()
                                                                              //+ "-v" + tbxVersionNumber.Text
                                                                              + "-" + getPaddedVersionNumberString()
                                                                              );
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
            Dictionary<string, string> dictFrameExtents)
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
                {"ref",             tbxMapDocument.Text}, // yes, this is now a duplicate of mapfilename, left as unsure if MDR needs it
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
                {"mapfilename",     tbxMapDocument.Text},
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
        private Dictionary<string, string> exportAllImages(string exportPathFileName)
        {
            IMapDocument pMapDoc = ArcMap.Application.Document as IMapDocument;
            //IActiveView pActiveView = pMxDoc.ActiveView;
            var dict = new Dictionary<string, string>();

            // refactored export code into non-static class which handles thumbnail filename and pixel size limits 
            MapImageExporter layoutexporter = new MapImageExporter(pMapDoc, exportPathFileName, null);
            // the ones added to the dictionary will be the ones that get added to the zip file
            dict["pdf"] =  // not pdf_non_ddp; the xml needs to actually say pdf
                layoutexporter.exportImage(MapActionExportTypes.pdf_non_ddp, Convert.ToUInt16(nudPdfResolution.Value));
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
            if (checkBoxEmf.Checked == true)
            {
                dfExporter.exportImage(MapActionExportTypes.emf, Convert.ToUInt16(nudEmfResolution.Value));
            }
            dfExporter.exportImage(MapActionExportTypes.jpeg, Convert.ToUInt16(nudJpegResolution.Value));
            return dict;
        }

        private void chkEditAllFields_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAllFields.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("Changing these values will create a discrepancy between the page layout and the metadata xml file. " +
                    "Values you change on this form will be reflected in the XML (and thus online in the MDR) but not on the map itself." +
                    "Do you want to continue?", 
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

        //### Methods to work with form validation #### Copied directly from the Alpha export tool
        //### At a later time they should be consolidated in the MapAction class library ###

        public static string getScaleTextLabel()
        
        {
            string pageSize = MapAction.Utilities.getPageSize(_pMxDoc as IMapDocument, "Main map");
            string scale = MapAction.Utilities.getActualScale(_pMxDoc as IMapDocument, "Main map");
            string scaleString = scale + " (At " + pageSize + ")";
            return scaleString;
        }

        public static string getGlideNo()
        {
            string GlideNo = string.Empty;
            string path = MapAction.Utilities.getEventConfigFilePath();

            if (MapAction.Utilities.detectEventConfig())
            {
                EventConfig config = MapAction.Utilities.getEventConfigValues(path);
                GlideNo = config.GlideNumber;
            }
            return GlideNo;
        }


        public static string getExportDirectory()
        {
            string exportDirectory = string.Empty;
            string path = MapAction.Utilities.getCrashMoveFolderConfigFilePath();

            if (MapAction.Utilities.detectCrashMoveFolderConfig())
            {
                CrashMoveFolderConfig config = MapAction.Utilities.getCrashMoveFolderConfigValues(path);
                
                exportDirectory = System.IO.Path.Combine(MapAction.Utilities.getCrashMoveFolderPath(), config.ExportDirectory);           
            }
            exportDirectory = exportDirectory.Replace('/', '\\');
            return exportDirectory;
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

        private void tbxCountry_TextChanged(object sender, EventArgs e)
        {
            _countryValidationResult = FormValidationExport.validateCountry(tbxCountry, eprCountryWarning, eprCountryError);
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
