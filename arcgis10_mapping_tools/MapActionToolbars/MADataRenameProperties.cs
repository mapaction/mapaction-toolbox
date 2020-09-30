using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapActionToolbar_Forms
{
    public class MADataRenameProperties
    {
        public bool initialised;
        public string ExtentPath { get; set; }
        public string CategoryPath { get; set; }
        public string ThemePath { get; set; }
        public string TypePath { get; set; }
        public string ScalePath { get; set; }
        public string SourcePath { get; set; }
        public string PermissionPath { get; set; }
        public string DNCmetadataPath { get; set; }
        public readonly string RenameLayerVersion = "v 1.3";
        public readonly string RenameLayerDate = "09 Nov 2018";

        public MADataRenameProperties()
        {
            initialised = false;

            if (ConstructLayerName.pathToLookupCSV() != "XXX")
            {
                ExtentPath = ConstructLayerName.pathToLookupCSV() + @"\01_geoextent.csv";
                CategoryPath = ConstructLayerName.pathToLookupCSV() + @"\02_category.csv";
                ThemePath = ConstructLayerName.pathToLookupCSV() + @"\03_theme.csv";
                TypePath = ConstructLayerName.pathToLookupCSV() + @"\04_geometry.csv";
                ScalePath = ConstructLayerName.pathToLookupCSV() + @"\05_scale.csv";
                SourcePath = ConstructLayerName.pathToLookupCSV() + @"\06_source.csv";
                PermissionPath = ConstructLayerName.pathToLookupCSV() + @"\07_permission.csv";
                DNCmetadataPath = ConstructLayerName.pathToLookupCSV() + @"\99_DNCmetadata.csv";
                initialised = true;
            }
        }
    }
}
