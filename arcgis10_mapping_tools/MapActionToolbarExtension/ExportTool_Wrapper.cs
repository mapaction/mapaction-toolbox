using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace MapActionToolbarExtension
{
    /// <summary>
    /// A COM-visible ArcObjects BaseCommand (button) for ArcMap, calling the existing Export Tool form on click.
    /// </summary>
    [Guid("d498ed1a-3e7c-49eb-bd5d-aa529ce0fd5c")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapActionToolbarExtension.ExportTool_Wrapper")]
    public sealed class ExportTool_Wrapper : BaseCommand
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
        public ExportTool_Wrapper()
        {
            // TODO: remove (AO) from the strings, this is here to highlight difference between addin and installed version during testing
            base.m_category = "MapAction Mapping Tools (AO)"; //localizable text
            base.m_caption = "Export Tool (AO)";  //localizable text
            base.m_message = "Exports the map layout to an image (pdf, jpeg, emf) and creates the map metadata xml ready to be uploaded to the MapAction website (AO)";  //localizable text 
            base.m_toolTip = "Export Layout(AO)";  //localizable text 
            base.m_name = "MapactionMappingTools_ExportTool";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")


            try
            {
                //
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
            //Check if 'Main map' frame exists.  If not show a message box telling the user so. Don't open GUI.
            //if (!PageLayoutProperties.detectMainMapFrame())
            string duplicates = "";
            IMxDocument pMxDoc = m_application.Document as IMxDocument;
            if (!MapAction.PageLayoutProperties.detectMapFrame(pMxDoc, "Main map"))
            {
                MessageBox.Show("This tool only works with the MapAction mapping templates.  The 'Main map' map frame could not be detected. Please load a MapAction template and try again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!MapAction.Utilities.detectEventConfig())
            {
                MessageBox.Show("The event configuration file is required for this tool.  It cannot be located.",
                    "Configuration file required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MapAction.PageLayoutProperties.checkLayoutTextElementsForDuplicates(pMxDoc, "Main map", out duplicates))
            {
                MessageBox.Show("Duplicate named elements have been identified in the layout. Please remove duplicate element names \"" + duplicates + "\" before trying again.", "Invalid map template",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (MapAction.PageLayoutProperties.detectMapFrame(pMxDoc, "Main map"))
            {
                var dlg = new MapActionToolbars.frmExportMain();

                if (dlg.Text.Length > 0)
                {
                    dlg.ShowDialog();
                }
            }
        }
        #endregion
    }
}
