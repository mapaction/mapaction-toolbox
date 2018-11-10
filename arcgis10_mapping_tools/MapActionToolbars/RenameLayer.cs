using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using System.Diagnostics;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;

//v1.2 PJR 21/10/2016
// - deals with csv files as produced directly fron DNC spreadsheet
//   - ie expects non blank header row
//   - makes use of info in 3rd column for geoextent, theme and scale
//   - accented characters now displayed correctly in drop downs on form (no need to avoid in DNC)
// - restricts choice of theme to be consistent with selected category
// - fields widened to accomodate longest descriptions in DNC (eg source ASTER) - consistent with use of Consolas font at 8.25
// - now caters for long descriptions that contain commas (instead of truncating at the first comma)
// - automatically recognises geometry type of shapefile
// - mono width font used for dropdowns (Consolas - hope it's generally available!) - makes for neater line up of 3rd column
// - version of tool now in form title
// - version of DNC used to generate cluse values abnd descriptions now at bottom of form
//
// Possibilities for future development:
// * take into account feedback from user (ideas to be firmed up before implementation) eg:
//   - put most commonly needed geoextent values at top of drop down to speed up selection of country/region for a mission
//     (at present this can be achieved by editing 01_geoextent.csv manually; perhaps could make use of country from config tool?)
//   - update local DNC lookup tables if custom value for a clause is entered (to ensure consistency over a mission)
// * extend to other spatial dataset types (eg raster catalog, document with spatial data, 
//     raster (georegistered or not), table, TIN, web feature service, web mapping service


// v1.1 of Add-In PJR 18/08/2016 - 19/08/2016
// - now looks for csv files in C:\MapAction\200_data_name_lookup if can't find them in crash move folder
// - renamed csv files with numeric prefixes (01_geoextent.csv instead of geoextent.csv) as per May 2016 DNC spec
// - renamed type.csv to 04_geometry.csv as per May 2016 DNC spec
// - changed Type on the form to Geometry
// - increased width of all fields on form, as some descriptions are too long to fit in otherwise
// - swapped position of the Rename and Cancel buttons to be consistent with normal arrangement
//



namespace MapActionToolbars
{
    public class RenameLayer : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        public RenameLayer()
        {
        }

        protected override void OnClick()
        {
            try
            {
                IGxApplication pApp = ArcMap.Application as IGxApplication;
                string pathFileName = pApp.SelectedObject.FullName;
                string root = System.IO.Path.GetDirectoryName(pathFileName);
                string filename = System.IO.Path.GetFileNameWithoutExtension(pathFileName);
                IFeatureClass fc = GetFeatureClassFromShapefileOnDisk(root, filename);
                IDataset ds = fc as IDataset;
                if (fc == null)
                {
                    MessageBox.Show("This tool works on the context menu of a shapefile in the ArcCatalog pane of an ArcMap window. " +
                    "Please check your installation.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //Check to see the csv files exist (Check is only for extent.csv)
                    if (ConstructLayerName.checkPathToLookupCSV())
                    {
                        frmRenameMain dlg = new frmRenameMain();
                        dlg.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Can't find DNC lookup CSV files." +
                        "\nIf working with crash move folder, then please set the correct path to crash move folder in the config tool." +
                        "\nOtherwise, CSV files are expected in folder \nC:\\MapAction\\200_data_name_lookup ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }    
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        public ESRI.ArcGIS.Geodatabase.IFeatureClass GetFeatureClassFromShapefileOnDisk(System.String string_ShapefileDirectory, System.String string_ShapefileName)
        {
            System.IO.DirectoryInfo directoryInfo_check = new System.IO.DirectoryInfo(string_ShapefileDirectory);
            if (directoryInfo_check.Exists)
            {
                //We have a valid directory, proceed
                System.IO.FileInfo fileInfo_check = new System.IO.FileInfo(System.IO.Path.Combine(string_ShapefileDirectory, (string_ShapefileName + ".shp")));
                if (fileInfo_check.Exists)
                {
                    //We have a valid shapefile, proceed

                    ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                    ESRI.ArcGIS.Geodatabase.IWorkspace workspace = workspaceFactory.OpenFromFile(string_ShapefileDirectory, 0);
                    ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)workspace; // Explict Cast
                    ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(string_ShapefileName);

                    return featureClass;
                }
                else
                {
                    //Not valid shapefile
                    return null;
                }
            }
            else
            {
                // Not valid directory
                return null;
            }
        }
    }
}