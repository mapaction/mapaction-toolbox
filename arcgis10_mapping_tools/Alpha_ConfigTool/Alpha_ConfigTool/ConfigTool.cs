using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Alpha_ConfigTool
{
    public class ConfigTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ConfigTool()
        {
        }

        protected override void OnClick()
        {
            frmMain form = new frmMain();
            form.ShowDialog();

        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
