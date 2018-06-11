using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenameLayer
{
    public class MADataRenameProperties
    {
        public string ExtentPath { get; set; }
        public string CategoryPath { get; set; }
        public string ThemePath { get; set; }
        public string TypePath { get; set; }
        public string ScalePath { get; set; }
        public string SourcePath { get; set; }
        public string PermissionPath { get; set; }
        public string DNCmetadataPath { get; set; }
        public readonly string RenameLayerVersion = "v 1.2";
        public readonly string RenameLayerDate = "21 Oct 2016";

        public MADataRenameProperties()
        {
            ExtentPath = ConstructLayerName.pathToLookupCSV() + @"\01_geoextent.csv";
            CategoryPath = ConstructLayerName.pathToLookupCSV() + @"\02_category.csv";
            ThemePath = ConstructLayerName.pathToLookupCSV() + @"\03_theme.csv";
            TypePath = ConstructLayerName.pathToLookupCSV() + @"\04_geometry.csv";
            ScalePath = ConstructLayerName.pathToLookupCSV() + @"\05_scale.csv";
            SourcePath = ConstructLayerName.pathToLookupCSV() + @"\06_source.csv";
            PermissionPath = ConstructLayerName.pathToLookupCSV() + @"\07_permission.csv";
            DNCmetadataPath = ConstructLayerName.pathToLookupCSV() + @"\99_DNCmetadata.csv";
        }
    }
}
