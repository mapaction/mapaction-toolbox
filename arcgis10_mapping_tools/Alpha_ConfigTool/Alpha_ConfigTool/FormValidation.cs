using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Alpha_ConfigTool
{
    public static class FormValidation
    {
        //Clear error providers (primarily on form close)
        public static void disposeErrorProvider(ErrorProvider epr)
        {
            epr.Dispose();
        }

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
                //epr.Clear();
                return true;
            }

        }

        //Validate individual form elements for blank values
        public static void validateOperationName(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateLanguage(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateCountry(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateOperationID(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateOrganisation(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateUrl(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateEmail(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateDisclaimer(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateDonor(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 3);
            validateEmptyField(control, epr);
        }

        public static void validateExportPath(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 40);
            validateEmptyField(control, epr);
            
            if (!Directory.Exists(@control.Text))
            {
                epr.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                epr.SetError(control, "Export Path is not valid.");
            }
        }

        //Validate individual form elements for blank values and regular expressions
        public static void validateGlideNumber(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            string varRegex = @"^((CW)|(CE)|(DR)|(EQ)|(EP)|(EC)|(ET)|(FA)|(FR)|(FF)|(FL)|(HT)|(IN)|(LS)|(MS)|(OT)|(ST)|(SL)|(AV)|(SS)|(AC)|(TO)|(TC)|(TS)|(VW)|(VO)|(WV)|(WF))-20\d{2}-\d{6}-[A-Z]{3}$";
            Match match = Regex.Match(control.Text, @varRegex);
            eprWarning.SetIconPadding(control, 3);
            eprError.SetIconPadding(control, 3);

            //Run the validation
            if (validateEmptyField(control, eprWarning))
            {
                // Here we check the regex match
                if (!match.Success)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Map number does not conform to naming standard. i.e. RC-2013-0715-TD");
                }
                else
                {
                    eprError.SetError(control, "");
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
            }

        }

        public static void validateTimezone(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            string varRegex = @"^[A-Za-z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$";
            Match match = Regex.Match(control.Text, @varRegex);
            eprWarning.SetIconPadding(control, 3);
            eprError.SetIconPadding(control, 3);

            //Run the validation
            if (validateEmptyField(control, eprWarning))
            {
                // Here we check the regex match
                if (!match.Success)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Map number does not conform to naming standard. i.e. MA001");
                }
                else
                {
                    eprError.SetError(control, "");
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
            }

        }

        public static void validatePrimaryEmail(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            string varRegex = @"^[A-Za-z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$";
            Match match = Regex.Match(control.Text, @varRegex);
            eprWarning.SetIconPadding(control, 3);
            eprError.SetIconPadding(control, 3);

            //Run the validation
            if (validateEmptyField(control, eprWarning))
            {
                // Here we check the regex match
                if (!match.Success)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Map number does not conform to naming standard. i.e. MA001");
                }
                else
                {
                    eprError.SetError(control, "");
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
            }

        }
 
    }
}
