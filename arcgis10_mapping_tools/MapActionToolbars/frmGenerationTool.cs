using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;
using MapAction;


namespace MapActionToolbars
{
    public partial class frmGenerationTool : Form
    {
        private readonly string cookbookFileName = "mapCookbook.json";
        private readonly string layerPropertiesFileName = "layerProperties.json";
        private readonly string automationDirectory = "GIS\\3_Mapping\\31_Resources\\316_Automation";
        private readonly string layerDirectorySubPath = "GIS\\3_Mapping\\38_Initial_Maps_Layer_Files\\All";
        private const string _operationConfigXmlFileName = "operation_config.xml";

        private string crashMoveFolder = "";
        private string cookbookFullPath = "";
        private string layerPropertiesFullPath = "";
        private string layerDirectory = "";
        private Cookbook cookbook = null;
        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;
        
        public frmGenerationTool()
        {
            InitializeComponent();
            this.crashMoveFolder = Utilities.getCrashMoveFolderPath();
            this.cookbookFullPath = System.IO.Path.Combine(this.crashMoveFolder, this.automationDirectory, this.cookbookFileName);
            this.layerPropertiesFullPath = System.IO.Path.Combine(this.crashMoveFolder, this.automationDirectory, this.layerPropertiesFileName);
            this.layerDirectory = System.IO.Path.Combine(this.crashMoveFolder, layerDirectorySubPath);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmGenerationTool_Load(object sender, EventArgs e)
        {
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = System.IO.Path.Combine(path, _operationConfigXmlFileName);
            OperationConfig config = MapAction.Utilities.getOperationConfigValues(filePath);
            tbxGeoExtent.Text = config.Country;

            cookbook = new Cookbook(this.cookbookFullPath);

            if (cookbook != null)
            {
                List<string> classificationDict = new List<string>();

                foreach (var c in cookbook.classifications)
                {
                    classificationDict.Add(c.ToString());
                }

                cboClassification.DataSource = null;
                cboClassification.Items.Clear();
                cboClassification.DataSource = new BindingSource(classificationDict, null);
                refreshProductTypes();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            IGeoProcessor2 gp = new GeoProcessor() as IGeoProcessor2;
            gp.AddToolbox(Utilities.getExportGPToolboxPath());
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;

            IVariantArray parameters = new VarArray();
            parameters.Add(cboProductType.Text);          // Parameter 0
            parameters.Add(tbxGeoExtent.Text);            // Parameter 1
            parameters.Add(this.cookbookFullPath);        // Parameter 2
            parameters.Add(this.layerPropertiesFullPath); // Parameter 3
            parameters.Add(this.crashMoveFolder);         // Parameter 4
            parameters.Add(this.layerDirectory);          // Parameter 5

            object sev = null;
            IGeoProcessorResult2 pyResult = null;
            try
            {
                pyResult = (IGeoProcessorResult2)gp.Execute("generateProduct", parameters, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string errorMsgs = gp.GetMessages(ref sev);
                Console.WriteLine(errorMsgs);
                throw;
            }
            //MessageBox.Show("Product \"" + cboProductType.Text + "\" generated.", "Map Action Automation", MessageBoxButtons.OK, MessageBoxIcon.Information);


            string rawJson = "{ \"result\": \"Success\", \"summary\": \"Map generation complete\", \"productName\": \"Country Overview\", \"classfication\": \"Core\", \"results\": [ { \"layerName\": \"Settlements - Places\", \"dateStamp\": \"01-08-2019 20:45:12\", \"dataSource\": \"D:/MapAction/2019-06-25 - Automation - El Salvador/GIS/2_Active_Data/229_stle/slv_stle_stl_pt_s0_osm_pp_places.shp\", \"added\": true, \"message\": \"Layer added successfully\" },	{ \"layerName\": \"Transport - Airports\", \"dateStamp\": \"01-08-2019 20:45:19\", \"dataSource\": \"D:/MapAction/2019-06-25 - Automation - El Salvador/GIS/2_Active_Data/232_tran/wrl_tran_air_pt_s0_ouairports_pp_airports.shp\", \"added\": true, \"message\": \"Layer added successfully\" } ] }";
            AutomationReport automationResult = JsonConvert.DeserializeObject<AutomationReport>(rawJson);
            var dlg = new frmAutomationResult();
            dlg.SetContent(automationResult);

            if (dlg.Text.Length > 0)
            {
                dlg.ShowDialog();
            }
            this.Close();
        }

        private void cbxClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshProductTypes();
        }

        private void cbxProductType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refreshProductTypes()
        {
            var cb = cookbook.recipeByClassification(cboClassification.Text);
            List<string> d = new List<string>();

            foreach (var r in cb)
            {
                d.Add(r.product);
            }
            cboProductType.DataSource = null;
            cboProductType.Items.Clear();
            cboProductType.DataSource = new BindingSource(d, null);
        }
    }
}
