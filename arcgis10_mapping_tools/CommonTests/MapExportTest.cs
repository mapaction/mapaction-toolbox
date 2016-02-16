using MapAction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using ESRI.ArcGIS.ArcMapUI;

namespace CommonTests
{
    
    
    /// <summary>
    ///This is a test class for MapExportTest and is intended
    ///to contain all MapExportTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MapExportTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        /// <summary>
        ///A test for createZip
        ///</summary>
        [TestMethod()]
        [DeploymentItem("testfiles", "testfiles")]
        public void createZipTest()
        {
            foreach (string outputPath in new Arraylist[@"testfiles\ImagesNoSpaces", @"testfiles\Images With Spaces"])
            {
                Dictionary<string, string> dictPaths = null; // TODO: Initialize to an appropriate value
                dictPaths = ExportFixtures.zipFileManifest;
                foreach (string path in dictPaths.Values)
                {
                    //path = Path.Combine
                }


                bool expected = false; // TODO: Initialize to an appropriate value
                bool actual;
                actual = MapExport.createZip(dictPaths);
                Assert.AreEqual(expected, actual);
                Assert.Inconclusive("Verify the correctness of this test method.");
            }
        }

        /// <summary>
        ///A test for exportImage
        ///</summary>
        [TestMethod()]
        public void exportImageTest()
        {
            IMxDocument pMxDoc = null; // TODO: Initialize to an appropriate value
            string exportType = string.Empty; // TODO: Initialize to an appropriate value
            string dpi = string.Empty; // TODO: Initialize to an appropriate value
            string pathDocumentName = string.Empty; // TODO: Initialize to an appropriate value
            string mapFrameName = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = MapExport.exportImage(pMxDoc, exportType, dpi, pathDocumentName, mapFrameName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for exportMapFrameKmlAsRaster
        ///</summary>
        [TestMethod()]
        public void exportMapFrameKmlAsRasterTest()
        {
            IMxDocument pMxDoc = null; // TODO: Initialize to an appropriate value
            string dataFrame = string.Empty; // TODO: Initialize to an appropriate value
            string filePathName = string.Empty; // TODO: Initialize to an appropriate value
            string scale = string.Empty; // TODO: Initialize to an appropriate value
            MapExport.exportMapFrameKmlAsRaster(pMxDoc, dataFrame, filePathName, scale);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for get7zipExePath
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MapAction.dll")]
        public void get7zipExePathTest()
        {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = MapExport_Accessor.get7zipExePath();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for openExplorerDirectory
        ///</summary>
        [TestMethod()]
        public void openExplorerDirectoryTest()
        {
            string path = string.Empty; // TODO: Initialize to an appropriate value
            MapExport.openExplorerDirectory(path);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
