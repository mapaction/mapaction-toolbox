using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;


namespace Prototype1_DatasetRename
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            IGeoProcessor2 gp = new GeoProcessorClass();
            
            IVariantArray parameters = new VarArrayClass();

            // Populate the variant array with parameter values.
            parameters.Add(@"C:\gis\Projects\syria\data\temp\Export_Output.shp");
            parameters.Add(@"C:\gis\Projects\syria\data\temp\Export_Output2.shp");
            // Execute the tool
            gp.Execute("Rename_management", parameters, null); 



        }



    }
}
