﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RenameLayer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Dsn=MS Access Database;dbq=C:\\SoftwareDevelopment\\desktop\\mapaction\\layer_rename_" +
            "tool\\layer_name_lookup.accdb;defaultdir=C:\\SoftwareDevelopment\\desktop\\mapaction" +
            "\\layer_rename_tool;driverid=25;fil=MS Access;maxbuffersize=2048;pagetimeout=5;ui" +
            "d=admin")]
        public string ODBC_lookup_ConnectionString {
            get {
                return ((string)(this["ODBC_lookup_ConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\Resources\\layer_nam" +
            "e_lookup.accdb;Persist Security Info=True")]
        public string layer_name_lookupConnectionString {
            get {
                return ((string)(this["layer_name_lookupConnectionString"]));
            }
        }
    }
}
