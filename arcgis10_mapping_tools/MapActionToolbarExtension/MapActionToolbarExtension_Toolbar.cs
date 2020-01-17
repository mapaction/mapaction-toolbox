using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace MapActionToolbarExtension
{
    /// <summary>
    /// Summary description for MapActionToolbarExtension_Toolbar.
    /// </summary>
    [Guid("cce9ef42-818e-4a90-8d81-e36e0d970a2a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapActionToolbarExtension.MapActionToolbarExtension_Toolbar")]
    public sealed class MapActionToolbarExtension_Toolbar : BaseToolbar
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

        public MapActionToolbarExtension_Toolbar()
        {
            //
            // TODO: Define your toolbar here by adding items
            //
            //AddItem("esriArcMapUI.ZoomInTool");
            //BeginGroup(); //Separator
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            //AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
            AddItem("MapActionToolbarExtension.EventTool_Wrapper");
            BeginGroup();
            AddItem("MapActionToolbarExtension.LayoutTool_Wrapper");
            BeginGroup();
            AddItem("MapActionToolbarExtension.ExportTool_Wrapper");
            BeginGroup();
            AddItem("MapActionToolbarExtension.GenerationTool_Wrapper");
            BeginGroup();
            AddItem("MapActionToolbarExtension.RenameTool_Wrapper");
            BeginGroup();
            AddItem("MapActionToolbarExtension.AboutBox_Wrapper");
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "MapAction Toolbar (ext)";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "MapActionToolbarExtension_Toolbar";
            }
        }
    }
}