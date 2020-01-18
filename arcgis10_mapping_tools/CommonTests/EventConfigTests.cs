using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MapActionToolbar_Core;

namespace MapActionToolbar_Core.tests
{
    [TestFixture]
    public class EventConfigTests
    {
        // Class properties 
        protected string testRootDir;
        protected MapActionToolbar_Core.EventConfig config;

        private readonly string expectedCrashMoveFolderDescriptorPath = "C:/Temp";
        private readonly string expectedOperationName= "Op_test";
        private readonly string expectedGlideNo= "XY-2006-123456-PAK";
        private readonly string expectedCountry= "AND";
        private readonly string expectedTimeZone= "UTC-12:00";
        private readonly string expectedLanguageIso2= "ar";
        private readonly string expectedOperationId= "tttths";
        private readonly string expectedDefaultSourceOrganisation= "MapAction";
        private readonly string expectedDefaultSourceOrganisationUrl= "www.mapaction.org";
        private readonly string expectedDeploymentPrimaryEmail= "info@mapaction.org";
        private readonly string expectedDefaultDisclaimerText= "The depiction and use of boundaries, names and associated data shown here do not imply endorsement or acceptance by MapAction.";
        private readonly string expectedDefaultDonorCredits = "Supported by DFID";
        private readonly string expectedDefaultJpegResDPI= "300";
        private readonly string expectedDefaultPdfResDPI= "300";
        private readonly string expectedDefaultEmfResDPI= "300";
        //private readonly string expectedDefaultPathToExportDir= "../Data20170414";

        // Default constructor, which is called just once.
        public EventConfigTests()
        {
            this.testRootDir = TestUtilities.GetTestsRootDir();
        }

        [SetUp]
        public void Setup()
        {
            string filePath = Path.Combine(this.testRootDir, @"testfiles\event_description.json");
            config = MapActionToolbar_Core.Utilities.getEventConfigValues(filePath);
        }

        [TestCase]
        public void TestOperationConfig()
        {
            CheckEventConfigWithKnownContents(config);
        }

        [TestCase]
        public void TestEventConfigWrite()
        {
            string testFileName = "test_event_config";
            string path = Path.Combine(this.testRootDir, @"testfiles");

            // Remove file if it exists
            if (File.Exists(Path.Combine(path, testFileName + ".xml")))
            {
                File.Delete(Path.Combine(path, testFileName + ".xml"));
            }
            /*
            string newFilePath = MapAction.Utilities.createJson(config, path, testFileName);
            EventConfig newConfig = MapAction.Utilities.getEventConfigValues(newFilePath);
            CheckEventConfigWithKnownContents(newConfig);

            // Tidy, -remove file if it exists
            if (File.Exists(Path.Combine(path, testFileName + ".xml")))
            {
                File.Delete(Path.Combine(path, testFileName + ".xml"));
            }
            */
        }

        public void CheckEventConfigWithKnownContents(EventConfig eventConfig)
        {
            Assert.AreEqual(expectedDefaultDisclaimerText, eventConfig.DefaultDisclaimerText);
            Assert.AreEqual(expectedOperationName, eventConfig.OperationName);
            Assert.AreEqual(expectedGlideNo, eventConfig.GlideNumber);
            Assert.AreEqual(expectedCountry, eventConfig.AffectedCountryIso3);
            Assert.AreEqual(expectedTimeZone, eventConfig.TimeZone);
            Assert.AreEqual(expectedLanguageIso2, eventConfig.LanguageIso2);
            Assert.AreEqual(expectedOperationId, eventConfig.OperationId.ToLower());
            Assert.AreEqual(expectedDefaultSourceOrganisation, eventConfig.DefaultSourceOrganisation);
            Assert.AreEqual(expectedDefaultSourceOrganisationUrl, eventConfig.DefaultSourceOrganisationUrl);
            Assert.AreEqual(expectedDeploymentPrimaryEmail, eventConfig.DeploymentPrimaryEmail);
            Assert.AreEqual(expectedDefaultDisclaimerText, eventConfig.DefaultDisclaimerText);
            Assert.AreEqual(expectedDefaultDonorCredits, eventConfig.DefaultDonorCredits);
            Assert.AreEqual(expectedDefaultJpegResDPI, eventConfig.DefaultJpegResDPI);
            Assert.AreEqual(expectedDefaultPdfResDPI, eventConfig.DefaultPdfResDPI);
            Assert.AreEqual(expectedDefaultEmfResDPI, eventConfig.DefaultEmfResDPI);
            //Assert.AreEqual(expectedDefaultPathToExportDir, eventConfig.DefaultPathToExportDir);
        }
    }
}
