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
using ESRI.ArcGIS.Framework;

namespace MapActionToolbars
{
    public class RenameTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public RenameTool()
        {
        }

        protected override void OnClick()
        {
            var dlg = new frmRenameMain();
            if (dlg.initialised)
            {
                dlg.ShowDialog();
            }
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
