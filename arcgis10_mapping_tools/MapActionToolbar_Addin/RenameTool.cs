using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MapActionToolbar_Addin
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
