using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Carto;

/**
 * Various static functions that are useful for setting up and tearing down tests for the MapActionToolbox Addins
 */
namespace MapAction.tests
{
    class TestUtilities
    {
        [DllImport("User32.dll")]
        public static extern int GetDesktopWindow();

        /*
         * Add runtime binding prior to any ArcObjects code in the static void Main() method.
         */   
        public static Boolean BindESRILicense()
        {
            try
            {
                if (ESRI.ArcGIS.RuntimeManager.ActiveRuntime == null)
                {
                    ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static string GetTestsRootDir()
        {
            String assemblyPath, assemblyDir, vsProjPath;
            // Get path relative to the CommonTests.dll
            assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            assemblyDir = Path.GetDirectoryName(assemblyPath);
            // Jump up two levels in directory tree to get the VS project root
            vsProjPath = Path.GetDirectoryName(Path.GetDirectoryName(assemblyDir));
            System.Console.WriteLine(String.Format("vsProjPath={0}", vsProjPath));
            return vsProjPath;
        }

        public static IMapDocument GetMXD(string mxdPath)
        {
            MapDocument _pMapDoc = new MapDocumentClass();
            _pMapDoc.Open(mxdPath);

            IPageLayout pageLayout = _pMapDoc.PageLayout;
            //Key line. Must Activate the ActiveView before it will work working as expected
            _pMapDoc.ActiveView.Activate(GetDesktopWindow()); 
            _pMapDoc.ActiveView.Refresh();

            return (IMapDocument)_pMapDoc;
        }


        /*
         * http://stackoverflow.com/questions/278439/creating-a-temporary-directory-in-windows
         */
        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

    }
}
