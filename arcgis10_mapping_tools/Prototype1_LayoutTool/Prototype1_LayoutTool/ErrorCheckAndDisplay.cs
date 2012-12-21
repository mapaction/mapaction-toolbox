using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;



namespace Prototype1_LayoutTool
{
    public static class ErrorCheckAndDisplay
    {
        

        public static void checkElement(ToolTip tooltip, TextBox element, string element_name) 
        {
            Dictionary<string, string> _dictConfig = MapAction.Utilities.getOperationConfigValues();

            if (element_name == "Operation Name")
            {
                if (element.Text != "Haiti")
                {
                    //Set the tooltip
                    tooltip.Active = true;
                    tooltip.ToolTipTitle = element_name;
                    tooltip.SetToolTip(element, "Please update the operation name.");
                    tooltip.ToolTipIcon = ToolTipIcon.Info;

                    //Set the border controls
                    //element.BackColor = Color.AliceBlue;
                    element.BackColor = ColorTranslator.FromHtml("#EEEEE0");
                    //element.BackColor = Color.FromArgb(0xE9, 0xE9, 0xE9);

                }
                else
                {
                    element.BackColor = Color.White;
                    tooltip.Active = false;

                }
            }
            else if (element_name == "Glide Number")
            {
                string glide_no = string.Empty;
                if (_dictConfig.ContainsKey("glide_no")) {glide_no = _dictConfig["glide_no"]; };

                if (element.Text != glide_no)
                {
                    //Set the tooltip
                    tooltip.Active = true;
                    tooltip.ToolTipTitle = element_name;
                    tooltip.SetToolTip(element, "Does not match the Emergency xml file.");
                    tooltip.ToolTipIcon = ToolTipIcon.Error;

                    //Set the border controls
                    element.BackColor = ColorTranslator.FromHtml("#FFE5EB");
                }
                else
                {
                    element.BackColor = Color.White;
                    tooltip.Active = false;
                }

            }
        }

    }
}
