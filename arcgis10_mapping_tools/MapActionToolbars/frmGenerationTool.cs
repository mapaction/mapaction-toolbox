using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using MapAction;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json.Linq;
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

namespace MapActionToolbars
{
    public class Cookbook
    {
        public List<Product> recipes;
    }

    public class Product
    {
        public string product;
        public List<string> layers;
    }


    public partial class frmGenerationTool : Form
    {
        // private static string targetMapFrame = "Main map";
        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;

        private readonly string cookbookFileName = "mapCookbook.json";
        private readonly string layerPropertiesFileName = "layerProperties.json";
        private readonly string automationDirectory = "GIS\\3_Mapping\\31_Resources\\316_Automation";
        private readonly string layerDirectorySubPath = "GIS\\3_Mapping\\38_Initial_Maps_Layer_Files\\All";
        private const string _operationConfigXmlFileName = "operation_config.xml";

        private string crashMoveFolder = "";
        private string cookbookFullPath = "";
        private string layerPropertiesFullPath = "";
        private string layerDirectory = "";

        public frmGenerationTool()
        {
            InitializeComponent();
            this.crashMoveFolder = Utilities.getCrashMoveFolderPath();
            this.cookbookFullPath = System.IO.Path.Combine(this.crashMoveFolder, this.automationDirectory, this.cookbookFileName);
            this.layerPropertiesFullPath = System.IO.Path.Combine(this.crashMoveFolder, this.automationDirectory, this.layerPropertiesFileName);
            this.layerDirectory = System.IO.Path.Combine(this.crashMoveFolder, layerDirectorySubPath);
        }

        private void gbxCrashMoveFolder_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
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
            /*
            for (int i = 0; i < pyResult.MessageCount; i++)
            {
                string msg = pyResult.GetMessage(i);
                Console.WriteLine(pyResult.GetMessage(i));
            }

            for (int i = 0; i < pyResult.OutputCount; i++)
            {
                var op = pyResult.GetOutput(i);
                    //.getOutput(i);
                Console.WriteLine(pyResult.GetMessage(i));
            }

            var banana = pyResult.GetResultMessages();
            string oi = "..";
            */
            MessageBox.Show("Product \"" + cboProductType.Text + "\" generated.", "Map Action Automation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        
        private void frmGenerationTool_Load(object sender, EventArgs e)
        {
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = System.IO.Path.Combine(path, _operationConfigXmlFileName);
            OperationConfig config = MapAction.Utilities.getOperationConfigValues(filePath);
            tbxGeoExtent.Text = config.Country;

            if (File.Exists(this.cookbookFullPath))
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                string json = File.ReadAllText(this.cookbookFullPath);

                var cookbook = JsonConvert.DeserializeObject<Cookbook>(json);

                foreach (var r in cookbook.recipes)
                {
                    Console.WriteLine("{0}\n", r.product);
                    d.Add(r.product, r.product);
                }
                cboProductType.DataSource = new BindingSource(d, null);
                cboProductType.ValueMember = "Key";
                cboProductType.DisplayMember = "Value";
            }
        }

        private void cboProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(cboProductType.SelectedIndex);
        }
    }
}
