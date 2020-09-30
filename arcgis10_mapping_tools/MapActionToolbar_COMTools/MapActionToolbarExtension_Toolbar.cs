using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace MapActionToolbar_COMTools
{
    /// <summary>
    /// Summary description for MapActionToolbarExtension_Toolbar.
    /// </summary>
    [Guid("cce9ef42-818e-4a90-8d81-e36e0d970a2a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapActionToolbar_COMTools.MapActionToolbar_COM")]
    public sealed class MapActionToolbar_COM : BaseToolbar
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
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public MapActionToolbar_COM()
        {
            //
            // TODO: Define your toolbar here by adding items
            //
            //AddItem("esriArcMapUI.ZoomInTool");
            //BeginGroup(); //Separator
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            //AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
            AddItem("MapActionToolbar_COMTools.EventTool_COM");
            BeginGroup();
            AddItem("MapActionToolbar_COMTools.LayoutTool_COM");
            BeginGroup();
            AddItem("MapActionToolbar_COMTools.ExportTool_COM");
            BeginGroup();
            AddItem("MapActionToolbar_COMTools.GenerationTool_COM");
            BeginGroup();
            AddItem("MapActionToolbar_COMTools.RenameTool_COM");
            BeginGroup();
            AddItem("MapActionToolbar_COMTools.AboutBox_COM");
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "MapAction Toolbar (COM)";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "MapActionToolbar_COM";
            }
        }
    }
}