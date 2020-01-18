using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using MapActionToolbar_Core;

namespace MapActionToolbars
{
    public partial class frmEvent : Form
    {
        private const string ToolName = "Event Configuration Tool";
        private const string OrganisationComponentName = "Organisation";
        private const string DisclaimerTextComponentName = "Disclaimer Text";
        private const string OrganisationUrlComponentName = "Organisation Url";
        private const string DonorTextComponentName = "Donor Text";
        private string _defaultSourceOrganisation = "";
        private string _defaultDisclaimerText = "";
        private string _defaultDonorText = "";
        private string _defaultMapRootUrl = "https://maps.mapaction.org/dataset";
        private string _defaultSourceOrganisationUrl = "https://mapaction.org";
        private const decimal _defaultJpegDpi = 300;
        private const decimal _defaultPdfDpi = 300;
        private const decimal _defaultEmfDpi = 300;
        private const string _defaultExportToolPath = "";
        private const string languageCodesXMLFileName = "language_codes.xml";

        private Boolean _configJsonEditState = false;
        private Boolean _configJsonNewFile = false;
        private MapActionToolbar_Core.LanguageCodeLookup languageCodeLookup = null;
        private MapActionToolbar_Core.Countries countries = null;
        private MapActionToolbarConfig mapActionToolbarConfig = null;

        public frmEvent()
        {
            this.mapActionToolbarConfig = MapActionToolbar_Core.Utilities.getToolboxConfig();

            if (this.mapActionToolbarConfig.Tools.Count == 0)
            {
                this.Close();
            }
            else
            {
                countries = MapActionToolbar_Core.Utilities.getCountries();

                string path = MapActionToolbar_Core.Utilities.getCrashMoveFolderPath();

                string languageFilePath = System.IO.Path.Combine(path, languageCodesXMLFileName);
                this.languageCodeLookup = MapActionToolbar_Core.Utilities.getLanguageCodeValues(languageFilePath);
                this._defaultSourceOrganisation = this.mapActionToolbarConfig.TextBoxItem(ToolName, OrganisationComponentName);
                this._defaultDisclaimerText = this.mapActionToolbarConfig.TextBoxItem(ToolName, DisclaimerTextComponentName);
                this._defaultMapRootUrl = MapActionToolbar_Core.Utilities.getMDRUrlRoot();
                this._defaultSourceOrganisationUrl = this.mapActionToolbarConfig.TextBoxItem(ToolName, OrganisationUrlComponentName);

                InitializeComponent();
                this.cboLanguage.Items.Clear();
                this.cboLanguage.Items.AddRange(this.languageCodeLookup.languages());
                this.cboMapRootUrl.Items.Clear();
                this.cboMapRootUrl.Items.AddRange(this.mapActionToolbarConfig.MapRootURLs().ToArray());
                this.checkedListBoxDonors.Enabled = false;
                this.checkedListBoxDonors.Items.Clear();
                this.checkedListBoxDonors.Items.AddRange(this.mapActionToolbarConfig.Donors().ToArray());
            }
        }

        private void gbxConfigXmlValues_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tbxOperationId.Text = tbxOperationId.Text.ToLower();

            //Set the application configuration file setting 'opXmlConfig' to the textbox path
            if (tbxPathToCrashMove.Text == "")
            {
                MessageBox.Show("The path to the config file is blank.  Please set a valid path, create a new file or cancel to close the tool.", "Empty directory",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else if (!Directory.Exists(tbxPathToCrashMove.Text))
            {
                MessageBox.Show("The crash move folder path is invalid.  Please set a valid path or use cancel to close the tool.", "Invalid directory",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_configJsonEditState != false)
                {
                    MapActionToolbar_Core.Utilities.setCrashMovePathTest(tbxPathToCrashMove.Text);
                    createConfigJson(_configJsonNewFile);
                }
                else if (_configJsonEditState != true)
                {
                    //Save the path of the config file to the applicaton settings file
                    MapActionToolbar_Core.Utilities.setCrashMovePathTest(tbxPathToCrashMove.Text);
                    MessageBox.Show("Config file path successfully updated.", "Config file path",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
        }

        private void frmEvent_Load(object sender, EventArgs e)
        {
            //get the preset path from the configuration file
            string path = MapActionToolbar_Core.Utilities.getCrashMoveFolderPath();
            string filepath = MapActionToolbar_Core.Utilities.getEventConfigFilePath();
            //Check if the config file has been set and if it exists
            if (@path != "" && !MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                //If not, set the dialog to empty
                tbxPathToCrashMove.Text = "< File moved or deleted: " + path + " >";
                populateDialogDefaultValues();
            }
            else if (!MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                //If the path is set and file exists, set the textbox to the path
                tbxPathToCrashMove.Text = string.Empty;
                populateDialogDefaultValues();
            }
            else if (File.Exists(@filepath))
            {
                //If the path is set and file exists, set the textbox to the path
                tbxPathToCrashMove.Text = @path;
                populateDialogExistingConfigJson(@filepath);
                btnSave.Text = "Update";
            }
            else
            {
                //If the path is set but doesn't exist, return a message to the user in the directory area
                tbxPathToCrashMove.Text = "other error";
            }
            
            //Perform validation checks
            FormValidationConfig.validateOperationName(tbxOperationName, eprOperationNameWarning);
            FormValidationConfig.validateGlideNumber(tbxGlideNo, eprGlideNoWarning, eprGlideNoError);
            //FormValidationConfig.validateCountry(tbxCountry, eprCountryWarning);
            FormValidationConfig.validateTimezone(cboTimeZone, eprTimezoneWarning, eprTimezoneError);
            FormValidationConfig.validateOperationID(tbxOperationId, eprOperationIdWarning);
            FormValidationConfig.validateOrganisation(tbxSourceOrganisation, eprOrganisationWarning);
            FormValidationConfig.validateUrl(tbxOrganisationUrl, eprUrlWarning);
            FormValidationConfig.validateUrl(cboMapRootUrl, eprUrlWarning);
            FormValidationConfig.validatePrimaryEmail(tbxPrimaryEmail, eprPrimaryEmailWarning, eprPrimaryEmailError);
            FormValidationConfig.validateDisclaimer(tbxDislaimerText, eprDisclaimerWarning);
            FormValidationConfig.validateDonor(tbxDonorText, eprDonorTextWarning);
        }

        private void btnPathToExistingJson_Click(object sender, EventArgs e)
        {
            //A variable to store the path to an exisiting config xml if it exists
            string pathToConfigJson;

            //set up select folder dialog properties 
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            //set the intial path
            //if the current path in the dialog is populated and valid, use that path
            string setPath = tbxPathToCrashMove.Text;
            if (setPath == "" || setPath == string.Empty)
            {
                dlg.SelectedPath = @"c:\";
            }
            else
            {
                if (Directory.Exists(setPath))
                {
                    dlg.SelectedPath = @setPath;
                }
                else
                {
                    dlg.SelectedPath = @"c:\";
                }
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxPathToCrashMove.Text = dlg.SelectedPath;

                //reset the edit checkbox to false
                chkEditConfigJson.Checked = false;
                //Check if a config file already exists in the directory
                pathToConfigJson = dlg.SelectedPath + @"\event_description.json";
                //If a config file already exists 1) save it immediately as path, 2) read the values in to the form
                if (File.Exists(pathToConfigJson))
                {
                    _configJsonEditState = false;
                    clearAllChildControls(tabConfigJson);
                    populateDialogExistingConfigJson(pathToConfigJson);
                    btnSave.Enabled = true;
                    btnSave.Text = "Set path";
                    tabConfigJson.Enabled = false;
                    _configJsonNewFile = false;
                }
                else
                {
                    _configJsonEditState = false;
                    chkEditConfigJson.Checked = true;
                    clearAllChildControls(tabConfigJson);
                    populateDialogDefaultValues();
                    tbxOperationName.Focus();
                    _configJsonNewFile = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Create";
                }
            }
            else
            {
                return;
            }
        }

        //##Alpha method
        private void clearAllChildControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                // The NumericUpDown Control has Textbox as its child control. This prevents the NumericUpDown 
                // child textbox from being cleared since that problably isn't the intention.
                if (ctrl.Controls.Count > 0 && !(ctrl is NumericUpDown))
                {
                    clearAllChildControls(ctrl);
                }

                if (ctrl is TextBox)
                {
                    ctrl.Text = String.Empty;
                }
                else if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).SelectedIndex = -1;
                }
            }
        }
        //##alpha method
        public void populateDialogExistingConfigJson(string path = null)
        {
            EventConfig newConfig = MapActionToolbar_Core.Utilities.getEventConfigValues(path);

            //Populate the text boxes with the values from the dictionary
            tbxPathToCrashMove.Text = newConfig.CrashMoveFolderDescriptorPath;
            tbxOperationName.Text = newConfig.OperationName;
            tbxGlideNo.Text = newConfig.GlideNumber;
            cboTimeZone.Text = newConfig.TimeZone;
            tbxOperationId.Text = newConfig.OperationId.ToLower();
            tbxPrimaryEmail.Text = newConfig.DeploymentPrimaryEmail;
            tbxSourceOrganisation.Text = newConfig.DefaultSourceOrganisation;
            tbxOrganisationUrl.Text = newConfig.DefaultSourceOrganisationUrl;
            cboMapRootUrl.Text = newConfig.DefaultPublishingBaseUrl;
            tbxDislaimerText.Text = newConfig.DefaultDisclaimerText;
            tbxDonorText.Text = newConfig.DefaultDonorCredits;
            numJpegDpi.Value = decimal.Parse(newConfig.DefaultJpegResDPI);
            numPdfDpi.Value = decimal.Parse(newConfig.DefaultPdfResDPI);
            numEmfDpi.Value = decimal.Parse(newConfig.DefaultEmfResDPI);
            //tbxExportToolPath.Text = newConfig.DefaultPathToExportDir;
            cboLanguage.Text = this.languageCodeLookup.lookupA2LanguageCode(newConfig.LanguageIso2, LanguageCodeFields.Language);
            cboCountry.Text = countries.nameFromAlpha3Code(newConfig.AffectedCountryIso3);

            foreach (var donor in newConfig.Donors)
            {
                for (int i = 0; i < checkedListBoxDonors.Items.Count; i++)
                {
                    if (checkedListBoxDonors.Items[i].ToString().Equals(donor))
                    {
                        checkedListBoxDonors.SetItemChecked(i, true);
                    }
                }
            }
        }

        //##alpha method
        public void populateDialogDefaultValues()
        {
            //Set the dialog default values
            tbxSourceOrganisation.Text = _defaultSourceOrganisation;
            tbxDislaimerText.Text = _defaultDisclaimerText;
            tbxDonorText.Text = _defaultDonorText;
            tbxOrganisationUrl.Text = _defaultSourceOrganisationUrl;
            cboMapRootUrl.Text = _defaultMapRootUrl;
            numJpegDpi.Value = _defaultJpegDpi;
            numPdfDpi.Value = _defaultPdfDpi;
            numEmfDpi.Value = _defaultEmfDpi;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Boolean createConfigJson(Boolean newJson)
        {
            string path = tbxPathToCrashMove.Text;
            string savedPath = string.Empty;
            Boolean createTrueFalse = false;
            string msgBoxHeaderSuccessCreateJson = "New event confliguration";
            string msgBoxTextSuccessCreateJson = "Configuration file successfully created.";
            string msgBoxHeaderSuccessUpdateJson = "Update event configuration";
            string msgBoxTextSuccessUpdateJson = "Configuration file successfully updated.";

            //check directory exists
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please enter a valid directory in the dialog", "Invalid directory");
            }
            else
            {
                EventConfig config = createEventConfig();

                //Call the MapAction create event config method on the utilities class
                try
                {
                    savedPath = MapActionToolbar_Core.Utilities.createEventConfig(config, path, "event_description");
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error.Message);
                    MessageBox.Show(error.Message);
                }
                //Check to see the file was actually created on disk, return a message with the result.  Close all dialogs.
                if (File.Exists(@savedPath) && newJson == true)
                {
                    this.Close();
                    MessageBox.Show(msgBoxTextSuccessCreateJson, msgBoxHeaderSuccessCreateJson,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //set the settings file with the new directory
                    //Properties.Settings.Default.crash_move_folder_path = @savedPath;
                    createTrueFalse = true;
                }
                else if (File.Exists(@savedPath) && newJson == false)
                {
                    this.Close();
                    MessageBox.Show(msgBoxTextSuccessUpdateJson, msgBoxHeaderSuccessUpdateJson,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //set the settings file with the new directory
                    //Properties.Settings.Default.crash_move_folder_path = @savedPath;
                    createTrueFalse = true;
                }
                else
                {
                    MessageBox.Show("Configuration file not created. Error creating file, please check you have write permissions to the directory before trying again.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return createTrueFalse;
        }

        public EventConfig createEventConfig()
        {
            EventConfig config = new EventConfig
            {
                CrashMoveFolderDescriptorPath = tbxPathToCrashMove.Text.Replace("\\", "/"),
                OperationName = tbxOperationName.Text,
                GlideNumber = tbxGlideNo.Text,
                AffectedCountryIso3 = countries.alpha3Code(cboCountry.Text),
                TimeZone = cboTimeZone.Text,
                OperationId = tbxOperationId.Text.ToLower(),
                DefaultSourceOrganisation = tbxSourceOrganisation.Text,
                DefaultSourceOrganisationUrl = tbxOrganisationUrl.Text,
                DefaultPublishingBaseUrl = cboMapRootUrl.Text,
                DeploymentPrimaryEmail = tbxPrimaryEmail.Text,
                DefaultDisclaimerText = tbxDislaimerText.Text,
                DefaultDonorCredits = tbxDonorText.Text,
                DefaultJpegResDPI = numJpegDpi.Value.ToString(),
                DefaultPdfResDPI = numPdfDpi.Value.ToString(),
                DefaultEmfResDPI = numEmfDpi.Value.ToString(),
                LanguageIso2 = this.languageCodeLookup.lookup(cboLanguage.Text, LanguageCodeFields.Alpha2),
                Donors = checkedListBoxDonors.CheckedItems.OfType<string>().ToList()
            };
            return config;
        }

        private void chkEditConfigJson_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditConfigJson.Checked == true)
            {
                this.btnPathToExistingJson.Enabled = true;
                tabConfigJson.Enabled = true;
                this.tbxOperationName.Enabled = true;
                this.tbxGlideNo.Enabled = true;
                cboTimeZone.Enabled = true;
                tbxOperationId.Enabled = true;
                tbxSourceOrganisation.Enabled = true;
                cboMapRootUrl.Enabled = true;
                tbxOrganisationUrl.Enabled = true;
                tbxPrimaryEmail.Enabled = true;
                tbxDislaimerText.Enabled = true;
                //tbxDonorText.Enabled = true;
                numJpegDpi.Enabled = true;
                numPdfDpi.Enabled = true;
                numEmfDpi.Enabled = true;
                //tbxExportToolPath.Enabled = true;
                //btnSetExportToolPath.Enabled = true;
                checkedListBoxDonors.Enabled = true;
                this.cboCountry.Enabled = true;
                this.cboLanguage.Enabled = true;
                _configJsonEditState = true;
                btnSave.Enabled = true;
                if (_configJsonNewFile == false)
                {
                    btnSave.Text = "Update";
                }
                else
                {
                    btnSave.Text = "Create";
                }
                this.cboCountry.Items.AddRange(countries.countryNames().ToArray());
            }
            else
            {
                this.btnPathToExistingJson.Enabled = false;
                this.tbxPathToCrashMove.Enabled = false;
                tbxOperationName.Enabled = false;
                checkedListBoxDonors.Enabled = false;
                tbxGlideNo.Enabled = false;
                cboTimeZone.Enabled = false;
                cboLanguage.Enabled = false;
                this.cboCountry.Enabled = false;
                tbxOperationId.Enabled = false;
                tbxSourceOrganisation.Enabled = false;
                cboMapRootUrl.Enabled = false;
                tbxPrimaryEmail.Enabled = false;
                tbxDislaimerText.Enabled = false;
                tbxOrganisationUrl.Enabled = false;
                tbxDonorText.Enabled = false;
                numJpegDpi.Enabled = false;
                numPdfDpi.Enabled = false;
                numEmfDpi.Enabled = false;
                //tbxExportToolPath.Enabled = false;
                //btnSetExportToolPath.Enabled = false;
            }
        }

        private void cboTimeZone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCountry.Text.Length > 0)
            {
                this.cboTimeZone.Items.Clear();
                var timeZones = countries.timeZones(cboCountry.Text).ToArray();
                this.cboTimeZone.Items.AddRange(timeZones);
                if (timeZones.Length > 0)
                {
                    this.cboTimeZone.Text = timeZones[0];
                }
                //this.cboLanguage.Items.Clear();
                //var languages = countries.languages(cboCountry.Text).ToArray();
                //this.cboLanguage.Items.AddRange(languages);
                //if (languages.Length > 0)
                //{
                //    this.cboLanguage.Text = languages[0];
                //}
                this.cboTimeZone.Enabled = true;
                //this.cboLanguage.Enabled = true;

            }
            else
            {
                this.cboTimeZone.Enabled = false;
                //this.cboLanguage.Enabled = false;
            }
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbxOperationName_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validateOperationName(tbxOperationName, eprOperationNameWarning);
        }

        private void tbxGlideNo_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validateGlideNumber(tbxGlideNo, eprGlideNoWarning, eprGlideNoError);
        }

        private void tbxSourceOrganisation_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validateOrganisation(tbxSourceOrganisation, eprOrganisationWarning);
        }

        private void tbxOrganisationUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPrimaryEmail_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validatePrimaryEmail(tbxPrimaryEmail, eprPrimaryEmailWarning, eprPrimaryEmailError);
        }

        private void tbxOperationId_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validateOperationID(tbxOperationId, eprOperationIdWarning);
        }

        private void tbxDislaimerText_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validateDisclaimer(tbxDislaimerText, eprDisclaimerWarning);
        }

        private void tbxDonorText_TextChanged(object sender, EventArgs e)
        {
            FormValidationConfig.validateDonor(tbxDonorText, eprDonorTextWarning);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBoxDonors_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<string> checkedItems = new List<string>();
            foreach (var item in checkedListBoxDonors.CheckedItems)
                checkedItems.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)
                checkedItems.Add(checkedListBoxDonors.Items[e.Index].ToString());
            else
                checkedItems.Remove(checkedListBoxDonors.Items[e.Index].ToString());

            if (checkedItems.Count > 0)
            {
                tbxDonorText.Text = "Supported by ";
            }
            else
            {
                tbxDonorText.Text = "";
            }
            int checkedItemIter = 0;

            foreach (string item in checkedItems)
            {
                if (checkedItemIter > 0)
                {
                    if (checkedItemIter < (checkedItems.Count - 1))
                    {
                        tbxDonorText.Text = tbxDonorText.Text + ", ";
                    }
                    else
                    {
                        tbxDonorText.Text = tbxDonorText.Text + " and ";
                    }
                }
                tbxDonorText.Text = tbxDonorText.Text + item;

                checkedItemIter++;
            }
        }
    }
}
