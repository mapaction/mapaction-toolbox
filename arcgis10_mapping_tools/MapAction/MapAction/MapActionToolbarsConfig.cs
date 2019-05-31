using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace MapAction
{
    [Serializable]
    [XmlRoot("MapActionToolbarConfig")]
    public class MapActionToolbarConfig
    {
        [XmlArray("Tools"), XmlArrayItem(typeof(Tool), ElementName = "Tool")]
        public List<Tool> Tools { get; set; }

        public MapActionToolbarConfig()
        {
            Tools = new List<Tool>();
        }

        public List<string> Themes()
        {
            List<string> themes = new List<string>();

            var exportTool = Tools.Find(i => i.ToolName == Tool.ExportToolName);

            var themeComponent = exportTool.Components.Find(i => i.ComponentName == Component.ThemeComponentName);

            foreach (CheckBoxItem checkBoxItem in themeComponent.CheckBoxItems)
            {
                themes.Add(checkBoxItem.CheckBoxItemName);
            }
            return themes;
        }

        public List<string> MapRootURLs()
        {
            List<string> mapRootURLs = new List<string>();

            var exportTool = Tools.Find(i => i.ToolName == Tool.OperationConfigToolName);

            var mapRootURLComponent = exportTool.Components.Find(i => i.ComponentName == Component.MapRootUrlComponentName);

            if (mapRootURLComponent != null)
            {
                foreach (ComboBoxItem comboBoxItem in mapRootURLComponent.ComboBoxItems)
                {
                    mapRootURLs.Add(comboBoxItem.ComboBoxItemValue);
                }
            }
            return mapRootURLs;
        }

        public string TextBoxItem(string toolName, string componentName)
        {
            string textBoxItemValue = "";

            var tool = Tools.Find(i => i.ToolName == toolName);
            if (tool != null)
            {
                var component = tool.Components.Find(i => i.ComponentName == componentName);
                if (component != null)
                {
                    if (component.TextBoxItem != null)
                    {
                        textBoxItemValue = component.TextBoxItem.TextBoxItemValue;
                    }
                }
            }
            return textBoxItemValue;
        }
    }

    public class Tool
    {
        public static readonly string ExportToolName = "Export Tool";
        public static readonly string OperationConfigToolName = "Operation Config Tool";

        public string ToolName { get; set; }
        public List<Component> Components { get; set; }

        public Tool()
        {
            Components = new List<Component>();
        }
    }

    public class Component
    {
        public static readonly string ThemeComponentName = "Themes";
        public static readonly string OrganisationUrlComponentName = "Organisation Url";
        public static readonly string MapRootUrlComponentName = "Map Root Url";
        public string ComponentName;
        public List<CheckBoxItem> CheckBoxItems;
        public List<ComboBoxItem> ComboBoxItems;
        public TextBoxItem TextBoxItem; 

        public Component()
        {
            CheckBoxItems = new List<CheckBoxItem>();
            ComboBoxItems = new List<ComboBoxItem>();
        }
    }

    public class CheckBoxItem
    {
        public string CheckBoxItemName;
    }

    public class ComboBoxItem
    {
        public string ComboBoxItemValue;
    }

    public class TextBoxItem
    {
        public string TextBoxItemValue;
    }
}
