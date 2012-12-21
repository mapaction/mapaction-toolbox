using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;

namespace Prototype1_LayoutTool
{
    public partial class frmCheckElements : Form
    {
        public frmCheckElements()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCheckElements_Load(object sender, EventArgs e)
        {
            //Call the MapAction class library and the getLayoutElements function that returns a dictionare of the key value
            //pairs of each text element in the layout
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            Dictionary<string, string> dict = MapAction.LayoutElements.getLayoutTextElements(pMxDoc, "Main map");
            
            //Check for the presence of text element items in the layout, if present change image to tick
            if (dict.ContainsKey("title"))
            {
                imgTitleStatus.Image = Properties.Resources.tick_17px;
            }

            if (dict.ContainsKey("summary"))
            {
                imgSummaryStatus.Image = Properties.Resources.tick_17px;
            }
            
            if (dict.ContainsKey("map_no"))
            {
                imgMapNoStatus.Image = Properties.Resources.tick_17px;
            }

            if (dict.ContainsKey("mxd_name"))
            {
                imgMxdNameStatus.Image = Properties.Resources.tick_17px;
            }
            
            if (dict.ContainsKey("spatial_reference"))
            {
                imgSpatialRefStatus.Image = Properties.Resources.tick_17px;
            }
            
            if (dict.ContainsKey("scale"))
            {
                imgScaleStatus.Image = Properties.Resources.tick_17px;
            }
            
            if (dict.ContainsKey("glide_no"))
            {
                imgGlideNoStatus.Image = Properties.Resources.tick_17px;
            }
            //Detect 'Main map' frame
            //if (MapAction.LayoutElements.detectMapFrame(pMxDoc, "Main map"))
            //{
            //    imgMainMapFrameStatus.Image = Properties.Resources.tick_17px;
            //}
        }


    }
}
