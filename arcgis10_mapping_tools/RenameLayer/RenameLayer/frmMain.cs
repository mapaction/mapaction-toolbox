﻿using System;
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
using Microsoft.VisualBasic.FileIO;

namespace RenameLayer
{
    public partial class frmMain : Form
    {
        //Create a local variable to set the path to each of the csv files that store the lookup values
        // added numeric prefixes and changed type to 04_geometry to fit in with revised DNC. PJR 18/08/2016
        string _extent_path = ConstructLayerName.pathToLookupCSV() + @"\01_geoextent.csv";
        string _category_path = ConstructLayerName.pathToLookupCSV() + @"\02_category.csv";
        string _theme_path = ConstructLayerName.pathToLookupCSV() + @"\03_theme.csv";
        string _type_path = ConstructLayerName.pathToLookupCSV() + @"\04_geometry.csv";
        string _scale_path = ConstructLayerName.pathToLookupCSV() + @"\05_scale.csv";
        string _source_path = ConstructLayerName.pathToLookupCSV() + @"\06_source.csv";
        string _permission_path = ConstructLayerName.pathToLookupCSV() + @"\07_permission.csv";
        
        // new csv to hold metadata on DNC.  PJR 21/10/2016
        // remember to change data as necessary when RenameLayer tool updates or DNC updates
        string _DNCmetadata_path = ConstructLayerName.pathToLookupCSV() + @"\99_DNCmetadata.csv";
        string _RenameLayerVersion = "v 1.2";
        string _RenameLayerDate = "21 Oct 2016";

        
        
        public frmMain()
        {
            InitializeComponent();
                                   
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            IGxApplication pApp = ArcMap.Application as IGxApplication;
            string pathFileName = pApp.SelectedObject.FullName;
            string root = System.IO.Path.GetDirectoryName(pathFileName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(pathFileName);
            IFeatureClass fc = GetFeatureClassFromShapefileOnDisk(root, filename);
            IDataset ds = fc as IDataset;
            
            //Construct layer name
            //ConstructLayerName newLayer = new ConstructLayerName();
            //string newLayerName = newLayer.LoopThroughNameElements(GetFormElementValues());
            string newLayerName = createNewLayerName();

            //Rename the layer
            ds.Rename(newLayerName);
            pApp.Refresh(root);
            this.Close();

        }

        public string createNewLayerName()
        {
            //Construct layer name
            ConstructLayerName newLayer = new ConstructLayerName();
            string newLayerName = newLayer.LoopThroughNameElements(GetFormElementValues());
            return newLayerName;
        }


        public string[] GetFormElementValues()
        {
            
            string _geoExtent;
            string _category;
            string _theme;
            string _type;
            string _scale;
            string _source;
            string _permission;
            string _freeText;

            if (!chkGeoExtent.Checked) {_geoExtent = cboGeoExtent.SelectedValue.ToString(); } else { _geoExtent = tbxGeoExtent.Text; };
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            if (!chkTheme.Checked) { _theme = cboTheme.SelectedValue.ToString(); } else { _theme = tbxTheme.Text; };
            if (!chkType.Checked) { _type = cboType.SelectedValue.ToString(); } else { _type = tbxType.Text; };
            if (!chkScale.Checked) { _scale = cboScale.SelectedValue.ToString(); } else { _scale = tbxScale.Text; };
            if (!chkSource.Checked) { _source = cboSource.SelectedValue.ToString(); } else { _source = tbxSource.Text; };
            if (!chkPermission.Checked) { _permission = cboPermission.SelectedValue.ToString(); } else { _permission = tbxPermission.Text; };
            _freeText = tbxFreeText.Text;
            
            //Create array of all the above elements
            string[] arr = { _geoExtent, _category, _theme, _type, _scale, _source, _permission, _freeText };
            
            return arr;
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

        private void frmMain_Load(object sender, EventArgs e)
        {


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnPermissionHelp_Click(object sender, EventArgs e)
        {
            frmPermissionsHelp dlg = new frmPermissionsHelp();
            dlg.ShowDialog();
        }

        private void frmMain_Load_1(object sender, EventArgs e)
        {

            //MessageBox.Show(this, "frmMain_load_1 entered", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); //PJR 18/082016

            // Add metadata/ QA info on this tool and on DNC to form  PJR 21/10/2016
            this.Text = "MapAction Dataset Rename Tool " + _RenameLayerVersion ;
            //this.Text = "MapAction Dataset Rename Tool " + _RenameLayerVersion + " of " + _RenameLayerDate;
            label12.Text = getDNCLabel(_DNCmetadata_path);
   

            //Populate combo box with value from the CSV files 
            comboValues(cboGeoExtent, _extent_path);
            comboValues(cboCategory, _category_path);
            comboValues(cboTheme, _theme_path);
            comboValues(cboType, _type_path);

            // set geometry type if known   PJR 20/10/2016
            string geomtype = getGeomType();
            if (geomtype != "other") { cboType.SelectedValue = geomtype; }

            comboValues(cboScale, _scale_path);
            comboValues(cboSource, _source_path);
            comboValues(cboPermission, _permission_path);

            // construct and display new file name if geometry type is already set
            if (geomtype != "other") { lblReviewLayerName.Text = createNewLayerName(); }

        }

        public string getDNCLabel(string DNCpath)
        // checks for existence of DNC metadata csv file
        // reads and returns single string designed to be assigned to Label12 on the form
        {
            string label="";
            if (File.Exists(DNCpath))
            {
                using (TextFieldParser parser = new TextFieldParser(DNCpath, System.Text.Encoding.GetEncoding(1252)))
                {
                    // full qualification of FieldType required to avoid confuision with Arc FieldType
                    parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.HasFieldsEnclosedInQuotes = true;

                    // read first row
                    string[] fields = parser.ReadFields();
                    if (fields[0] == "DNC Filename")
                    {
                        // looks to contain correct information
                        // read second row
                        fields = parser.ReadFields();
                        label= "Clause values and descriptions based on those in: " + fields[0] + " dated " + fields[1];
                    }
                }
            }
            return label;

        }

        
            public string getGeomType()
            // queries shapefile that is to be renamed and returns geometry type as string
            // with same values as expected by DNC
        {
            IGxApplication pApp = ArcMap.Application as IGxApplication;
            string pathFileName = pApp.SelectedObject.FullName;
            string root = System.IO.Path.GetDirectoryName(pathFileName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(pathFileName);
            IFeatureClass fc = GetFeatureClassFromShapefileOnDisk(root, filename);
            // get geometry type of feature class
            string geomtype;
            switch (fc.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    {
                        geomtype = "pt";
                        break;
                    }
                case esriGeometryType.esriGeometryPolyline:
                    {
                        geomtype = "ln";
                        break;
                    }
                case esriGeometryType.esriGeometryPolygon:
                    {
                        geomtype = "py";
                        break;
                    }
                default:
                    {
                        geomtype = "other";
                        break;
                    }

            }
            return geomtype;
        }


        public void comboValues(ComboBox cbo, string path)
        {

            Dictionary<string, string> d = RenameLayerToolValues.csvToDictionary(path);
            cbo.DataSource = new BindingSource(d, null);
            cbo.ValueMember = "Key";
            cbo.DisplayMember = "Value";
            
        }

        public void comboValues(ComboBox cbo, string path, string selCategory)
        {

            Dictionary<string, string> d = RenameLayerToolValues.csvToDictionary(path,selCategory);
            cbo.DataSource = new BindingSource(d, null);
            cbo.ValueMember = "Key";
            cbo.DisplayMember = "Value";

        }

        //Change the GUI to reflect a change in the check box status for each input field
        public void ifCheckBoxChanged(CheckBox chk, ComboBox cbo, TextBox tbx, string path)
        {
            if (!chk.Checked)
            {
                cbo.Enabled = true;
                tbx.Enabled = false;
                tbx.Text = string.Empty;
                comboValues(cbo, path);
                cbo.Focus();
            }
            else
            {
                cbo.Enabled = false;
                cbo.Text = string.Empty;
                tbx.Enabled = true;
                tbx.Focus();

            }

            lblReviewLayerName.Text = createNewLayerName();
        }

        // overload version for case when Theme check box changed  PJR 20/10/2016
        public void ifCheckBoxChanged(CheckBox chk, ComboBox cbo, TextBox tbx, string path, string selCategory)
        {
            if (!chk.Checked)
            {
                cbo.Enabled = true;
                tbx.Enabled = false;
                tbx.Text = string.Empty;
                comboValues(cbo, path,selCategory);
                cbo.Focus();
            }
            else
            {
                cbo.Enabled = false;
                cbo.Text = string.Empty;
                tbx.Enabled = true;
                tbx.Focus();

            }

            lblReviewLayerName.Text = createNewLayerName();
        }

        private void chkGeoExtent_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkGeoExtent, cboGeoExtent, tbxGeoExtent, _extent_path);
        }

        private void cboGeoExtent_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxGeoExtent_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void cboCategory_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
            // regenerate combolist for theme based on selected Category
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            comboValues(cboTheme, _theme_path,_category);
        }

        private void cboTheme_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void cboType_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void cboScale_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void cboSource_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void cboPermission_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxCategory_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
            // regenerate combolist for theme based on selected Category
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            comboValues(cboTheme, _theme_path, _category);
        }

        private void tbxTheme_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
            // regenerate combolist for theme based on selected Category
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            comboValues(cboTheme, _theme_path, _category);
        }

        private void tbxType_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxScale_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxSource_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxPermission_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxFreeText_TextChanged(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkCategory, cboCategory, tbxCategory, _category_path);
        }

        private void chkTheme_CheckedChanged(object sender, EventArgs e)
        {
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
                        
            ifCheckBoxChanged(chkTheme, cboTheme, tbxTheme, _theme_path, _category);
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkType, cboType, tbxType, _type_path);
        }

        private void chkScale_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkScale, cboScale, tbxScale, _scale_path);
        }

        private void chkSource_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkSource, cboSource, tbxSource, _source_path);
        }

        private void chkPermission_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkPermission, cboPermission, tbxPermission, _permission_path);
        }

        private void tbxFreeText_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void tbxFreeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void tbxGeoExtent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        

       


    }
}
