using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;


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
            AssemblyName an;
            an = Assembly.GetExecutingAssembly().GetName();

            String version_string = an.Version.ToString();
            DateTime compile_date = new DateTime(2000, 1, 1);
            compile_date = compile_date.AddDays(an.Version.Build);
            compile_date = compile_date.AddSeconds(2 * an.Version.Revision);

            m_thisaddin_desc = String.Format("Version {0}\n\n Compiled {1} {2}", version_string, compile_date.ToShortDateString(), compile_date.ToShortTimeString());
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
