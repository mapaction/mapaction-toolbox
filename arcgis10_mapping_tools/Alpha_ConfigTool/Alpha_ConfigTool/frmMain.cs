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
using MapAction;

namespace Alpha_ConfigTool
{
    public partial class frmMain : Form
    {
        private const string _defaultLanguage = "English";
        private const string _defaultSourceOrganisation = "MapAction";
        private const string _defaultDisclaimerText = "The depiction and use of boundaries, names and associated data shown here do not imply endorsement or acceptance by MapAction.";
        private const string _defaultDonorText = "MapAction is grateful for the support of UK aid from the Department for International Development";
        private const decimal _defaultJpegDpi = 300;
        private const decimal _defaultPdfDpi = 300;
        private const string _defaultExportToolPath = "";

        private Boolean _configXmlEditState = false;
        private Boolean _configXmlNewFile = false;
        private Boolean _btnSaveEnalbled = false;
        private Boolean _configPathHasChanged = false;


        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPathToExistingXml_Click(object sender, EventArgs e)
        {
            //A variable to store the path to an exisiting config xml if it exists
            string pathToConfigXml;
            
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
                chkEditConfigXml.Checked = false;
                //Check if a config file already exists in the directory
                pathToConfigXml = dlg.SelectedPath + @"\operation_config.xml";
                //If a config file already exists 1) save it immediately as path, 2) read the values in to the form
                if (File.Exists(@pathToConfigXml))
                {
                    _configXmlEditState = false;
                    _configPathHasChanged = true;
                    clearAllChildControls(tabConfigXml);
                    populateDialogExistingConfigXml(pathToConfigXml);
                    btnSave.Enabled = true;
                    btnSave.Text = "Set path";
                    tabConfigXml.Enabled = false;
                    _configXmlNewFile = false;
                }
                else
                {
                    _configXmlEditState = false;
                    chkEditConfigXml.Checked = true;
                    clearAllChildControls(tabConfigXml);
                    populateDialogDefaultValues();
                    tbxOperationName.Focus();
                    _configXmlNewFile = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Create XML";
                }

            }
            else
            {
                return;
            }
         
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
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
                if (_configXmlEditState != false)
                {
                    MapAction.Properties.Settings.Default.crash_move_folder_path = tbxPathToCrashMove.Text;
                    MapAction.Properties.Settings.Default.Save();
                    createConfigXml(_configXmlNewFile);
                }
                else if (_configXmlEditState != true)
                {
                    //Save the path of the config file to the applicaton settings file
                    MapAction.Properties.Settings.Default.crash_move_folder_path = tbxPathToCrashMove.Text;
                    MapAction.Properties.Settings.Default.Save();
                    MessageBox.Show("Config file path successfully updated.", "Config file path",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                this.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Populate the form with the xml data if it exists 
            //dlgDefaultValuesOrExistingXml();
            
            //get the preset path from the configuration file
            string path = MapAction.Properties.Settings.Default.crash_move_folder_path;
            string filepath = path + @"\operation_config.xml";
            Debug.WriteLine("path: " + path);
            //Check if the config file has been set and if it exists
            if (@path != "" && !MapAction.Utilities.detectOperationConfig())
            {
                //If not, set the dialog to empty
                tbxPathToCrashMove.Text = "< File moved or deleted: " + path + " >";
            }
            else if (!MapAction.Utilities.detectOperationConfig())
            {
                //If the path is set and file exists, set the textbox to the path
                tbxPathToCrashMove.Text = string.Empty;
            }
            else if (File.Exists(@filepath))
            {
                //If the path is set and file exists, set the textbox to the path
                tbxPathToCrashMove.Text = @path;
                populateDialogExistingConfigXml(@filepath);
                btnSave.Text = "Update XML";
            }
            else
            {
                //If the path is set but doesn't exist, return a message to the user in the directory area
                tbxPathToCrashMove.Text = "other error";
            }
             
        }

        public void setPathToConfig(string path)
        {
            tbxPathToCrashMove.Text = path;
        }


        //##alpha method
        public Dictionary<string,string> createConfigXmlDict()
        {
            //Create a dictionary to store the form values
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("OperationName", tbxOperationName.Text);
            dict.Add("GlideNo", tbxGlideNo.Text);
            dict.Add("Language", cboLanguage.Text);
            dict.Add("Country", cboCountry.Text);
            dict.Add("TimeZone", cboTimeZone.Text);
            dict.Add("OperationId", tbxOperationId.Text);
            dict.Add("DeploymentPrimaryEmail", tbxPrimaryEmail.Text);
            dict.Add("DefaultSourceOrganisation", tbxSourceOrganisation.Text);
            dict.Add("DefaultDisclaimerText", tbxDislaimerText.Text);
            dict.Add("DefaultDonorsText", tbxDonorText.Text);
            dict.Add("DefaultJpegResDPI", numJpegDpi.Value.ToString());
            dict.Add("DefaultPdfResDPI", numPdfDpi.Value.ToString());
            dict.Add("DefaultEmfResDPI", numPdfDpi.Value.ToString());
            dict.Add("DefaultPathToExportDir", tbxExportToolPath.Text);

            return dict;
        }

        //##alpha method
        public Boolean createConfigXml(Boolean newXML)
        {
            string path = tbxPathToCrashMove.Text;
            string savedPath = string.Empty;
            Boolean createTrueFalse = false;
            string msgBoxHeaderSuccessCreateXML = "New operation_config.xml";
            string msgBoxTextSuccessCreateXML = "Configuration file successfully created.";
            string msgBoxHeaderSuccessUpdateXML = "Update operation_config.xml";
            string msgBoxTextSuccessUpdateXML = "Configuration file successfully updated.";

            //check directory exists
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please enter a valid directory in the dialog", "Invalid directory");
            }
            else
            {

                //Create a dictionary to store the form values
                Dictionary<string, string> dict = createConfigXmlDict();

                //Call the MapAction create xml method on the utilities class
                try
                {
                    savedPath = MapAction.Utilities.createXML(dict, "emergency", path, "operation_config", 1);
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error.Message);
                }
                //Check to see the file was actually created on disk, return a message with the result.  Close all dialogs.
                if (File.Exists(@savedPath) && newXML == true)
                {
                    this.Close();
                    MessageBox.Show(msgBoxTextSuccessCreateXML, msgBoxHeaderSuccessCreateXML,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //set the settings file with the new directory
                    //MapAction.Properties.Settings.Default.crash_move_folder_path = @savedPath;
                    createTrueFalse = true;
                }
                else if (File.Exists(@savedPath) && newXML == false)
                {
                    this.Close();
                    MessageBox.Show(msgBoxTextSuccessUpdateXML, msgBoxHeaderSuccessUpdateXML,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //set the settings file with the new directory
                    //MapAction.Properties.Settings.Default.crash_move_folder_path = @savedPath;
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


        //##alpha method
        public Boolean dlgDefaultValuesOrExistingXml()
        {
            //Get the currently set path
            string crashMovePath = MapAction.Properties.Settings.Default.crash_move_folder_path;
            string xmlPath = crashMovePath + @"\opertional_config.xml";
            //Check if the path exists 
            if (File.Exists(@xmlPath))
            {
                populateDialogExistingConfigXml(xmlPath);
                return true;
            }
            else
            {
                populateDialogDefaultValues();
                return false;
            }

        }


        //##alpha method
        public void populateDialogExistingConfigXml(string path = null)
        {

            //Create a dictionary to store the xml values of the current config file
            Dictionary<string, string> dict = MapAction.Utilities.getOperationConfigValues(path);
            //Populate the text boxes with the values from the dictionary
            tbxOperationName.Text = dict["OperationName"];
            tbxGlideNo.Text = dict["GlideNo"];
            cboLanguage.Text = dict["Language"];
            cboCountry.Text = dict["Country"];
            cboTimeZone.Text = dict["TimeZone"];
            tbxOperationId.Text = dict["OperationId"];
            tbxPrimaryEmail.Text = dict["DeploymentPrimaryEmail"];
            tbxSourceOrganisation.Text = dict["DefaultSourceOrganisation"];
            tbxDislaimerText.Text = dict["DefaultDisclaimerText"];
            tbxDonorText.Text = dict["DefaultDonorsText"];
            numJpegDpi.Value = decimal.Parse(dict["DefaultJpegResDPI"]);
            numPdfDpi.Value = decimal.Parse(dict["DefaultPdfResDPI"]);
            tbxExportToolPath.Text = dict["DefaultPathToExportDir"];

        }

        //##alpha method
        public void populateDialogDefaultValues()
        {
            //Set the dialog default values
            cboLanguage.Text = _defaultLanguage;
            tbxSourceOrganisation.Text = _defaultSourceOrganisation;
            tbxDislaimerText.Text = _defaultDisclaimerText;
            tbxDonorText.Text = _defaultDonorText;
            numJpegDpi.Value = _defaultJpegDpi;
            numPdfDpi.Value = _defaultPdfDpi;
            tbxExportToolPath.Text = _defaultExportToolPath;

        }

        private void chkEditConfigXml_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditConfigXml.Checked == true)
            {
                //tabConfigXml.Enabled = true;
                tbxOperationName.Enabled = true;
                tbxGlideNo.Enabled = true;
                cboLanguage.Enabled = true;
                cboCountry.Enabled = true;
                cboTimeZone.Enabled = true;
                tbxOperationId.Enabled = true;
                tbxPrimaryEmail.Enabled = true;
                tbxSourceOrganisation.Enabled = true;
                tbxDislaimerText.Enabled = true;
                tbxDonorText.Enabled = true;
                numJpegDpi.Enabled = true;
                numPdfDpi.Enabled = true;
                numEmfDpi.Enabled = true;
                tbxExportToolPath.Enabled = true;
                btnSetExportToolPath.Enabled = true;

                _configXmlEditState = true;
                btnSave.Enabled = true;
                if (_configXmlNewFile == false)
                {
                    btnSave.Text = "Update XML";   
                }
                else
                {
                    btnSave.Text = "Create XML";
                }
            }
            else
            {
                tbxOperationName.Enabled = false;
                tbxGlideNo.Enabled = false;
                cboLanguage.Enabled = false;
                cboCountry.Enabled = false;
                cboTimeZone.Enabled = false;
                tbxOperationId.Enabled = false;
                tbxPrimaryEmail.Enabled = false;
                tbxSourceOrganisation.Enabled = false;
                tbxDislaimerText.Enabled = false;
                tbxDonorText.Enabled = false;
                numJpegDpi.Enabled = false;
                numPdfDpi.Enabled = false;
                numEmfDpi.Enabled = false;
                tbxExportToolPath.Enabled = false;
                btnSetExportToolPath.Enabled = false;
            }
        }

        //##Alpha method
        private void clearAllChildControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Controls.Count > 0) clearAllChildControls(ctrl);
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

        private void btnSetExportToolPath_Click(object sender, EventArgs e)
        {
            //set up select folder dialog properties 
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            //set the intial path 
            dlg.SelectedPath = @"c:\";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxExportToolPath.Text = dlg.SelectedPath;
            }
        }


    }
}
