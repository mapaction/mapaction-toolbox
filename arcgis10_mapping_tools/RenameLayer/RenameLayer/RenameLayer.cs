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

// v1.1 of Add-In PJR 18/08/2016 - 19/08/2016
// - now looks for csv files in C:\MapAction\200_data_name_lookup if can't find them in crash move folder
// - renamed csv files with numeric prefixes (01_geoextent.csv instead of geoextent.csv) as per May 2016 DNC spec
// - renamed type.csv to 04_geometry.csv as per May 2016 DNC spec
// - changed Type on the form to Geometry
// - increased width of all fields on form, as some descpritions are too long to fit in otherwise
// - swapped position of the Rename and Cancel buttons to be consistent with normal arrangement
//
// Still to do:
// * restrict choice of theme to be consistent with selected category
// * automatically recognise geometry type of shapefile
// * extend to other spatial dataset types (eg raster catalog, document with spatial data, 
// *    raster (georegistered or not), table, TIN, web feature service, web mapping service
// * update local DNC lookup tables if new value for a clause is entered
// * take clause names that appear on form from the csv file names - eg first one is xxxx if 01_xxxx.csv is name of csv file
// *    possible problem in that test for whether csv files present looks for existence of 01_geoextent.csv.
// *    hence change of logic necessary
// * take lookups from Excel spreadsheet rather than csv files to avoid need to process spreadsheet


namespace RenameLayer
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
                        frmMain dlg = new frmMain();
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

                System.IO.FileInfo fileInfo_check = new System.IO.FileInfo(string_ShapefileDirectory + "\\" + string_ShapefileName + ".shp");
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
