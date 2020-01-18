using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace MapActionToolbar_COMTools
{
    /// <summary>
    /// /// A COM-visible ArcObjects BaseCommand (button) for ArcMap, calling the existing Event (op-config) Tool form on click.
    /// </summary>
    [Guid("6fb20e0f-b837-4034-b633-97aab31216e0")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapActionToolbar_COMTools.EventTool_COM")]
    public sealed class EventTool_COM : BaseCommand
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
        public EventTool_COM()
        {
            // TODO: Remove (AO) from strings, currently here to distinguish this from addin-generated button
            base.m_category = "MapAction Mapping Tools (AO)"; //localizable text
            base.m_caption = "Event Tool (AO)";  //localizable text
            base.m_message = "Create or edit the event configuration file which is used by the MapAction layour and export tools (AO)";  //localizable text 
            base.m_toolTip = "Update Event Configuration (AO)";  //localizable text 
            base.m_name = "MapactionMappingTools_EventTool";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                // TODO: change bitmap name 
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
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            MapActionToolbars.frmEvent form = new MapActionToolbars.frmEvent();
            if (form.Text.Length > 0)
            {
                form.ShowDialog();
            }
        }

        #endregion
    }
}
