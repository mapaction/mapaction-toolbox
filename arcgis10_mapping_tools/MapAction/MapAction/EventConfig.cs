using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapActionToolbar_Core
{
    public class EventConfig
    {
        public EventConfig()
        {
            Donors = new List<string>();
        }

        [JsonProperty("cmf_descriptor_path")]
        public string CrashMoveFolderDescriptorPath { get; set; }

        [JsonProperty("operation_name")]
        public string OperationName { get; set; }

        [JsonProperty("glide_number")]
        public string GlideNumber { get; set; }

        [JsonProperty("affected_country_iso3")]
        public string AffectedCountryIso3 { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("language_iso2")]
        public string LanguageIso2 { get; set; }

        [JsonProperty("operation_id")]
        public string OperationId { get; set; }

        [JsonProperty("default_source_organisation")]
        public string DefaultSourceOrganisation { get; set; }

        [JsonProperty("default_source_organisation_url")]
        public string DefaultSourceOrganisationUrl { get; set; }

        [JsonProperty("default_publishing_base_url")]
        public string DefaultPublishingBaseUrl { get; set; }

        [JsonProperty("deployment_primary_email")]
        public string DeploymentPrimaryEmail { get; set; }

        [JsonProperty("default_disclaimer_text")]
        public string DefaultDisclaimerText { get; set; }

        [JsonProperty("default_donor_credits")]
        public string DefaultDonorCredits { get; set; }

        [JsonProperty("default_jpeg_res_dpi")]
        public string DefaultJpegResDPI { get; set; }
        
        [JsonProperty("default_pdf_res_dpi")]
        public string DefaultPdfResDPI { get; set; }

        [JsonProperty("default_emf_res_dpi")]
        public string DefaultEmfResDPI { get; set; }

        [JsonProperty("donors")]
        public List<string> Donors { get; set; }
    }
}
