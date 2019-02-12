using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.Framework;

namespace MapActionToolbars
{
    public static class FormValidationExport
    {
        private static string targetMapFrame = "Main map";
        private static IMxDocument _pMxDoc = ArcMap.Application.Document as IMxDocument;
        
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
                return true;
            }

        }

        //Validate individual form elements
        public static string validateMapTitle(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            string layoutMapTitle = string.Empty;
            Dictionary<string, string> dictMapValues = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, targetMapFrame);
            if (dictMapValues.ContainsKey("title")) { layoutMapTitle = dictMapValues["title"]; }
            
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != layoutMapTitle && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from page layout 'title' element value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateMapSummary(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            string layoutMapSummary = string.Empty;
            Dictionary<string, string> dictMapValues = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, targetMapFrame);
            if (dictMapValues.ContainsKey("summary")) { layoutMapSummary = dictMapValues["summary"]; }

            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != layoutMapSummary && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from page layout 'summary' element value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateMapDocument(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {

            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string mapValue = string.Empty;
            string automatedValue = MapAction.PageLayoutProperties.getMxdTitle(ArcMap.Application);

            //Get and set the map value
            var dict = new Dictionary<string, string>();
            dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, targetMapFrame);
            //Update form text boxes with values from the map
            if (dict.ContainsKey("title")) {  mapValue = dict["title"]; }

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text.Trim() != mapValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from the document title and or the 'title' element on the page layout");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
                return "Blank";
            }
        }

        public static string validateDatum(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            // Set the spatial reference information on load
            var dictSpatialRef = new Dictionary<string, string>();
            dictSpatialRef = MapAction.Utilities.getDataFrameSpatialReference(_pMxDoc, targetMapFrame);
            string automatedValue = string.Empty;
            if (dictSpatialRef.ContainsKey("datum")) { automatedValue = dictSpatialRef["datum"]; }

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateProjection(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            // Set the spatial reference information on load
            var dictSpatialRef = new Dictionary<string, string>();
            dictSpatialRef = MapAction.Utilities.getDataFrameSpatialReference(_pMxDoc, targetMapFrame);
            string automatedValue = string.Empty;
            if (dictSpatialRef.ContainsKey("projection")) { automatedValue = dictSpatialRef["projection"]; }

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateScale(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string automatedValue = MapAction.Utilities.getScale(_pMxDoc as IMapDocument, targetMapFrame);
 
            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateDate(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string automatedValue = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            
            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Date shown is not formatted to the current date & time(yyyy-MM-dd hh:mm:ss).");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateTime(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            Match match = Regex.Match(control.Text, @"\d\d:\d\d");
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);

            //Run the validation
            if (validateEmptyField(control, eprWarning))
            {
                // Here we check the regex match
                if (!match.Success)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Time doesn't comform to the format hh:mm");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
                return "Blank";
            }
            
        }

        public static string validatePaperSize(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string automatedValue = MapAction.Utilities.getPageSize(_pMxDoc as IMapDocument, targetMapFrame);

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from automated value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateImageryDate(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateDataSources(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string automatedValue = string.Empty;

            //Get and set the map value
            var dict = new Dictionary<string, string>();
            dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, targetMapFrame);
            //Update form text boxes with values from the map
            if (!dict.ContainsKey("data_sources"))
            {
                eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                eprWarning.SetError(control, "'data_sources' element not present page layout, cannot validate");
                return "warning";
            }
            else
            {
                automatedValue = dict["data_sources"];
                if (validateEmptyField(control, eprWarning))
                {
                    if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                    {
                        eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                        eprError.SetError(control, "Text differs from the 'data_sources' element on the page layout");
                        return "Error";
                    }
                    else
                    {
                        eprError.SetError(control, "");
                        return "Valid";
                    }
                }
                else
                {
                    eprError.Dispose();
                    validateEmptyField(control, eprWarning);
                    return "Blank";
                }
            }
        }

        public static string validateOperationId(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string automatedValue = string.Empty;
            
            string path = MapAction.Utilities.getCrashMoveFolderPath();
            string filePath = MapAction.Utilities.getOperationConfigFilePath();
            MapAction.OperationConfig config = MapAction.Utilities.getOperationConfigValues(filePath);
            automatedValue = config.OperationId;

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, "Text differs from the operation_config.xml value");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.SetError(control, "");
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateGlideNumber(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            eprWarning.SetIconPadding(control, 5);
            eprError.SetIconPadding(control, 5);
            string automatedValue = frmExportMain.getGlideNo();
            string mapValue = string.Empty;

            //Get and set the map value
            var dict = new Dictionary<string, string>();
            dict = MapAction.PageLayoutProperties.getLayoutTextElements(_pMxDoc, targetMapFrame);
            //Update form text boxes with values from the map
            if (dict.ContainsKey("glide_no")) {  mapValue = dict["glide_no"]; }

            if (validateEmptyField(control, eprWarning))
            {
                if (control.Text.Trim() != mapValue && control.Text != string.Empty)
                {
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control, 
                        "The value is different to the page layout.");
                    return "Error";
                }
                else if (control.Text.Trim() != automatedValue && control.Text != string.Empty)
                {
                    eprError.SetError(control, "");
                    eprError.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                    eprError.SetError(control,
                        "The page layout and the operation_config.xml value don't match. Use the Layout Tool to resolve this.");
                    return "Error";
                }
                else
                {
                    eprError.SetError(control, "");
                    return "Valid";
                }
            }
            else
            {
                eprError.Dispose();
                validateEmptyField(control, eprWarning);
                return "Blank";
            }

        }

        public static string validateLocation(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateTheme(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateCountry(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateStatus(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateAccess(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateAccessNote(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateQualityControl(Control control, ErrorProvider epr)
        {
            epr.SetIconPadding(control, 5);
            if (validateEmptyField(control, epr))
            {
                return "Valid";
            }
            else
            {
                return "Blank";
            }
        }

        public static string validateLanguage(Control control, ErrorProvider eprWarning, ErrorProvider eprError)
        {
            // Since using language_config.xml, this will always be valid.
            return "Valid";
        }

        public static void validationCheck(string result, PictureBox pbox)
        {
            if (result == "Blank")
            {
                pbox.Image = Properties.Resources.empty17px;
            }
            else if (result == "Error")
            {
                pbox.Image = Properties.Resources.cross_17px;
            }
            else if (result == "Valid")
            {
                pbox.Image = Properties.Resources.tick_17px;
            }

        }


    }

}
