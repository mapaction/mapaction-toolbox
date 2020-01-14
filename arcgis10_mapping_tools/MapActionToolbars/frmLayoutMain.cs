using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
using ESRI.ArcGIS.Framework;
using System.Diagnostics;
using MapAction;

namespace MapActionToolbars
{
    public partial class frmLayoutMain : Form
    {
        private static IApplication _pMxApplication;  // initialisation moved to  constructor for flexibility
        private List<string> languages;
        private string _languageIso2;
        private static string _operationId;
        private const string languageConfigXmlFileName = "language_config.xml";
        private const string elementLanguageLabel = "language_label";
        private static string _mapRootURL = "https://maps.mapaction.org/dataset";
        private const string defaultMapNumber = "MA001";
        private const string defaultMapVersion = "1";

        /// <summary>
        /// parameterless constructor which is called by the addin framework button as before
        /// </summary>
        public frmLayoutMain() : this(ArcMap.Application)
        {
        }

        /// <summary>
        /// constructor taking a reference to the IApplication the form should be associated with
        /// </summary>
        /// <remarks>This is necessary because the IApplication object "ArcMap.Application" is provided by the addin framework and not 
        /// available through the arcobjects BaseCommand / extension approach, so we need to be able to pass 
        /// in the IApplication as a parameter when calling from there.</remarks>
        /// <param name="arcMapApp"></param>
        public frmLayoutMain(IApplication arcMapApp)
        {
            _pMxApplication = arcMapApp;//
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = System.IO.Path.Combine(path, languageConfigXmlFileName);
            _mapRootURL = MapAction.Utilities.getMDRUrlRoot();

            // Set up Language of labels
            this.languageDictionary = MapAction.Utilities.getLanguageConfigValues(filePath);
            this.languages = new List<string>();
            for (int i = 0; i < languageDictionary.Count; i++)
            {
                languages.Add(languageDictionary[i].getLanguage());
            }
            InitializeComponent();
            this.cboLabelLanguage.Items.AddRange(languages.ToArray());
        }

        //Gets the automated values for Tab 1 and populates each textbox
        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            IMxDocument doc = _pMxApplication.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.PageLayoutProperties.getLayoutTextElements(doc, "Main map");
            
            tbxScale.Text = tbxScale.Text = updateScale();
            tbxSpatialReference.Text = getSpatialReference();
            tbxMapDocument.Text = tbxMapDocument.Text = MapAction.PageLayoutProperties.getMxdTitle(_pMxApplication);
            tbxGlideNumber.Text = LayoutToolAutomatedValues.getGlideNo();
        }

        private void btnMapDocument_Click(object sender, EventArgs e)
        {
            tbxMapDocument.Text = MapAction.PageLayoutProperties.getMxdTitle(_pMxApplication);
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
            tbxGlideNumber.Text = LayoutToolAutomatedValues.getGlideNo();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Read the Operation Config file 
            string path = MapAction.Utilities.getEventConfigFilePath();
            if (MapAction.Utilities.detectEventConfig())
            {
                EventConfig config = MapAction.Utilities.getEventConfigValues(path);
                _languageIso2 = config.LanguageIso2;
                _operationId = config.OperationId.ToLower();
                _mapRootURL = config.DefaultPublishingBaseUrl;
                if (_mapRootURL.Length == 0)
                {
                    _mapRootURL = MapAction.Utilities.getMDRUrlRoot();
                }
            }

            //Perform validation checks tab 1
            FormValidationLayout.validateMapTitle(tbxTitle, eprMapTitle);
            FormValidationLayout.validateMapSummary(tbxSummary, eprMapSummary);
            FormValidationLayout.validateDataSources(tbxDataSources, eprDataSources);
            FormValidationLayout.validateMapNumber(tbxMapNumber, eprMapNumberWarning, eprMapNumberError);
            FormValidationLayout.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
            FormValidationLayout.validateSpatialReference(tbxSpatialReference, eprSpatialReferenceWarning, eprSpatialReferenceError);
            FormValidationLayout.validateScaleText(tbxScale, eprScaleTextWarning, eprScaleTextError);
            FormValidationLayout.validateGlideNumber(tbxGlideNumber, eprGlideNumberWarning, eprSpatialReferenceError);
            //Perform validation checks tab 2
            FormValidationLayout.validateDisclaimer(tbxDisclaimer, eprDisclaimerWarning, eprDisclaimerError);
            FormValidationLayout.validateDonorCredit(tbxDonorCredit, eprDonorWarning, eprDonorError);
            FormValidationLayout.validateMapProducer(tbxMapProducer, eprProducedByWarning, eprProducedByError);
            FormValidationLayout.validateTimezone(tbxTimezone, eprTimezoneWarning, eprTimezoneError);

            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            IMxDocument doc = _pMxApplication.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.PageLayoutProperties.getLayoutTextElements(doc, "Main map");
            
            //Check if the various elements exist that automated update, if not disable the automation buttons.
            //If they are present then update the text boxes with the value from the dictionary 
            if (!dict.ContainsKey("mxd_name") || !dict.ContainsKey("scale") || !dict.ContainsKey("scale") || !dict.ContainsKey("spatial_reference"))
            {
                btnUpdateAll.Enabled = false;
            }
            if (dict.ContainsKey("title") == true) { tbxTitle.Text = dict["title"]; } else { tbxTitle.Text = "Element not present"; tbxTitle.ReadOnly = true; };
            if (dict.ContainsKey("summary") == true) { tbxSummary.Text = dict["summary"]; } else { tbxSummary.Text = "Element not present"; tbxSummary.ReadOnly = true; };
            if (dict.ContainsKey("data_sources") == true) { tbxDataSources.Text = dict["data_sources"]; } else { tbxDataSources.Text = "Element not present"; tbxDataSources.ReadOnly = true; };
            if (dict.ContainsKey("map_no") == true)
            {
                setMapNumberAndVersion(dict["map_no"]);
                dict["map_no"] = tbxMapNumber.Text;
                dict["map_version"] = nudVersionNumber.Text;
            }
            else
            {
                tbxMapNumber.Text = "Element not present";
                tbxMapNumber.ReadOnly = true;
            }
            if (dict.ContainsKey("mxd_name") == true) { tbxMapDocument.Text = dict["mxd_name"]; } else { tbxMapDocument.Text = "Element not present"; tbxMapDocument.ReadOnly = true; btnMapDocument.Enabled = false; };
            if (dict.ContainsKey("scale") == true) { tbxScale.Text = dict["scale"]; } else { tbxScale.Text = "Element not present"; tbxScale.ReadOnly = true; btnUpdateScale.Enabled = false; };
            if (dict.ContainsKey("spatial_reference") == true) { tbxSpatialReference.Text = dict["spatial_reference"]; } else { tbxSpatialReference.Text = "Element not present"; tbxSpatialReference.ReadOnly = true; btnSpatialReference.Enabled = false; };
            if (dict.ContainsKey("glide_no") == true) { tbxGlideNumber.Text = dict["glide_no"]; } else { tbxGlideNumber.Text = "Element not present"; tbxGlideNumber.ReadOnly = true; btnGlideNo.Enabled = false; };
            //Tab 2 - Standard elements
            if (dict.ContainsKey("map_producer") == true) { tbxMapProducer.Text = dict["map_producer"]; } else { tbxMapProducer.Text = "Element not present"; tbxMapProducer.ReadOnly = true; btnUpdateProducedBy.Enabled = false; };
            if (dict.ContainsKey("donor_credit") == true) { tbxDonorCredit.Text = dict["donor_credit"]; } else { tbxDonorCredit.Text = "Element not present"; tbxDonorCredit.ReadOnly = true; btnUpdateDonorCredits.Enabled = false; };
            if (dict.ContainsKey("timezone") == true) { tbxTimezone.Text = dict["timezone"]; } else { tbxTimezone.Text = "Element not present"; tbxTimezone.ReadOnly = true; btnUpdateTimezone.Enabled = false; };
            if (dict.ContainsKey("disclaimer") == true) { tbxDisclaimer.Text = dict["disclaimer"]; } else { tbxDisclaimer.Text = "Element not present"; tbxDisclaimer.ReadOnly = true; btnUpdateDisclaimer.Enabled = false; };

            if (dict.ContainsKey(elementLanguageLabel) == true) 
            {
                if (this.languages.Contains(dict[elementLanguageLabel]))
                {
                    for (int i = 0; i < this.languages.Count; i++)
                    {
                        if (this.languages[i] == dict[elementLanguageLabel])
                        {
                            this.cboLabelLanguage.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    this.cboLabelLanguage.SelectedIndex = 0;
                }         
            } 
        }

        private void tspBtnClearForm_Click(object sender, EventArgs e)
        {
            //Clear tab 1
            this.tbxTitle.Text = string.Empty;
            this.tbxSummary.Text = string.Empty;
            this.tbxDataSources.Text = string.Empty;
            this.tbxMapNumber.Text = string.Empty;
            this.nudVersionNumber.Text = string.Empty;
            this.tbxMapDocument.Text = string.Empty;
            this.tbxScale.Text = string.Empty;
            this.tbxSpatialReference.Text = string.Empty;
            this.tbxGlideNumber.Text = string.Empty;
            //Clear tab 2
            tbxMapProducer.Text = string.Empty;
            tbxDisclaimer.Text = string.Empty;
            tbxDonorCredit.Text = string.Empty;
            tbxTimezone.Text = string.Empty;
        }

        private void disposeAllErrorProviders()
        {
            FormValidationLayout.disposeErrorProvider(eprDataSources);
            FormValidationLayout.disposeErrorProvider(eprGlideNumberWarning);
            FormValidationLayout.disposeErrorProvider(eprMapDocumentWarning);
            FormValidationLayout.disposeErrorProvider(eprMapNumberError);
            FormValidationLayout.disposeErrorProvider(eprMapSummary);
            FormValidationLayout.disposeErrorProvider(eprMapTitle);
            FormValidationLayout.disposeErrorProvider(eprScaleTextError);
            FormValidationLayout.disposeErrorProvider(eprSpatialReferenceWarning);
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
            dict.Add("map_version", this.nudVersionNumber.Text);
            dict.Add("mxd_name", this.tbxMapDocument.Text);
            dict.Add("scale", this.tbxScale.Text);
            dict.Add("spatial_reference", this.tbxSpatialReference.Text);
            dict.Add("glide_no", this.tbxGlideNumber.Text);
            dict.Add("timezone", this.tbxTimezone.Text);
            dict.Add("donor_credit", this.tbxDonorCredit.Text);
            dict.Add("disclaimer", this.tbxDisclaimer.Text);
            dict.Add("map_producer", this.tbxMapProducer.Text);

            setLabelLanguage();

            setAllElements(dict);
            this.disposeAllErrorProviders();
            this.Close();
        }

        private void setMapNumberAndVersion(string mapNumberAndVersion)
        {
            string mapNumber = defaultMapNumber;
            string mapVersion = defaultMapVersion;

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
            this.nudVersionNumber.Text = mapVersion;
        }

        private void tspBtnCheckElements_Click(object sender, EventArgs e)
        {
            frmCheckElements dlg = new frmCheckElements();
            dlg.ShowDialog();
        }

        public static string getSpatialReference()
        {
            IMxDocument doc = _pMxApplication.Document as IMxDocument;
            Dictionary<string, string> dictSpatialRef = MapAction.Utilities.getDataFrameSpatialReference(doc as IMxDocument, "Main map");
            string stringSpatialRef;

            if (dictSpatialRef["type"] == "Geographic")
            {
                stringSpatialRef = dictSpatialRef["datum"];
            }
            else if (dictSpatialRef["type"] == "Projected")
            {
                stringSpatialRef = dictSpatialRef["projection"] + " / " + dictSpatialRef["datum"]; 
            }
            else
            {
                stringSpatialRef = "Unknown";
            }

            return stringSpatialRef;
        }

        public static void setAllElements(Dictionary<string, string> dict)
        {
            IMxDocument doc = _pMxApplication.Document as IMxDocument;
            IPageLayout pLayout = doc.PageLayout;
            IGraphicsContainer pGraphics = pLayout as IGraphicsContainer;
            pGraphics.Reset();

            IMapDocument mapDoc;
            mapDoc = (doc as MxDocument) as IMapDocument;
    
            // Update QR Code
            IElement element = new TextElementClass();
            IElementProperties2 pElementProp;
            ITextElement pTextElement;
            IPictureElement pPictureElement;
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
                            pTextElement.Text = dict["map_no"] + " v" + dict["map_version"];
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
                        else if (pElementProp.Name == "map_producer")
                        {
                            pTextElement.Text = dict["map_producer"];
                        }
                        else if (pElementProp.Name == "disclaimer")
                        {
                            pTextElement.Text = dict["disclaimer"];
                        }
                        else if (pElementProp.Name == "donor_credit")
                        {
                            pTextElement.Text = dict["donor_credit"];
                        }
                        else if (pElementProp.Name == "timezone")
                        {
                            pTextElement.Text = dict["timezone"];
                        }
                    }
                    else if (element is IPictureElement)
                    {
                        pPictureElement = element as IPictureElement;
                        pElementProp = element as IElementProperties2;
                        if (pElementProp.Name == "qr_code")
                        {
                            // Now update the QR Code
                            string qrCodeImagePath = Utilities.GenerateQRCode(_mapRootURL + _operationId.ToLower() +
                                                                              "-" + dict["map_no"].ToLower()
                                                                              + "?utm_source=qr_code&utm_medium=mapsheet&utm_campaign="
                                                                              + _operationId.ToLower() + "&utm_content=" + dict["map_no"].ToLower() + "-v"
                                                                              + dict["map_version"]);

                            pPictureElement.ImportPictureFromFile(qrCodeImagePath);
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

            IActiveView activeView = doc.ActivatedView as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public static string updateScale()
        {
            //string scale = MapAction.PageLayoutProperties.getScale(ArcMap.Application.Document as IMapDocument, "Main map");
            IMapDocument doc = _pMxApplication.Document as IMapDocument;

            string scale = MapAction.Utilities.getScale(doc, "Main map");

            string pageSize = MapAction.Utilities.getPageSize(doc, "Main map");
            string scaleString = scale + " (At " + pageSize + ")";
            return scaleString;
        }

        //Perform validation checks on text change in each form element
        private void tbxMapNumber_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateMapNumber(tbxMapNumber, eprMapNumberWarning, eprMapNumberError);
        }

        private void tbxTitle_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateMapTitle(tbxTitle, eprMapTitle);
        }

        private void tbxSummary_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateMapSummary(tbxSummary, eprMapSummary);
        }

        private void tbxDataSources_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateDataSources(tbxDataSources, eprDataSources);
        }

        private void tbxSpatialReference_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateSpatialReference(tbxSpatialReference, eprSpatialReferenceWarning, eprSpatialReferenceError); 
        }

        private void tbxMapDocument_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateMapDocument(tbxMapDocument, eprMapDocumentWarning, eprMapDocumentError);
        }

        private void tbxScale_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateScaleText(tbxScale, eprScaleTextWarning, eprScaleTextError);
        }

        private void tbxGlideNumber_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateGlideNumber(tbxGlideNumber, eprGlideNumberWarning, eprGlideNumberError);
        }

        //Update disclaimer tab 2 automated  value button 

        //Gets the automated values for Tab 2 and populates each textbox
        private void btnUpdateAllTab2_Click(object sender, EventArgs e)
        {
            IMxDocument doc = _pMxApplication.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.PageLayoutProperties.getLayoutTextElements(doc, "Main map");
            //If the elements are present in the map, update the values
            if (dict.ContainsKey("donor_credit") == true) { tbxDonorCredit.Text = LayoutToolAutomatedValues.getConfigDonorText(); } 
            if (dict.ContainsKey("timezone") == true) { tbxTimezone.Text = LayoutToolAutomatedValues.getConfigTimezone(); } 
            if (dict.ContainsKey("map_producer") == true) { tbxMapProducer.Text = LayoutToolAutomatedValues.getProducedByText(); }
            if (dict.ContainsKey("disclaimer") == true) { tbxDisclaimer.Text = LayoutToolAutomatedValues.getConfigDisclaimer(); } 
        }

        private void btnUpdateDisclaimer_Click(object sender, EventArgs e)
        {
            tbxDisclaimer.Text = LayoutToolAutomatedValues.getConfigDisclaimer();
        }

        private void btnUpdateTimezone_Click(object sender, EventArgs e)
        {
            tbxTimezone.Text = LayoutToolAutomatedValues.getConfigTimezone();
        }

        private void btnUpdateOrganisationDetails_Click(object sender, EventArgs e)
        {
            tbxMapProducer.Text = LayoutToolAutomatedValues.getProducedByText();
        }

        private void btnUpdateDonorCredits_Click(object sender, EventArgs e)
        {
            tbxDonorCredit.Text = LayoutToolAutomatedValues.getConfigDonorText();
        }
        
        private void tbxDisclaimer_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateDisclaimer(tbxDisclaimer, eprDisclaimerWarning, eprDisclaimerError);
        }

        private void tbxDonorCredit_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateDonorCredit(tbxDonorCredit, eprDonorWarning, eprDonorError);
        }

        private void tbxMapProducer_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateMapProducer(tbxMapProducer, eprProducedByWarning, eprProducedByError);
        }

        private void tbxTimezone_TextChanged(object sender, EventArgs e)
        {
            FormValidationLayout.validateTimezone(tbxTimezone, eprTimezoneWarning, eprTimezoneError);
        }

        private void cboLabelLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        
        public void setLabelLanguage()
        {
            IMxDocument doc = _pMxApplication.Document as IMxDocument;
            IPageLayout pLayout = doc.PageLayout;
            IGraphicsContainer pGraphics = pLayout as IGraphicsContainer;
            pGraphics.Reset();

            IElement element = new TextElementClass();
            IElementProperties2 pElementProp;
            ITextElement pTextElement;

            Dictionary<string, string> labelLookup = null;
            for (int i = 0; i < this.languageDictionary.Count; i++)
            {
                if (this.languageDictionary[i].getLanguage() == this.cboLabelLanguage.Text)
                {
                    labelLookup = this.languageDictionary[i].getDictionary();
                    break;
                }
            }

            try
            {
                element = (IElement)pGraphics.Next();
                while (element != null)
                {
                    if (element is ITextElement)
                    {
                        pTextElement = element as ITextElement;
                        pElementProp = element as IElementProperties2;
                        if (labelLookup.ContainsKey(pElementProp.Name) == true)
                        {
                            pTextElement.Text = labelLookup[pElementProp.Name];
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

            IActiveView activeView = doc.ActivatedView as IActiveView;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
    }
}