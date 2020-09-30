using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapActionToolbar_Core
{
    public class CrashMoveFolderConfig
    {
        public CrashMoveFolderConfig()
        {
            Categories = new List<string>();
        }
        [JsonProperty("original_data")]
        public string OriginalData { get; set; }

        [JsonProperty("affected_country_iso3")]
        public string AffectedCountryIso3 { get; set; }
        
        [JsonProperty("active_data")]
        public string ActiveData { get; set; }

        [JsonProperty("layer_rendering")]
        public string LayerRendering { get; set; }

        [JsonProperty("automation")]
        public string Automation { get; set; }

        [JsonProperty("mxd_templates")]
        public string MxdTemplates { get; set; }

        [JsonProperty("arcgis_version")]
        public string ArcGisVersion { get; set; }

        [JsonProperty("mxd_products")]
        public string MxdProducts { get; set; }

        [JsonProperty("qgis_templates")]
        public string QGISTemplates { get; set; }

        [JsonProperty("export_dir")]
        public string ExportDirectory { get; set; }

        [JsonProperty("dnc_definition")]
        public string DataNamingConventionDefinition { get; set; }

        [JsonProperty("layer_nc_definition")]
        public string LayerNamingConventionDefinition { get; set; }

        [JsonProperty("mxd_nc_definition")]
        public string MXDNamingDefinitin { get; set; }
        
        [JsonProperty("map_definitions")]
        public string MapDefinitions { get; set; }

        [JsonProperty("layer_properties")]
        public string LayerProperties { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }
    }
}
