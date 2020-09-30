using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Framework;
using MapActionToolbar_Forms;

namespace MapActionToolbar_Addin
{
    public class GenerationTool_Addin : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public GenerationTool_Addin()
        {
        }

        protected override void OnClick()
        {
            //Check if 'Main map' frame exists.  If not show a message box telling the user so. Don't open GUI.
            string duplicates = "";
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            if (!MapActionToolbar_Core.PageLayoutProperties.detectMapFrame(pMxDoc, "Main map"))
            {
                MessageBox.Show("This tool only works with the MapAction mapping templates.  The 'Main map' map frame could not be detected. Please load a MapAction template and try again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                MessageBox.Show("The event configuration file is required for this tool.  It cannot be located.",
                    "Configuration file required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MapActionToolbar_Core.PageLayoutProperties.checkLayoutTextElementsForDuplicates(pMxDoc, "Main map", out duplicates))
            {
                MessageBox.Show("Duplicate named elements have been identified in the layout. Please remove duplicate element names \"" + duplicates + "\" before trying again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (MapActionToolbar_Core.PageLayoutProperties.detectMapFrame(pMxDoc, "Main map"))
            {
                var dlg = new frmGenerationTool(ArcMap.Application);

                if (dlg.Text.Length > 0)
                {
                    dlg.ShowDialog();
                }
            }
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
