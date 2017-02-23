using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using NUnit.Framework;
using MapAction;


namespace MapAction.tests
{
    /// <summary>
    /// The tests in this case are focused on code dealing exporting MapAction specific MXDs which have DataDrivenPages enabled.
    /// </summary>
    [TestFixture]
    public class DDPExportTests
    {
        protected string exportPath;
        protected string testRootDir;
        protected IMapDocument pMapDoc; // Map document 


        // default constructor
        public DDPExportTests()
        {
            TestUtilities.BindESRILicense();
            this.testRootDir = TestUtilities.GetTestsRootDir();
        }

 
        // This is called prior to each test and allows state to be reset.
        // This keeps each test isolated and idenpendant from the others.
        [SetUp]
        public void Setup()
        {
            this.pMapDoc = null;
            this.exportPath = TestUtilities.GetTemporaryDirectory();
        }
        

        [TearDown]
        public void TearDown()
        {
            //shutdown ArcMap
            MapDocument mapDoc = (MapDocument)this.pMapDoc;
            mapDoc.Close();
        }


        [TestCase(@"testfiles\ddp_enabled_three_pages_layout_view.mxd", true)]
        [TestCase(@"testfiles\ddp_enabled_three_pages_map_view.mxd", true)]
        [TestCase(@"testfiles\ddp_enabled_single_page.mxd", true)]
        [TestCase(@"testfiles\MA_A3_landscape.mxd", false)]
        public void TestIsDataDrivenPagesEnabled(string relativeMXDfilename, bool isDDPEnabled)
        {
            string documentName;
            documentName = Path.Combine(this.testRootDir, relativeMXDfilename);
            // Open map document
            this.pMapDoc = TestUtilities.GetMXD(documentName);

            bool result = PageLayoutProperties.isDataDrivenPagesEnabled(pMapDoc);

            Assert.AreEqual(isDDPEnabled, result, "Correctly detected if DataDrivenPages is enabled in example MXD");
        }

        [TestCase(@"testfiles\ddp_enabled_three_pages_layout_view.mxd", 3)]
        [TestCase(@"testfiles\ddp_enabled_single_page.mxd", 1)]
        [TestCase(@"testfiles\MA_A3_landscape.mxd", 1)]
        public void TestCountPdfOutputFiles(string relativeMXDfilename, int pdfCount)
        {
            // Open map document
            string documentName;
            documentName = Path.Combine(this.testRootDir, relativeMXDfilename);
            this.pMapDoc = TestUtilities.GetMXD(documentName);
            
            String baseExportFileName = Path.Combine(this.exportPath, "test-ddp");

            // Query export directory
            DirectoryInfo di = new DirectoryInfo(this.exportPath);
            String fileExtention = "pdf";
            String searchPattern = String.Format("*.{0}", fileExtention);

            int preExportFileCnt = di.GetFiles(searchPattern).Length;

            // do export
            try
            {
                MapImageExporter mie = new MapImageExporter(this.pMapDoc, baseExportFileName, "Main map");
                mie.exportDataDrivenPagesImages();
            }catch (System.Runtime.InteropServices.COMException ce){
                System.Console.WriteLine("COMException message:");
                System.Console.WriteLine(ce.Message);
                System.Console.WriteLine(ce.ErrorCode);
                System.Console.WriteLine(ce.Data);
                System.Console.WriteLine(ce.TargetSite);
            }

            // Check result
            int postExportFileCnt = di.GetFiles(searchPattern).Length;
            int result = postExportFileCnt - preExportFileCnt;

            Assert.AreEqual(pdfCount, result, "Expected number of files produced by DataDrivenPages Export");
         }
    }
}
