using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Alpha_LayoutTool
{
    public static class FormValidation
    {
        //General validation checks 
        private static Boolean validateEmptyField(Control control, ErrorProvider epr)
        {

            if (control.Text.Trim() == String.Empty)
            {
                epr.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                epr.SetError(control, "Textbox is empty");
                return false;
            }
            else
            {
                epr.SetError(control, "");
                return true;
            }

        }

        //Validate individual form elements
        public static void validateMapNumber(Control control, ErrorProvider epr)
        {
            Match match = Regex.Match(control.Text, @"MA\d\d\d");
            epr.SetIconPadding(control, 3);

            //Run the validation
            if (validateEmptyField(control, epr))
            {
                // Here we check the Match instance.
                if (!match.Success)
                {
                    epr.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    epr.SetError(control, "Map number does not conform to naming standard. i.e. MA001");
                }
                else
                {
                    epr.SetError(control, "");
                }
            }
            else
            {
                validateEmptyField(control, epr);
            }

        }

        public static void validateMapTitle(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateMapSummary(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateDataSources(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateMapDocument(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 33);
            validateEmptyField(control, epr);
        }

        public static void validateSpatialReference(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 33);
            string automatedValue = frmMain.getSpatialReference();

            if (validateEmptyField(control, epr))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    epr.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    epr.SetError(control, "Text differs from automated value");
                }
                else
                {
                    epr.SetError(control, "");
                }
            }
            else
            {
                validateEmptyField(control, epr);
            }

        }

        public static void validateScaleText(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 33);
            string automatedValue = frmMain.updateScale();

            if (validateEmptyField(control, epr))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    epr.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    epr.SetError(control, "Text differs from automated value");
                }
                else
                {
                    epr.SetError(control, "");
                }
            }
            else
            {
                validateEmptyField(control, epr);
            }

        }

        public static void validateGlideNumber(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 33);
            string automatedValue = frmMain.getGlideNo();

            if (validateEmptyField(control, epr))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    epr.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    epr.SetError(control, "Text differs from automated value");
                }
                else
                {
                    epr.SetError(control, "");
                }
            }
            else
            {
                validateEmptyField(control, epr);
            }

        }
      
    }
}
