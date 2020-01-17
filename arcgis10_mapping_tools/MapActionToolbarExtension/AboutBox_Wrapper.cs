using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using System.Reflection;
using System.Windows.Forms;

namespace MapActionToolbarExtension
{
    /// <summary>
    /// Summary description for AboutBox_Wrapper.
    /// </summary>
    [Guid("0e75c31f-334d-4e98-a351-ba2e9d89d0b9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapActionToolbarExtension.AboutBox_Wrapper")]
    public sealed class AboutBox_Wrapper : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        private string m_thisCompilation_desc;

        public AboutBox_Wrapper()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "MapAction Mapping Tools (AO)"; //localizable text
            base.m_caption = "About MapAction Toolbar (installed version)";  //localizable text
            base.m_message = "About MapAction Toolbar (installed version)";  //localizable text 
            base.m_toolTip = "Shows the version number of the installed MapAction Tools";  //localizable text 
            base.m_name = "MapactionMappingTools_About";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".png";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
            AssemblyName an;
            an = Assembly.GetExecutingAssembly().GetName();

            String version_string = an.Version.ToString();
            DateTime compile_date = new DateTime(2000, 1, 1);
            compile_date = compile_date.AddDays(an.Version.Build);
            compile_date = compile_date.AddSeconds(2 * an.Version.Revision);

            m_thisCompilation_desc = String.Format("Version {0}\n\n Compiled {1} {2}", version_string, compile_date.ToShortDateString(), compile_date.ToShortTimeString());
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add AboutBox_Wrapper.OnClick implementation
            showDialog();
        }

        private void showDialog()
        {
            MessageBox.Show(m_thisCompilation_desc, "About MapAction toolbox", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
