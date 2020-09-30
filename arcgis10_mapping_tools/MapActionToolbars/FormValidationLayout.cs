using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace MapActionToolbars
{
    public static class FormValidationLayout
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

        //Validate individual form elements for tab 1
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

        public static void validateMapDocument(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {

            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue = MapAction.PageLayoutProperties.getMxdTitle(ArcMap.Application);

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
                }
                else
                {
                    eprError.SetError(control, "");
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
            }
        }

        public static void validateMapNumber(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            Match match = Regex.Match(control.Text, @"MA\d\d\d");
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

        public static void validateSpatialReference(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue = frmLayoutMain.getSpatialReference();

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
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

        
        public static void validateGlideNumber(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue =  LayoutToolAutomatedValues.getGlideNo();

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
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

        //Validate individual form elements for tab 2
        public static void validateDisclaimer(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue = LayoutToolAutomatedValues.getConfigDisclaimer();

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
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

        public static void validateDonorCredit(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue = LayoutToolAutomatedValues.getConfigDonorText();

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
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

        public static void validateMapProducer(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue = LayoutToolAutomatedValues.getProducedByText();

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
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
            eprWarning.SetIconPadding(control, 33);
            eprError.SetIconPadding(control, 33);
            string automatedValue = LayoutToolAutomatedValues.getConfigTimezone();

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
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
