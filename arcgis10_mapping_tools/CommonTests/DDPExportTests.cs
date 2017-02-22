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
    /// The tests in this case are focused on the testing that code dealing with the MapAction specific MXDs work correctly
    /// 
    /// At present the PageLayoutProperties class is entirely procedural and only uses static methods. The layout of these test is intened to be adaptable 
    /// to 
    /// </summary>
    [TestFixture]
    public class DDPExportTests
    {

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
        }
        

        [TearDown]
        public void TearDown()
        {
            //shutdown ArcMap
            MapDocument mapDoc = (MapDocument)this.pMapDoc;
            mapDoc.Close();
        }


        [TestCase(@"testfiles\ddp_enabled_single_page.mxd", true)]
        [TestCase(@"testfiles\ddp_enabled_three_pages_layout_view.mxd", true)]
        [TestCase(@"testfiles\ddp_enabled_three_pages_map_view.mxd", true)]
        [TestCase(@"testfiles\MA_A3_landscape.mxd", false)]
        public void TestIsDataDrivenPagesEnabled(string relativeMXDfilename, bool isDDPEnabled)
        {
            string documentName;
            documentName = Path.Combine(this.testRootDir, relativeMXDfilename);
            // Open map document
            this.pMapDoc = TestUtilities.GetMXD(documentName);

            bool result = PageLayoutProperties.isDataDrivenPagesEnabled(pMapDoc);

            Assert.AreEqual(result, isDDPEnabled);
        }

    }
}
