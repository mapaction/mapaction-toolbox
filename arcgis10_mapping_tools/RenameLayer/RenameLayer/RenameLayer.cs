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
                    MessageBox.Show("This tool works on the context menu of a shapefile in ArcCatalog.  Please check your installation.", "Error",
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
                        MessageBox.Show("The path to the CSV files is incorrect.  Please set the correct path in the config tool.", "Error",
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
