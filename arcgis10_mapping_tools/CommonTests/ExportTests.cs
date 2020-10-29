using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using NUnit.Framework;
using MapAction;


namespace MapAction.tests
{
    //[Ignore("Ignore Exports whilst fixing PageLayoutElements")]
    [TestFixture]
    public class Export
    {
        // Class properties 
        protected string exportPath;
        protected string testRootDir;
        protected IMapDocument pMapDoc; // Map document 

        // Default constructor, which is called just once.
        public Export()
        {
            TestUtilities.BindESRILicense();
            this.testRootDir = TestUtilities.GetTestsRootDir();
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
            string documentName;

            this.exportPath = TestUtilities.GetTemporaryDirectory();
            // hardcoded path to a test MXD relative to the VS Project root directory
            // ConfigurationManager.AppSettings["mapDocument"];
            documentName = Path.Combine(this.testRootDir, @"testfiles\MA_A3_landscape.mxd");
            // Open map document
            this.pMapDoc = TestUtilities.GetMXD(documentName); 
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
       
        [TestCase(MapActionExportTypes.png_thumbnail_zip, null, 2)]
        [TestCase(MapActionExportTypes.jpeg, null, 50)]
        public void exportSizedImageNewCreatesFileTest(MapActionExportTypes fileType, string dataFrameName, int expectedFileSize)
        {
            string stubPath = Path.Combine(this.exportPath, "testmap");
            string exportFileName;
            ushort width;
            if (fileType == MapActionExportTypes.png_thumbnail_zip)
            {
                string outDir = this.exportPath;// just for clarity...

                exportFileName = Path.Combine(outDir, "thumbnail.png");
                width = MapAction.Properties.Settings.Default.thumbnail_width_px;
            }
            else
            {
                // at present the sized export still uses dpi to build the filename, using screen resolution
                int dpi = MapAction.MapImageExporter.SCREEN_RES_DPI;

                width = 1024;
                if (dataFrameName == null)
                {
                    exportFileName = String.Format("{0}-{1}dpi.{2}", stubPath, dpi, fileType.ToString());
                }
                else
                {
                    exportFileName = String.Format("{0}-mapframe-{1}dpi.{2}", stubPath, dpi, fileType.ToString());
                    //pathFileName = @pathDocumentName + "-mapframe-" + dpi.ToString() + "dpi." + exportType;
                }
            }
            Console.WriteLine("Export Filename:\t{0}", exportFileName);
            FileInfo fi = new FileInfo(exportFileName);

            // Test export file not present already 
            // Assert.IsFalse(System.IO.File.Exists(exportFileName), "A map file did not exist prior to the export function being called.");
            Assert.IsFalse(fi.Exists, "A map file did not exist prior to the export function being called as expected.");

            // Do the export
            MapImageExporter exporter = new MapImageExporter(pMapDoc, stubPath, dataFrameName);
            XYDimensions constrainTo = new XYDimensions(){
                Width = width,
                Height = null
            };
            string resultPath = exporter.exportImage(fileType, constrainTo);
                
            // Assert it made the file we expected it to
            Assert.IsTrue(resultPath != null, "The export function returned a value");
            Assert.IsTrue(resultPath == exportFileName, "The export function created the filename we were expecting");

            // Assert file exported. 
            fi.Refresh();
            Assert.IsTrue(fi.Exists, "The map file has been exported as expected.");
            // this test is a little difficult with a thumbnail which could be v small if the map is plain
            Assert.IsTrue(fi.Length > (expectedFileSize * 1024), "The map file size is vaguely sensible.");
        }


        [TestCase(MapActionExportTypes.pdf_non_ddp , null, 300)]
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
            string expectedExportFileName;
            if (dataFrameName == null)
            {
                expectedExportFileName = String.Format("{0}-{1}dpi.{2}", stubPath, dpi, fileType.ToString());
            }
            else
            {
                expectedExportFileName = String.Format("{0}-mapframe-{1}dpi.{2}", stubPath, dpi, fileType.ToString());
                //pathFileName = @pathDocumentName + "-mapframe-" + dpi.ToString() + "dpi." + exportType;
            }

            Console.WriteLine("Expected export Filename:\t{0}", expectedExportFileName);
            FileInfo fi = new FileInfo(expectedExportFileName);

            // Test export file not present already 
            // Assert.IsFalse(System.IO.File.Exists(exportFileName), "A map file did not exist prior to the export function being called.");
            Assert.IsFalse(fi.Exists, "A map file did not exist prior to the export function being called as expected.");
            
            // Do the export
            MapImageExporter exporter = new MapImageExporter(pMapDoc, stubPath, dataFrameName);
            string resultPath = exporter.exportImage(fileType, dpi);
            // changed to use non-static exporter class
            //MapExport.exportImage(this.pMapDoc, fileType, dpi, stubPath, dataFrameName);

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

            try
            {
                MapExport.exportMapFrameKmlAsRaster(this.pMapDoc, dataFrameName, kmlFileName, scale, "50");
            }
            catch (System.Runtime.InteropServices.COMException ce)
            {
                System.Console.WriteLine("COMException message:");
                System.Console.WriteLine(ce.Message);
                System.Console.WriteLine(ce.ErrorCode);
                System.Console.WriteLine(ce.Data);
                System.Console.WriteLine(ce.TargetSite);
            }

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


    }
}
