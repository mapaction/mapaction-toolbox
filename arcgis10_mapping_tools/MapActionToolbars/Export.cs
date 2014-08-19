using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;


namespace MapActionToolbars
{
    class Export
    {

        public static void openExplorerDirectory(string path)
        {
            try
            {
                if (Directory.Exists(@path))
                {
                    Process.Start("explorer.exe", @path);
                }
                else
                {
                    MessageBox.Show("Path doesn't exist", "Invalid path");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Cannot open export folder in explorer");
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

        }


    }
}
