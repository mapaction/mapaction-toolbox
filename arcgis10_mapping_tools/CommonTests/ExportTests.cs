using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using NUnit.Framework;
using MapAction;


namespace MapAction.tests
{
     [TestFixture]
    public class Export
    {
        // Class properties 
        protected string exportPath;
        protected string documentName;
        protected string testRootDir;
        protected IMapDocument pMapDoc; // Map document 

        [DllImport("User32.dll")]
        public static extern int GetDesktopWindow();

        // Default constructor, which is called just once.
        public Export()
        {
            //Add runtime binding prior to any ArcObjects code in the static void Main() method.
            if (ESRI.ArcGIS.RuntimeManager.ActiveRuntime == null)
            {
                ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            }

            // Get path relative to the CommonTests.dll
            string asmbyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            // Strip out spurious wording
            asmbyPath = asmbyPath.Replace(@"file:\", string.Empty);
            // Jump up two levels in directory tree to get the VS project root
            this.testRootDir = Path.Combine(asmbyPath, @"..\..\");
        }

        // This is called prior to each test and allows state to be reset.
        // This keeps each test isolated and idenpendant from the others.
        [SetUp]
        public void Setup()
        {
            /* TODO
             * An alternative (and possibly better) way to get these values would be to use the configuration 
             * manager:
                    this.exportPath = ConfigurationManager.AppSettings["exportPath"];
                    this.documentName = ConfigurationManager.AppSettings["mapDocument"];            
            */

            this.exportPath = GetTemporaryDirectory();
            // hardcoded path to a test MXD relative to the VS Project root directory
            // ConfigurationManager.AppSettings["mapDocument"];
            this.documentName = Path.Combine(this.testRootDir, @"testfiles\MA_A3_landscape.mxd");
            // Open map document
            this.pMapDoc = this.getMxd(this.documentName); 
        }

        [TearDown]
        public void TearDown()
        {
            //shutdown ArcMap
            MapDocument mxDoc = (MapDocument)this.pMapDoc;
            //mxDoc.Parent.Shutdown();
            mxDoc.Close();

            // delete the temporary directory and everything in it.
            // TODO Whilst we have the problem with the PDFs being exported blank in tests leave this commented out
            // Directory.Delete(this.exportPath, true);
        }

         /// <summary>
         /// Runs at end of tests to close ArcMap and free up resources. 
         /// </summary>
        [TestFixtureTearDown]
        public void closeArcMap()
        {
            //MxDocument mxDoc = (MxDocument)this.pMapDoc;
            MapDocument mxDoc = (MapDocument)this.pMapDoc;
            //mxDoc.Parent.Shutdown();
            mxDoc.Close();
            // ^ Is this the best way to do it, or does the runtime manager provide a method?
        }

        [TestCase(MapActionExportTypes.pdf, null, 300)]
        [TestCase(MapActionExportTypes.jpeg, null, 300)]
        [TestCase(MapActionExportTypes.jpeg, "Main map", 30)]
        [TestCase(MapActionExportTypes.emf, "Main map", 30)]
        public void exportImageCreatesFileTest(MapActionExportTypes fileType, string dataFrameName, int expectedFileSize)
{
            // Console.WriteLine("Settings2 :Export Path {0},Map Document {1}", this.exportPath, this.documentName);

            // string fileType = "pdf";
            UInt16 dpi = 300;

            /*
             * Exported file name is dynamically generated.
             * For individual map frames:
             *      pathFileName = @pathDocumentName + "-" mapframe + "-" + dpi.ToString() + "dpi." + exportType; 
             * For map layouts:
             *      pathFileName = @pathDocumentName + "-" + dpi.ToString() + "dpi." + exportType; 
             */
            string stubPath = Path.Combine(this.exportPath, "testmap");
            string exportFileName;
            if (dataFrameName == null)
            {
                exportFileName = String.Format("{0}-{1}dpi.{2}", stubPath, dpi, fileType.ToString());
            }
            else
            {
                exportFileName = String.Format("{0}-mapframe-{1}dpi.{2}", stubPath, dpi, fileType.ToString());
                //pathFileName = @pathDocumentName + "-mapframe-" + dpi.ToString() + "dpi." + exportType;
            }

            Console.WriteLine("Export Filename:\t{0}", exportFileName);
            FileInfo fi = new FileInfo(exportFileName);

            // Test export file not present already 
            // Assert.IsFalse(System.IO.File.Exists(exportFileName), "A map file did not exist prior to the export function being called.");
            Assert.IsFalse(fi.Exists, "A map file did not exist prior to the export function being called as expected.");
            
            // Do the export
            MapExport.exportImage(this.pMapDoc, fileType, dpi, stubPath, dataFrameName);

            // Assert file exported. 
            fi.Refresh();
            Assert.IsTrue(fi.Exists, "The map file has been exported as expected.");
            Assert.IsTrue(fi.Length > (expectedFileSize * 1024), "The map file is larger than 50kb as expected.");

            // TODO - Check file exported is valid image of the type requested. 

        }

        [TestCase("Main map", 8, "10000000")]
        [TestCase("Main map", 8, null)]
        public void exportKMLFileTest(string dataFrameName, int expectedFileSize, string scale)
        {
            string kmlFileName = Path.Combine(this.exportPath, "testfile.kmz");
            Console.WriteLine("Export KML filename:\t{0}", kmlFileName);
            FileInfo fi = new FileInfo(kmlFileName);

            // Test export file not present already 
            // Assert.IsFalse(System.IO.File.Exists(exportFileName), "A map file did not exist prior to the export function being called.");
            Assert.IsFalse(fi.Exists, "A map file did not exist prior to the export function being called as expected.");
            MapExport.exportMapFrameKmlAsRaster(this.pMapDoc, dataFrameName, kmlFileName, scale, "50");
            // Assert file exported. 
            fi.Refresh();
            Assert.IsTrue(fi.Exists, "The map file has been exported as expected.");
            Assert.IsTrue(fi.Length > (expectedFileSize * 1024), "The map file is larger than 50kb as expected.");
        }

         /*
         * [Test]
        public void failingTest()
        {
            Assert.Fail();
        }

        [Test]
        public void passingTest()
        {
            Assert.Pass();
        }
         */

        private IMapDocument getMxd(string mxdPath)
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
        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

    }
}
