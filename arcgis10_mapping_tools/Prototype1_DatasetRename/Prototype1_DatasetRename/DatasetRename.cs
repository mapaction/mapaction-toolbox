using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Prototype1_DatasetRename
{
    public class DatasetRename : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public DatasetRename()
        {
        }

        protected override void OnClick()
        {
            frmMain dlg = new frmMain();
            dlg.Show();
            dlg.TopMost = true;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
