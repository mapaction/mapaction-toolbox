using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;


namespace Prototype1_ConfigTool
{
    class CreateAndUpdateXml
    {
        //private class properties 
        


        //class methods
        public static void createXML(string path, Dictionary<string,string> elemValues)
        {
            //Create and add the root element
            var xml = new XDocument();
            var rootElem = new XElement("emergency");
            xml.Add(rootElem);

            //Create Add all other elements to the 'emergency_config' element
            var opName = new XElement("operation_name", "Mongoalia");
            var opID = new XElement("operation_id", "54321");

            rootElem.Add(opName);
            rootElem.Add(opID);

            xml.Save(path + "\\operation_config.xml");

        }

        public static Dictionary<String, String> parseXML(string @filepath)
        {
            //Load the xml file 
            XDocument doc = XDocument.Load(@filepath);

            //List<string> temp = new List<string>();
            foreach (XElement i in doc.Root.Descendants())
            {
                System.Diagnostics.Debug.WriteLine("Name " + i.Name + "| Value " + i.Value);
            }
            
            //Get an array of all the elements in operation_config.xml file
            string[] xmlElements = CreateAndUpdateXml.opXmlElements();
            //Create a dictionary to store the element names with their values 
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //add the array to the dictionary and set the values to empty
            foreach (var i in xmlElements)
            {
                dict.Add(i, "");
            }

            foreach (XElement i in doc.Root.Descendants())
            {
                if (i.Name == "operation_name")
                {
                    dict["operation_name"] = i.Value;
                }
                else if (i.Name == "operation_id")
                {
                    dict["operation_id"] = i.Value;
                }
                else if (i.Name == "glide_no")
                {
                    dict["glide_no"] = i.Value;
                }

            }

            return dict;


        }

        public static string[] opXmlElements()
        {
            
            string[] xmlElements = { "operation_name", "operation_id", "glide_no"};

            return xmlElements;
            
        }




        internal static void parseXML(TextBox txtPathToExistingXml, TextBox textBox, TextBox textBox_2)
        {
            throw new NotImplementedException();
        }
    }
}
