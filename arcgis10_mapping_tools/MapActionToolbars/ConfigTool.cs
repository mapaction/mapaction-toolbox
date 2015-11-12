using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MapActionToolbars
{
    public class ConfigTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ConfigTool()
        {
        }

        protected override void OnClick()
        {
            frmConfigMain form = new frmConfigMain();
            form.ShowDialog();

        }
        protected override void OnUpdate()
        {
            Enabled = MapActionToolbars.ArcMap.Application != null;
        }
    }

}
