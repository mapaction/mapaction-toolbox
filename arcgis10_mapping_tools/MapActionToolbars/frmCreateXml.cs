using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Diagnostics;

namespace MapActionToolbars
{
    public partial class frmCreateXml : Form
    {
        public frmCreateXml()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string path = tbxNewFileFolder.Text;
            string savedPath = string.Empty;
            //check directory exists
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please enter a valid directory in the dialog", "Invalid directory");
            }
            else
            {
                
                //Create a dictionary to store the form values
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("operation_name", tbxOperationName.Text);
                dict.Add("operation_id", tbxOperationID.Text);
                dict.Add("glide_no", tbxGlideNo.Text);

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
                if (File.Exists(@savedPath))
                {
                    this.Close();
                    MessageBox.Show("Configuration file successfully created.", "New operation_config.xml",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //set the settings file with the new directory
                    MapActionToolbars.Properties.Settings.Default.crash_move_folder_path = @savedPath;
                }
                else
                {
                    MessageBox.Show("Configuration file not created. Error creating file, please check you have write permissions to the directory before trying again.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public string setOperationName()
        {
            return tbxOperationName.Text;
        }

        private void btnCreateNewXmlPath_Click(object sender, EventArgs e)
        {
            //set up select folder dialog properties 
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            //set the intial path
            dlg.SelectedPath = @"c:\";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxNewFileFolder.Text = dlg.SelectedPath;
            }
            else
            {
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCreateXml_Load(object sender, EventArgs e)
        {

        }

    }
}
