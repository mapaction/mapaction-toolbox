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
using Microsoft.VisualBasic.FileIO;

namespace MapActionToolbars
{
    public partial class frmRenameMain : Form
    {
        //Create a local variable to set the path to each of the csv files that store the lookup values
        // added numeric prefixes and changed type to 04_geometry to fit in with revised DNC. PJR 18/08/2016
        
        // new csv to hold metadata on DNC.  PJR 21/10/2016
        // remember to change data as necessary when RenameLayer tool updates or DNC updates
        MADataRenameProperties _Properties;
        private string pathFileName;
        private string latestValidText;
        private const int MAX_PATH_LENGTH = 260;
        private const string invalidCharacters = @"\/:*?""<>|#&_ ";
        private const string renameAction = "Renamed";
        private const string copyAction = "Copied";
        private const string dialogBoxTitleOperationFailed = "Operation failed";
        private const string dialogBoxTitleOperationSuccessful = "Operation successful";
        public bool initialised;

        private static bool ContainsInvalidCharacters(string input)
        {
            bool result = input.IndexOfAny(invalidCharacters.ToCharArray()) != -1;
            return result;
        }

        public frmRenameMain()
        {
            initialised = false;
            _Properties = new MADataRenameProperties();
            if (_Properties.initialised)
            {
                InitializeComponent();
                initialised = true;
            }
            else
            {
                MessageBox.Show("Can't find DNC lookup CSV files.  If working with crash move folder, then please set the correct path to crash move folder in the config tool. Otherwise, CSV files are expected in folder C:\\MapAction\\200_data_name_lookup", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            string newPath = "";
            string root = System.IO.Path.GetDirectoryName(this.pathFileName);           
            string fileExtension = System.IO.Path.GetExtension(this.pathFileName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(this.pathFileName);
            bool operationSuccessful = false;
            IFeatureClass fc = GetFeatureClassFromShapefileOnDisk(root, filename);
            IDataset ds = fc as IDataset;
            
            // Message box contents
            MessageBoxIcon icon = MessageBoxIcon.Information;
            string dialogBoxTitle = dialogBoxTitleOperationSuccessful;
            string message = "";


            //Construct layer name
            string newLayerName = createNewLayerName();

            // Is the path a crash move folder?   
            if (HasCrashMoveFolderStructure(root))
            {   
                // If it is, copy the renamed file(s) to the appropriate directory.
                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                ESRI.ArcGIS.Geodatabase.IWorkspace workspace = workspaceFactory.OpenFromFile(CategoryPath(root, Category()), 0);

                newPath = System.IO.Path.Combine(workspace.PathName, (newLayerName + fileExtension));
                if (newPath.Length > MAX_PATH_LENGTH)
                {
                    dialogBoxTitle = dialogBoxTitleOperationFailed;
                    icon = MessageBoxIcon.Error;
                    message = String.Format("Target path exceeds maximum permitted length of {0} characters", MAX_PATH_LENGTH);
                }
                else
                {
                    ds.Copy(newLayerName, workspace);
                    message = copyAction + " " + System.IO.Path.Combine(root, filename + fileExtension) + " to " + System.IO.Path.Combine(workspace.PathName, (newLayerName + fileExtension));
                    operationSuccessful = true;
                }
            }
            else
            {
                newPath = System.IO.Path.Combine(root, newLayerName + fileExtension);
                if (newPath.Length > MAX_PATH_LENGTH)
                {
                    dialogBoxTitle = dialogBoxTitleOperationFailed;
                    icon = MessageBoxIcon.Error;
                    message = String.Format("Target path exceeds maximum permitted length of {0} characters", MAX_PATH_LENGTH);
                }
                else
                {
                    message = renameAction + " " + System.IO.Path.Combine(root, filename + fileExtension) + " to " + newPath;
                    //Rename the layer
                    ds.Rename(newLayerName);
                    operationSuccessful = true;
                }
            }
            MessageBox.Show(message, dialogBoxTitle, MessageBoxButtons.OK, icon);
            if (operationSuccessful)
            {
                this.Close();
            }
        }

        public string createNewLayerName()
        {
            //Construct layer name
            ConstructLayerName newLayer = new ConstructLayerName();
            string newLayerName = newLayer.LoopThroughNameElements(GetFormElementValues());
            return newLayerName;
        }

        public string Category()
        {
            string category;
            if (!chkCategory.Checked) 
            { 
                category = cboCategory.SelectedValue.ToString(); 
            } 
            else 
            { 
                category = tbxCategory.Text; 
            };
            return category;
        }

        public string[] GetFormElementValues()
        {
            string _geoExtent;
            string _theme;
            string _type;
            string _scale;
            string _source;
            string _permission;
            string _freeText;

            if (!chkGeoExtent.Checked) {_geoExtent = cboGeoExtent.SelectedValue.ToString(); } else { _geoExtent = tbxGeoExtent.Text; };
            if (!chkTheme.Checked) { _theme = cboTheme.SelectedValue.ToString(); } else { _theme = tbxTheme.Text; };
            if (!chkType.Checked) { _type = cboType.SelectedValue.ToString(); } else { _type = tbxType.Text; };
            if (!chkScale.Checked) { _scale = cboScale.SelectedValue.ToString(); } else { _scale = tbxScale.Text; };
            if (!chkSource.Checked) { _source = cboSource.SelectedValue.ToString(); } else { _source = tbxSource.Text; };
            if (!chkPermission.Checked) { _permission = cboPermission.SelectedValue.ToString(); } else { _permission = tbxPermission.Text; };
            _freeText = tbxFreeText.Text;
            
            //Create array of all the above elements
            string[] arr = { _geoExtent, Category(), _theme, _type, _scale, _source, _permission, _freeText };
            
            return arr;
        }

        public ESRI.ArcGIS.Geodatabase.IFeatureClass GetFeatureClassFromShapefileOnDisk(System.String string_ShapefileDirectory, System.String string_ShapefileName)
        {

            System.IO.DirectoryInfo directoryInfo_check = new System.IO.DirectoryInfo(string_ShapefileDirectory);
            if (directoryInfo_check.Exists)
            {
                //We have a valid directory, proceed
                System.IO.FileInfo fileInfo_check = new System.IO.FileInfo(System.IO.Path.Combine(string_ShapefileDirectory, string_ShapefileName + ".shp"));
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
            btnRename.Enabled = false;
            // Add metadata/ QA info on this tool and on DNC to form  PJR 21/10/2016
            this.Text = "MapAction Dataset Rename Tool " + _Properties.RenameLayerVersion;
            label12.Text = getDNCLabel(_Properties.DNCmetadataPath);

            //Populate combo box with value from the CSV files 
            comboValues(cboGeoExtent, _Properties.ExtentPath);
            comboValues(cboCategory, _Properties.CategoryPath);
            comboValues(cboTheme, _Properties.ThemePath);
            comboValues(cboType, _Properties.TypePath);
            
            comboValues(cboScale, _Properties.ScalePath);
            comboValues(cboSource, _Properties.SourcePath);
            comboValues(cboPermission, _Properties.PermissionPath);

            cboGeoExtent.Enabled = false;
            chkGeoExtent.Enabled = false;
            cboCategory.Enabled = false;
            chkCategory.Enabled = false;
            cboTheme.Enabled = false;
            chkTheme.Enabled = false;
            cboType.Enabled = false;
            chkType.Enabled = false;
            cboScale.Enabled = false;
            chkScale.Enabled = false;
            cboSource.Enabled = false;
            chkSource.Enabled = false;
            cboPermission.Enabled = false;
            chkPermission.Enabled = false;
            tbxFreeText.Enabled = false;
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
            string root = System.IO.Path.GetDirectoryName(this.pathFileName);
            string filename = System.IO.Path.GetFileNameWithoutExtension(this.pathFileName);
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

        public bool HasCrashMoveFolderStructure(System.String string_ShapefileDirectory)
        {
            bool result = false;
            const System.String activeDataDirectory = "2_Active_Data";

            System.String pathRoot = System.IO.Path.GetPathRoot(string_ShapefileDirectory);
            string testPath = string_ShapefileDirectory;
            System.IO.Path.GetFullPath(testPath).Equals(System.IO.Path.GetFullPath(pathRoot));
            System.String activeDataPath = System.IO.Path.Combine(testPath, activeDataDirectory);

            if (System.IO.Directory.Exists(activeDataPath))
            {
                result = true;
            }
            else
            {
                while ((!System.IO.Path.GetFullPath(testPath).Equals(System.IO.Path.GetFullPath(pathRoot))) && result == false)
                {
                    testPath = System.IO.Path.Combine(testPath, "..");
                    activeDataPath = System.IO.Path.Combine(testPath, activeDataDirectory);
                    // Does path exist?
                    if (System.IO.Directory.Exists(activeDataPath))
                    {
                        result = true;
                    }
                }
            }
            Console.WriteLine(activeDataPath);
            return result;
        }

        public string ActiveDataPath(System.String string_ShapefileDirectory)
        {
            bool result = false;
            const System.String activeDataDirectory = "2_Active_Data";

            System.String pathRoot = System.IO.Path.GetPathRoot(string_ShapefileDirectory);
            string testPath = string_ShapefileDirectory;
            System.IO.Path.GetFullPath(testPath).Equals(System.IO.Path.GetFullPath(pathRoot));
            System.String activeDataPath = System.IO.Path.Combine(testPath, activeDataDirectory);

            if (System.IO.Directory.Exists(activeDataPath))
            {
                result = true;
            }
            else
            {
                while ((!System.IO.Path.GetFullPath(testPath).Equals(System.IO.Path.GetFullPath(pathRoot))) && result == false)
                {
                    testPath = System.IO.Path.Combine(testPath, "..");
                    activeDataPath = System.IO.Path.Combine(testPath, activeDataDirectory);
                    // Does path exist?
                    if (System.IO.Directory.Exists(activeDataPath))
                    {
                        result = true;
                    }
                }
            }
            return activeDataPath;
        }

        public string CategoryPath(System.String string_ShapefileDirectory, string category)
        {
            string activeDataPath = ActiveDataPath(string_ShapefileDirectory);

            string[] directories = Directory.GetDirectories(string_ShapefileDirectory);

            string categoryPath = "";
            try 
            {
                string[] dirs = Directory.GetDirectories(activeDataPath, ("*" + category), System.IO.SearchOption.TopDirectoryOnly);
                foreach (string dir in dirs) 
                {
                    categoryPath = dir;
                    break;
                }

                // If no category path detected, create one.
                if (categoryPath.Length == 0)
                {
                    categoryPath = System.IO.Path.Combine(activeDataPath, category);
                    Directory.CreateDirectory(categoryPath);
                }
            } 
            catch (Exception e) 
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return categoryPath;
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
            ifCheckBoxChanged(chkGeoExtent, cboGeoExtent, tbxGeoExtent, _Properties.ExtentPath);
        }

        private void cboGeoExtent_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void checkForInvalidCharacters(object sender, EventArgs e)
        {
            TextBox target = sender as TextBox;
            if (ContainsInvalidCharacters(target.Text))
            {
                // display alert and reset text
                MessageBox.Show(String.Format("The text may not contain characters: {0}", invalidCharacters));
                target.Text = latestValidText;
            }
            else
            {
                latestValidText = target.Text;
            }
        }

        private void tbxGeoExtent_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void cboCategory_DropDownClosed(object sender, EventArgs e)
        {
            lblReviewLayerName.Text = createNewLayerName();
            // regenerate combolist for theme based on selected Category
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            comboValues(cboTheme, _Properties.ThemePath,_category);
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
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();

            // regenerate combolist for theme based on selected Category
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            comboValues(cboTheme, _Properties.ThemePath, _category);
        }

        private void tbxTheme_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
            // regenerate combolist for theme based on selected Category
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };
            comboValues(cboTheme, _Properties.ThemePath, _category);
        }

        private void tbxType_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxScale_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxSource_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxPermission_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void tbxFreeText_TextChanged(object sender, EventArgs e)
        {
            checkForInvalidCharacters(sender, e);
            lblReviewLayerName.Text = createNewLayerName();
        }

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkCategory, cboCategory, tbxCategory, _Properties.CategoryPath);
        }

        private void chkTheme_CheckedChanged(object sender, EventArgs e)
        {
            string _category;
            if (!chkCategory.Checked) { _category = cboCategory.SelectedValue.ToString(); } else { _category = tbxCategory.Text; };

            ifCheckBoxChanged(chkTheme, cboTheme, tbxTheme, _Properties.ThemePath, _category);
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkType, cboType, tbxType, _Properties.TypePath);
        }

        private void chkScale_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkScale, cboScale, tbxScale, _Properties.ScalePath);
        }

        private void chkSource_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkSource, cboSource, tbxSource, _Properties.SourcePath);
        }

        private void chkPermission_CheckedChanged(object sender, EventArgs e)
        {
            ifCheckBoxChanged(chkPermission, cboPermission, tbxPermission, _Properties.PermissionPath);
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

        private void btnNavigateToShapeFile_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = RenameLayerToolValues.initialDirectory;
                openFileDialog.Filter = "shp files (*.shp)|*.shp";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                    
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    RenameLayerToolValues.initialDirectory = new FileInfo(filePath).Directory.FullName;

                    tbxPathToShapeFile.Text = filePath;
                    this.pathFileName = filePath;
                    string root = System.IO.Path.GetDirectoryName(filePath);
                    string filename = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    string geomtype = "other";
                    if (System.IO.Path.GetExtension(filePath).ToLower() == ".shp")
                    {
                        geomtype = getGeomType();
                    }

                    if (geomtype != "other") { cboType.SelectedValue = geomtype; }
                    btnRename.Enabled = true;
                    cboGeoExtent.Enabled = true;
                    chkGeoExtent.Enabled = true;
                    cboCategory.Enabled = true;
                    chkCategory.Enabled = true;
                    cboTheme.Enabled = true;
                    chkTheme.Enabled = true;
                    cboType.Enabled = true;
                    chkType.Enabled = true;
                    cboScale.Enabled = true;
                    chkScale.Enabled = true;
                    cboSource.Enabled = true;
                    chkSource.Enabled = true;
                    cboPermission.Enabled = true;
                    chkPermission.Enabled = true;
                    tbxFreeText.Enabled = true;

                    // construct and display new file name if geometry type is already set
                    if (geomtype != "other") { lblReviewLayerName.Text = createNewLayerName(); }
                }
            }
        }
    }
}
