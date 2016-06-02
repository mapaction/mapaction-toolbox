using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace MapActionToolbars
{
    public partial class frmEditXml : Form
    {
        public frmEditXml()
        {
            InitializeComponent();
        }

        private void frmEditXml_Load(object sender, EventArgs e)
        {
            //Populate the form fields with the xml file
            string configPath = MapActionToolbars.Properties.Settings.Default.crash_move_folder_path;
            //Set the textbox to show the path to the file being edited
            tbxEditXmlCurrentPath.Text = configPath;
            //Create a dictionary to store the xml values of the current config file
            Dictionary<string, string> dict = MapAction.Utilities.getOperationConfigValues();
            //Populate the text boxes with the values from the dictionary
            tbxOperationName.Text = dict["operation_name"];
            tbxOperationID.Text = dict["operation_id"];
            tbxGlideNo.Text = dict["glide_no"];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = MapActionToolbars.Properties.Settings.Default.crash_move_folder_path;
            
            try
            {
                //Load the xml file 
                XDocument doc = XDocument.Load(filePath);
                foreach (XElement i in doc.Root.Descendants())
                {
                    //Assign values from the form to the xml elements
                    if (i.Name == "operation_name")
                    {
                        i.Value = tbxOperationName.Text;
                    }
                    else if (i.Name == "operation_id")
                    {
                        i.Value = tbxOperationID.Text;
                    }
                    else if (i.Name == "glide_no")
                    {
                        i.Value = tbxGlideNo.Text;
                    }
                    
                }
                //Save the xml file with new values from the form.  Simply overwrites existing values, even if they are the same.
                doc.Save(filePath);
            }
            catch (Exception e_save_xml)
            {
                Debug.WriteLine(e_save_xml.Message);
            }

            if (File.Exists(@filePath))
            {
                this.Close();
                MessageBox.Show("Configuration file successfully updated.", "Updated operation_config.xml",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error updating the file. Please check folder permissions and try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

    }
}
