using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype1_ExportTool
{
    public static class validation
    {
        public static List<string> getEmptyRequiredFieldList(Dictionary<string, string> dict)
        {
            //create a list of the required fields
            List<string> lst = new List<string> {"operationID","sourceorg","title","ref","language","countries",
                "createdate","createtime","status","datum","qclevel","qcname","access"};

            //create a list to add the empty fields too
            List<string> lstEmptyFields = new List<string>();

            foreach (var i in lst)
            {
                foreach (var x in dict)
                {
                    if (i == x.Key)
                    {
                        if (x.Value == string.Empty || x.Value == "")
                        {
                            lstEmptyFields.Add(x.Key);
                        }
                    }
                }

            }
            //return the list with a record for each empty field
            return lstEmptyFields;
        }

        #region Public method validateBlankXmlValues
        //Takes a dictionary of form elements and their values
        //returns a dictionary with the form element 
        public static int validateBlankXmlValues(Dictionary<string, string> dict)
        {

            int counter = 0;
            foreach (var i in dict)
            {
                if (i.Value == "")
                {
                    counter++;
                }
            }
            return counter;
        }
        #endregion

    }
}
