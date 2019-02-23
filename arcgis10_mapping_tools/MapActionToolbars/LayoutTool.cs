using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;

namespace MapActionToolbars
{
    public class LayoutTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public LayoutTool()
        {
        }

        protected override void OnClick()
        {
            //Check if 'Main map' frame exists.  If not show a message box telling the user so. Don't open GUI.
            //Check to see if element name duplicates exist
            //Check to see if the operational config file exists
             //Check to see if the config file exists, if not abort and send the user a message
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = MapAction.Utilities.getOperationConfigFilePath();
            string duplicateString = "";
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            if (!MapAction.PageLayoutProperties.detectMapFrame(pMxDoc, "Main map"))
            {
                MessageBox.Show("This tool only works with the MapAction mapping templates.  The 'Main map' map frame could not be detected. Please load a MapAction template and try again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (MapAction.PageLayoutProperties.checkLayoutTextElementsForDuplicates(pMxDoc, "Main map", out duplicateString))
            {
                MessageBox.Show("Duplicate named elements have been identified in the layout. Please remove duplicate element names \"" + duplicateString + "\" before trying again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!File.Exists(@filePath))
            {
                MessageBox.Show("The operation configuration file is required for this tool.  It cannot be located.",
                    "Configuration file required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MapAction.PageLayoutProperties.detectMapFrame(pMxDoc, "Main map"))
            {
                frmLayoutMain form = new frmLayoutMain();
                form.ShowDialog();
            }
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }        
    }
}
