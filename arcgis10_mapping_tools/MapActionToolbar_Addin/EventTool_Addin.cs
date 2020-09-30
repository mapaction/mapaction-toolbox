using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MapActionToolbar_Forms;

namespace MapActionToolbar_Addin
{
    public class EventTool_Addin : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public EventTool_Addin()
        {
        }

        protected override void OnClick()
        {
            frmEvent form = new frmEvent();
            if (form.Text.Length > 0)
            {
                form.ShowDialog();
            }
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
