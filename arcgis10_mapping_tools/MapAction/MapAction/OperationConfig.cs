using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace MapAction
{
    [XmlRoot("OperationConfig")]
    public class OperationConfig
    {
        [XmlElement("OperationName")]
        public string OperationName { get; set; }

        [XmlElement("GlideNo")]
        public string GlideNo { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }

        [XmlElement("TimeZone")]
        public string TimeZone { get; set; }

        [XmlElement("language-iso2")]
        public string LanguageIso2 { get; set; }

        [XmlElement("OperationId")]
        public string OperationId { get; set; }

        [XmlElement("DefaultSourceOrganisation")]
        public string DefaultSourceOrganisation { get; set; }

        [XmlElement("DefaultSourceOrganisationUrl")]
        public string DefaultSourceOrganisationUrl { get; set; }

        [XmlElement("DeploymentPrimaryEmail")]
        public string DeploymentPrimaryEmail { get; set; }

        [XmlElement("DefaultDisclaimerText")]
        public string DefaultDisclaimerText { get; set; }

        [XmlElement("DefaultDonorsText")]
        public string DefaultDonorsText { get; set; }

        [XmlElement("DefaultJpegResDPI")]
        public string DefaultJpegResDPI { get; set; }

        [XmlElement("DefaultPdfResDPI")]
        public string DefaultPdfResDPI { get; set; }

        [XmlElement("DefaultEmfResDPI")]
        public string DefaultEmfResDPI { get; set; }

        [XmlElement("DefaultPathToExportDir")]
        public string DefaultPathToExportDir { get; set; }

        [XmlElement("Language")]
        public string Language { get; set; }
    }
}
