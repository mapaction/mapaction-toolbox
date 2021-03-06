﻿using System;
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
        private const string _eventConfigJsonFileName = "event_description.json";
        private string crashMoveFolder = "";
        private string cookbookFullPath = "";
        private string layerPropertiesFullPath = "";
        private string layerDirectory = "";
        private Cookbook cookbook = null;
        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;
        
        public frmGenerationTool()
        {
            this.crashMoveFolder = Utilities.getCrashMoveFolderPath();
            if (MapAction.Utilities.detectCrashMoveFolderConfig())
            {
                InitializeComponent();                
                string path = MapAction.Utilities.getCrashMoveFolderConfigFilePath();

                CrashMoveFolderConfig config = MapAction.Utilities.getCrashMoveFolderConfigValues(path);
                this.cookbookFullPath = System.IO.Path.Combine(this.crashMoveFolder, config.MapDefinitions);
                this.layerPropertiesFullPath = System.IO.Path.Combine(this.crashMoveFolder, config.LayerProperties);
                this.layerDirectory = System.IO.Path.Combine(this.crashMoveFolder, config.LayerRendering);
            }
            else
            {
                MessageBox.Show("Crash Move Folder must contain valid cmf_description.json file.",
                                "Configuration file required",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmGenerationTool_Load(object sender, EventArgs e)
        {
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = System.IO.Path.Combine(path, _eventConfigJsonFileName);
            EventConfig config = MapAction.Utilities.getEventConfigValues(filePath);
            tbxGeoExtent.Text = MapAction.Utilities.getCountries().nameFromAlpha3Code(config.AffectedCountryIso3);

            cookbook = new Cookbook(this.cookbookFullPath);

            if (cookbook != null)
            {
                List<string> categoryDict = new List<string>();

                foreach (var c in cookbook.categories)
                {
                    categoryDict.Add(c.ToString());
                }
                cboCategory.DataSource = null;
                cboCategory.Items.Clear();
                cboCategory.DataSource = new BindingSource(categoryDict, null);
                refreshProductTypes();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            bool prerequisites = true;
            IGeoProcessor2 gp = new GeoProcessor() as IGeoProcessor2;
            gp.AddToolbox(Utilities.getExportGPToolboxPath());
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;

            string[] expectedDirectories = { this.crashMoveFolder};
            string[] expectedFiles = { this.cookbookFullPath };
            string errorMessage = "Could not execute automation.  The following paths are required:\n";

            foreach (string directoryName in expectedDirectories)
            {
                if (!Directory.Exists(directoryName))
                {
                    errorMessage = errorMessage + "\n" + directoryName;
                    prerequisites = false;
                }
            }

            foreach (string fileName in expectedFiles)
            {
                if (!File.Exists(fileName))
                {
                    errorMessage = errorMessage + "\n" + fileName;
                    prerequisites = false;
                }
            }

            if (prerequisites)
            {
                btnGenerate.Enabled = false;
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
                string rawJson = "";
                for (int o = 0; o < pyResult.OutputCount; o++)
                {
                    var opMsg = pyResult.GetOutput(o);
                    rawJson = opMsg.GetAsText();
                }

                AutomationReport automationResult = JsonConvert.DeserializeObject<AutomationReport>(rawJson);
                var dlg = new frmAutomationResult();
                dlg.SetContent(automationResult);

                if (dlg.Text.Length > 0)
                {
                    dlg.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(errorMessage,
                    "Automation Failure",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
            var cb = cookbook.recipeByCategory(cboCategory.Text);
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
