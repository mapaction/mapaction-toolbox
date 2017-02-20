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
    public class MATemplateTests
    {

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
            MxDocument mxDoc = (MxDocument)this.pMxDoc;
            mxDoc.Parent.Shutdown();
        }

        // This is called prior to each test and allows state to be reset.
        // This keeps each test isolated and idenpendant from the others.
        [SetUp]
        public void Setup()
        {
            string documentName;
            documentName = Path.Combine(this.testRootDir, @"testfiles\MA_A3_landscape.mxd");
            // Open map document
            this.pMapDoc = TestUtilities.GetMXD(documentName);
        }
        

        [TearDown]
        public void TearDown()
        {
            //shutdown ArcMap
            MapDocument mapDoc = (MapDocument)this.pMapDoc;
            mapDoc.Close();
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


        [TestCase("non-existing-map-frame")]
        [TestCase("Main map")]
        public void TestGetLayoutTextElements(string dataFrameName)
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
    }
}
