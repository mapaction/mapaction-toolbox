using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using MapAction;
using NUnit.Framework;

namespace MapAction.tests
{
    /// <summary>
    /// The tests in this case are focused on the testing that code dealing with the MapAction specific MXDs work correctly
    /// 
    /// At present the PageLayoutProperties class is entirely procedural and only uses static methods. The layout of these tests 
    /// is intened to be adaptable to some furture refactoring of this class, as a non-statci object.
    /// </summary>
    [TestFixture]
    public class MATemplateTests
    {
        protected string exportPath;
        protected string testRootDir;
        protected IMapDocument pMapDoc; // Map document 
        protected IMxDocument pMxDoc; // Map document 


        // default constructor
        public MATemplateTests()
        {
            TestUtilities.BindESRILicense();
            this.testRootDir = TestUtilities.GetTestsRootDir();

            // Openning a IMxDoc here just so that it doesn't get called every time in Setup()
            string documentName;
            documentName = Path.Combine(this.testRootDir, @"testfiles\MA_A3_landscape.mxd");
            MxDocument mxd = new MxDocument();
            mxd.Parent.OpenDocument(documentName);
            this.pMxDoc = (IMxDocument)mxd;
        }

        ~MATemplateTests()
        {
            if (this.pMxDoc != null)
            {
                MxDocument mxDoc = (MxDocument)this.pMxDoc;
                mxDoc.Parent.Shutdown();
            }
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
            if (this.pMapDoc != null)
            {
                MapDocument mapDoc = (MapDocument)this.pMapDoc;
                mapDoc.Close();
            }
        }


        //[Ignore("Ignore TestMapElementNames whilst fixing PageLayoutElements")]
        [TestCase]
        public void TestMapElementNames()
        {
            string[] referenceNames = {"title", "summary", "data_sources", "map_no", "mxd_name", "spatial_reference",
                                       "scale", "glide_no", "disclaimer", "donor_credit", "map_producer", "timezone"};

            MapElementNames[] allElements = (MapElementNames[])Enum.GetValues(typeof(MapElementNames));

            // TODO: Surely there is a tidier way to do this?
            if (referenceNames.Length == allElements.Length)
            {
                List<string> allNamesList = new List<string>();
                foreach (MapElementNames en in allElements)
                {
                    allNamesList.Add(en.ToString());
                }
                string[] allNames = allNamesList.ToArray();
                Array.Sort(referenceNames);
                Array.Sort(allNames);
                Assert.IsTrue(Enumerable.SequenceEqual(referenceNames, allNames));
            }
        }

        /// <summary>
        /// This tests whether or not a suitable exception is raised
        /// if the named map frame does not exist in the MXD
        /// </summary>
        /// <param name="relativeMXDfilename"></param>
        /// <param name="dataFrameName"></param>
        /// <param name="passExpected"></param>
        [TestCase(@"testfiles\MA_A3_landscape.mxd", "Main map", false)]
        [TestCase(@"testfiles\MA_A3_landscape.mxd", "non-existing-map-frame", true)]
        public void TestMissingMapFrameException(string relativeMXDfilename, string dataFrameName, bool exceptionExpected)
        {
            bool exceptionRaised = false;

            try
            {
                Dictionary<string, string> dict = PageLayoutProperties.getLayoutTextElements(this.pMxDoc, dataFrameName);
            }
            catch (MapActionMapTemplateException mmte)
            {
                exceptionRaised = mmte.isMapFrameMissing;
            }
            Assert.AreEqual(exceptionExpected, exceptionRaised,
                String.Format("MapFrame '{0}' -  expected:{1}, found:{2}", dataFrameName, exceptionExpected, exceptionRaised));
        }


        // This tests whether or not the correct information about missing and dulicate
        // map elements is included if/when an exception in raised about an MXD which 
        // doesn't not addear to MapAction's map template standard.

        /// <summary>
        /// This tests whether or not the correct information about missing and dulicate 
        /// map elements is included if/when an exception in raised about an MXD which 
        /// doesn't not addear to MapAction's map template standard.
        /// </summary>
        /// <param name="relativeMXDfilename"></param>
        /// <param name="dataFrameName"></param>
        /// <param name="missingNum"></param>
        /// <param name="duplicateNum"></param>
        [Ignore("Ignore whilst test is imcomplete")]
        [TestCase(@"testfiles\MA_A3_landscape.mxd", "Main map", 0, 0)]
        [TestCase(@"testfiles\ARCGIS_10_2_MA000_Landscape_Bottom", "Main map", 0, 0)]
        public void TestGetLayoutTextElements(string relativeMXDfilename, string dataFrameName, int missingNum, int duplicateNum)
        {
            Dictionary<string, string> dict = PageLayoutProperties.getLayoutTextElements(this.pMxDoc, dataFrameName);

            if (dict != null)
            {
                //foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                foreach (MapElementNames elementName in Enum.GetValues(typeof(MapElementNames)))
                {
                    if (!dict.ContainsKey(elementName.ToString()))
                    {
                        Assert.Fail("Could not find map elements {0}", elementName.ToString());
                    }
                }
                Assert.Pass("All map elements found");
            }
            else
            {
                Assert.Fail("Null object returned for map elements array");
            }
        }


        /// <summary>
        /// This tests handling of ESRI Label markup within strings. 
        /// 
        /// Each of the test cases have been manually tested by entering them into a text
        /// element in ArcMap.
        /// </summary>
        /// <param name="input">Input string which may contain markup</param>
        /// <param name="expectedOutput">, Null value indicates that the expected output is identical to the input</param>
        // no markup
        [TestCase(@"1 - The quick brown fox", null)]
        // single bold tag
        [TestCase(@"2 - The <BOL>quick</BOL> brown fox", @"2 - The quick brown fox")]
        // double bold and italic tags
        [TestCase(@"3 - The <BOL><ITA>quick</ITA> brown</BOL> fox", @"3 - The quick brown fox")]
        // incorrectly ordered double bold and italic tags
        [TestCase(@"4 - The <BOL><ITA>quick</BOL> brown</ITA> fox", null)]
        // ampersand
        [TestCase(@"5 - The fox & hounds", null)]
        // ampersand and valid mark up
        [TestCase(@"6 - The <BOL>fox</BOL> & <ITA>hounds</ITA>", null)]
        // less than sign with valid mark up
        [TestCase(@"7 - The <BOL>fox</BOL> < <ITA>hounds</ITA>", null)]
        // greater than sign with valid mark up
        [TestCase(@"8 - The <BOL>fox</BOL> > <ITA>hounds</ITA>", @"8 - The fox > hounds")]
        // HTML encoded ampersand
        [TestCase(@"9 - The fox &amp; hounds", null)]
        // HTML encoded ampersand with valid mark up
        [TestCase(@"10 - The <BOL>fox</BOL> &amp; <ITA>hounds</ITA>", @"10 - The fox & hounds")]
        // Dynamic Page markup, when DDP is disabled
        //[TestCase(@"11 - The<dyn type='page' property='Name'/> quick brown fox", @"11 - The quick brown fox")]
        // Dynamic Page markup, when DDP is disabled
        //[TestCase(@"12 - The <dyn type='page' property='Name' emptyStr='quick'/> brown fox", @"12 - The quick brown fox")]
        // Dynamic Page markup, when DDP is disabled and formating markup
        //[TestCase(@"13 - The <dyn type='page' property='Name' emptyStr='quick'/> brown <BOL>fox</BOL>", @"13 - The quick brown fox")]
        public void TestStripESRILabelMarkup(string input, string expectedOutput)
        {
            expectedOutput = expectedOutput ?? input;
            string actualOutput = PageLayoutProperties.stripESRILabelMarkup(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
