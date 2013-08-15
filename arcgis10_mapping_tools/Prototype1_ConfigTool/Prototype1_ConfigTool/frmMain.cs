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

namespace Prototype1_ConfigTool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        //For the radio buttons not selected, set the associated text box & button controls 
        //as enabled or disabled and clear the values
        private void rdoPathXml_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPathXml.Checked == true)
            {
             
                tbxPathToExistingXml.Enabled = true;
                btnPathToExistingXml.Enabled = true;
                
                btnGetEditXmlPath.Enabled = false;

                btnCreateNewXmlDoc.Enabled = false;

            }
        }
        
        private void rdoEditXml_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEditXml.Checked == true)
            {

                btnGetEditXmlPath.Enabled = true;

                tbxPathToExistingXml.Enabled = false;
                btnPathToExistingXml.Enabled = false;

                btnCreateNewXmlDoc.Enabled = false;

            }

        }

        private void rdoNewXml_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNewXml.Checked == true)
            {
                btnGetEditXmlPath.Enabled = false;

                tbxPathToExistingXml.Enabled = false;
                btnPathToExistingXml.Enabled = false;

                btnCreateNewXmlDoc.Enabled = true;
            }
        }

        private void btnCreateNewXmlDoc_Click(object sender, EventArgs e)
        {
            frmCreateXml dlg = new frmCreateXml();
            this.Close();
            dlg.TopMost = true;
            dlg.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPathToExistingXml_Click(object sender, EventArgs e)
        {
            //set up select folder dialog properties 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Title = "Path to operation_confix.xml";
            dlg.Filter = "|operation_config.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxPathToExistingXml.Text = dlg.FileName;
            }
            else
            {
                return;
            }
        }

        private void btnGetEditXmlPath_Click(object sender, EventArgs e)
        {
            string settingsFilePath = @Prototype1_ConfigTool.Properties.Settings.Default.xml_path;
            string textboxPath = @tbxPathToExistingXml.Text;
            if (MapAction.Utilities.detectOperationConfig())
            {
                if (@settingsFilePath != @tbxPathToExistingXml.Text)
                {
                    MessageBox.Show("The config file in use is different to path specified above. Please edit the current config file or create a new file.",
                        "Invalid directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //check the path to the existing file exists.  If true open the file in the edit dialog.
                    if (File.Exists(@textboxPath))
                    {
                        //instantiate and show the edit xml form
                        frmEditXml dlg = new frmEditXml();
                        this.Close();
                        dlg.TopMost = true;
                        dlg.Show();
                    }
                    else
                    {
                        MessageBox.Show("The file path for the operation_config.xml is invalid.", "Invalid directory");
                    }
                }
            }
            else
            {
                //set up a timer and flash the background of the tbxPathToExistingXml control red for .25 of a second
                //then take the form focus
                Timer t = new Timer();
                t.Interval = 250;
                t.Tick += delegate(System.Object o, System.EventArgs error)
                {
                    t.Stop();
                    t.Dispose();
                    tbxPathToExistingXml.Focus();
                    tbxPathToExistingXml.BackColor = System.Drawing.Color.White;
                    rdoPathXml.Checked = true;
                };
                t.Start();
                tbxPathToExistingXml.BackColor = ColorTranslator.FromHtml("#FFE5EB");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Set the application configuration file setting 'opXmlConfig' to the textbox path
            if (tbxPathToExistingXml.Text == "")
            {
                MessageBox.Show("The path to the config file is blank.  Please set a valid path, create a new file or cancel to close the tool.", "Empty directory",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else if (!File.Exists(@tbxPathToExistingXml.Text))
            {
                MessageBox.Show("The path to the config file is invalid.  Please set a valid path or use cancel to close the tool.", "Invalid directory",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Save the path of the config file to the applicaton settings file
                Properties.Settings.Default.xml_path = this.tbxPathToExistingXml.Text;
                Properties.Settings.Default.Save();
                this.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //get the preset path from the configuration file
            string path = Properties.Settings.Default.xml_path;
            Debug.WriteLine("path: " + path + "@path: " + @path);
            //Check if the config file has been set and if it exists
            if (path != "" && !MapAction.Utilities.detectOperationConfig())
            {
                //If not, set the dialog to empty
                tbxPathToExistingXml.Text = tbxPathToExistingXml.Text = "< File moved or deleted: " + path + " >";
            }
            else if (!MapAction.Utilities.detectOperationConfig())
            {
                //If the path is set and file exists, set the textbox to the path
                tbxPathToExistingXml.Text = string.Empty;
            }
            else if (File.Exists(@path))
            {
                //If the path is set and file exists, set the textbox to the path
                tbxPathToExistingXml.Text = path;
            }
            else
            {
                //If the path is set but doesn't exist, return a message to the user in the directory area
                tbxPathToExistingXml.Text = string.Empty;
            }
             
        }

        public void setPathToConfig(string path)
        {
            tbxPathToExistingXml.Text = path;
        }
    }
}
