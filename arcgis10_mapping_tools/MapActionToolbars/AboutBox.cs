using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace MapActionToolbars
{
    public class ma_addin_about_box : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private string[] m_known_toolnames = {"MapAction_Alpha_ConfigTool_ConfigTool",
                                                "Alpha_LayoutTool_LayoutTool",
                                                "Alpha_ExportTool_ExportTool" };

        private string m_thisaddin_desc;

        public ma_addin_about_box()
        {
            // m_thisaddin_desc = "version " + MapActionToolbars.ThisAddIn.Date.ToString();
            m_thisaddin_desc = "Version " + System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName().Version.ToString()
                                           + "\n";
        }

        protected override void OnClick()
        {
            showDialog();
        }

        protected override void OnUpdate()
        {
        }

        private void showDialog()
        {
            MessageBox.Show(m_thisaddin_desc, "About MapAction toolbox", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
