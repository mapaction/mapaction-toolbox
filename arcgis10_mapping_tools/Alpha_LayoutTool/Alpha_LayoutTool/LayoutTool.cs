using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;

namespace Alpha_LayoutTool
{
    public class LayoutTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public LayoutTool()
        {
        }

        protected override void OnClick()
        {
            //Check if 'Main map' frame exists.  If not show a message box telling the user so. Don't open GUI.
            //if (!LayoutElements.detectMainMapFrame())
            IMxDocument MxDoc = ArcMap.Application.Document as IMxDocument;
            if (!MapAction.PageLayoutProperties.detectMapFrame(MxDoc, "Main map"))
            {
                MessageBox.Show("This tool only works with the MapAction mapping templates.  The 'Main map' map frame could not be detected. Please load a MapAction template and try again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (MapAction.PageLayoutProperties.detectMapFrame(MxDoc, "Main map"))
            {
                frmMain form = new frmMain();
                form.ShowDialog();
            }
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
        
    }

}
