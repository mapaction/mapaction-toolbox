using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MapAction;

namespace MapAction.tests
{
    [TestFixture]
    public class OperationConfigTests
    {
        // Class properties 
        protected string testRootDir;
        protected MapAction.OperationConfig config;

        private readonly string expectedOperationName= "Op_test";
        private readonly string expectedGlideNo= "XY-2006-123456-PAK";
        private readonly string expectedCountry= "Andorra";
        private readonly string expectedTimeZone= "UTC-12:00";
        private readonly string expectedLanguageIso2= "ar";
        private readonly string expectedOperationId= "tttths";
        private readonly string expectedDefaultSourceOrganisation= "MapAction";
        private readonly string expectedDefaultSourceOrganisationUrl= "www.mapaction.org";
        private readonly string expectedDeploymentPrimaryEmail= "info@mapaction.org";
        private readonly string expectedDefaultDisclaimerText= "The depiction and use of boundaries, names and associated data shown here do not imply endorsement or acceptance by MapAction.";
        private readonly string expectedDefaultDonorsText= "Supported by";
        private readonly string expectedDefaultJpegResDPI= "300";
        private readonly string expectedDefaultPdfResDPI= "300";
        private readonly string expectedDefaultEmfResDPI= "300";
        private readonly string expectedDefaultPathToExportDir= "../Data20170414";

        // Default constructor, which is called just once.
        public OperationConfigTests()
        {
            this.testRootDir = TestUtilities.GetTestsRootDir();
        }

        [SetUp]
        public void Setup()
        {
            string filePath = Path.Combine(this.testRootDir, @"testfiles\operation_config.xml");
            config = MapAction.Utilities.getOperationConfigValues(filePath);
        }

        [TestCase]
        public void TestOperationConfig()
        {
            CheckOperationConfigWithKnownContents(config);
        }

        [TestCase]
        public void TestOperationConfigWrite()
        {
            string testFileName = "test_operation_config";
            string path = Path.Combine(this.testRootDir, @"testfiles");

            // Remove file if it exists
            if (File.Exists(Path.Combine(path, testFileName + ".xml")))
            {
                File.Delete(Path.Combine(path, testFileName + ".xml"));
            }
            string newFilePath = MapAction.Utilities.createXML(config, path, testFileName);
            OperationConfig newConfig = MapAction.Utilities.getOperationConfigValues(newFilePath);
            CheckOperationConfigWithKnownContents(newConfig);

            // Tidy, -remove file if it exists
            if (File.Exists(Path.Combine(path, testFileName + ".xml")))
            {
                File.Delete(Path.Combine(path, testFileName + ".xml"));
            }
        }

        public void CheckOperationConfigWithKnownContents(OperationConfig operationConfig)
        {
            Assert.AreEqual(expectedDefaultDisclaimerText, operationConfig.DefaultDisclaimerText);
            Assert.AreEqual(expectedOperationName, operationConfig.OperationName);
            Assert.AreEqual(expectedGlideNo, operationConfig.GlideNo);
            Assert.AreEqual(expectedCountry, operationConfig.Country);
            Assert.AreEqual(expectedTimeZone, operationConfig.TimeZone);
            Assert.AreEqual(expectedLanguageIso2, operationConfig.LanguageIso2);
            Assert.AreEqual(expectedOperationId, operationConfig.OperationId.ToLower());
            Assert.AreEqual(expectedDefaultSourceOrganisation, operationConfig.DefaultSourceOrganisation);
            Assert.AreEqual(expectedDefaultSourceOrganisationUrl, operationConfig.DefaultSourceOrganisationUrl);
            Assert.AreEqual(expectedDeploymentPrimaryEmail, operationConfig.DeploymentPrimaryEmail);
            Assert.AreEqual(expectedDefaultDisclaimerText, operationConfig.DefaultDisclaimerText);
            Assert.AreEqual(expectedDefaultDonorsText, operationConfig.DefaultDonorsText);
            Assert.AreEqual(expectedDefaultJpegResDPI, operationConfig.DefaultJpegResDPI);
            Assert.AreEqual(expectedDefaultPdfResDPI, operationConfig.DefaultPdfResDPI);
            Assert.AreEqual(expectedDefaultEmfResDPI, operationConfig.DefaultEmfResDPI);
            Assert.AreEqual(expectedDefaultPathToExportDir, operationConfig.DefaultPathToExportDir);
        }
    }
}
