using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;


namespace Prototype1_ExportTool
{
    public class ExportTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ExportTool()
        {
        }

        protected override void OnClick()
        {
            //Check if 'Main map' frame exists.  If not show a message box telling the user so. Don't open GUI.
            //if (!PageLayoutProperties.detectMainMapFrame())
            IMxDocument MxDoc = ArcMap.Application.Document as IMxDocument;
            if (!MapAction.PageLayoutProperties.detectMapFrame(MxDoc, "Main map"))
            {
                MessageBox.Show("This tool only works with the MapAction mapping templates.  The 'Main map' map frame could not be detected. Please load a MapAction template and try again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (MapAction.PageLayoutProperties.detectMapFrame(MxDoc, "Main map"))
            {
                var dlg = new frmMain();
                dlg.ShowDialog();
            }    

        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
