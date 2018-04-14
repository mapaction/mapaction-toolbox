using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.Diagnostics;

namespace MapActionToolbars
{
    class LayoutToolAutomatedValues
    {
        /*################################
        Tab 1 automated values
        ##################################*/

        //Returns the automated value for the glide number from the operation config file
        public static string getGlideNo()
        {
            string GlideNo = string.Empty;
            string path = MapAction.Utilities.getOperationConfigFilePath();

            if (MapAction.Utilities.detectOperationConfig())
            {
                MapAction.OperationConfig config = MapAction.Utilities.getOperationConfigValues(path);
                GlideNo = config.GlideNo;
            }
            return GlideNo;
        }

        /*################################
        Tab 2 automated values
        ##################################*/

        public static string getConfigDisclaimer()
        {
            string DefaultDisclaimerText = string.Empty;
            string path = MapAction.Utilities.getOperationConfigFilePath();

            if (MapAction.Utilities.detectOperationConfig())
            {
                MapAction.OperationConfig config =  MapAction.Utilities.getOperationConfigValues(path);
                DefaultDisclaimerText = config.DefaultDisclaimerText;
            }
            return DefaultDisclaimerText;
        }

        public static string getConfigDonorText()
        {
            string DefaultDonorsText = string.Empty;
            string path = MapAction.Utilities.getOperationConfigFilePath();

            if (MapAction.Utilities.detectOperationConfig())
            {
                MapAction.OperationConfig config = MapAction.Utilities.getOperationConfigValues(path);
                DefaultDonorsText = config.DefaultDonorsText;
            }
            return DefaultDonorsText;
        }

        public static string getConfigTimezone()
        {
            string DefaultTimeZone = string.Empty;
            string path = MapAction.Utilities.getOperationConfigFilePath();

            if (MapAction.Utilities.detectOperationConfig())
            {
                MapAction.OperationConfig config = MapAction.Utilities.getOperationConfigValues(path);
                DefaultTimeZone = config.TimeZone;
            }
            return DefaultTimeZone;
        }

        public static string getProducedByText()
        {
            //string OrganisationDetailsText = string.Empty;
            string OrgName = string.Empty;
            string OrgUrl = string.Empty;
            string PrimaryEmail = string.Empty;
            
            string path = MapAction.Utilities.getOperationConfigFilePath();

            if (MapAction.Utilities.detectOperationConfig())
            {
                MapAction.OperationConfig config = MapAction.Utilities.getOperationConfigValues(path);

                OrgName = config.DefaultSourceOrganisation;
                OrgUrl = config.DefaultSourceOrganisationUrl;
                PrimaryEmail = config.DeploymentPrimaryEmail;
                string OrganisationDetailsText = "Produced by " + OrgName + " " + OrgUrl + Environment.NewLine + PrimaryEmail;
                return OrganisationDetailsText;
            }
            else
            {
                return string.Empty;
            }            
        }
    }
}
