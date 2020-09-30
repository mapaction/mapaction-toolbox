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
using ESRI.ArcGIS.Framework;
using System.Diagnostics;

namespace MapActionToolbar_Forms
{
    class LayoutToolAutomatedValues
    {
        /*################################
        Tab 1 automated values
        ##################################*/

        //Returns the automated value for the glide number from the event config file
        public static string getGlideNo()
        {
            string GlideNo = string.Empty;
            string path = MapActionToolbar_Core.Utilities.getEventConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                MapActionToolbar_Core.EventConfig config = MapActionToolbar_Core.Utilities.getEventConfigValues(path);
                GlideNo = config.GlideNumber;
            }
            return GlideNo;
        }

        /*################################
        Tab 2 automated values
        ##################################*/

        public static string getConfigDisclaimer()
        {
            string DefaultDisclaimerText = string.Empty;
            string path = MapActionToolbar_Core.Utilities.getEventConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                MapActionToolbar_Core.EventConfig config =  MapActionToolbar_Core.Utilities.getEventConfigValues(path);
                DefaultDisclaimerText = config.DefaultDisclaimerText;
            }
            return DefaultDisclaimerText;
        }

        public static string getConfigDonorText()
        {
            string DefaultDonorsText = string.Empty;
            string path = MapActionToolbar_Core.Utilities.getEventConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                MapActionToolbar_Core.EventConfig config = MapActionToolbar_Core.Utilities.getEventConfigValues(path);
                DefaultDonorsText = config.DefaultDonorCredits;
            }
            return DefaultDonorsText;
        }

        public static string getConfigTimezone()
        {
            string DefaultTimeZone = string.Empty;
            string path = MapActionToolbar_Core.Utilities.getEventConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                MapActionToolbar_Core.EventConfig config = MapActionToolbar_Core.Utilities.getEventConfigValues(path);
                DefaultTimeZone = config.TimeZone;
            }
            return DefaultTimeZone;
        }

        public static string getProducedByText()
        {
            string OrgName = string.Empty;
            string OrgUrl = string.Empty;
            string PrimaryEmail = string.Empty;
            
            string path = MapActionToolbar_Core.Utilities.getEventConfigFilePath();

            if (MapActionToolbar_Core.Utilities.detectEventConfig())
            {
                MapActionToolbar_Core.EventConfig config = MapActionToolbar_Core.Utilities.getEventConfigValues(path);

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