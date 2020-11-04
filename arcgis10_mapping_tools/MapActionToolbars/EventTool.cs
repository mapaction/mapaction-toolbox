using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MapActionToolbars
{
    public class EventTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public EventTool()
        {
        }

        protected override void OnClick()
        {
            frmEvent form = new frmEvent();
            if (form.Text.Length > 0)
            {
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("MapActionToolbarsConfig.xml could not be found in the CMF (and default one could not be loaded for some reason). Cannot load dialog.",
                    "Config file missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void OnUpdate()
        {
            Enabled = MapActionToolbars.ArcMap.Application != null;
        }
    }

}
