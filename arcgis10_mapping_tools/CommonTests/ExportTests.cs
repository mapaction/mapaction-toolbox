using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using NUnit.Framework;
using MapAction;


namespace MapAction.tests
{
     [TestFixture]
    public class Export
    {
        // Class properties 
        protected string exportPath;
        protected string documentName;
        protected IMxDocument pMxDoc; // Map document 


        public Export()
        {
            // This seems to run but the instance isn't use by thetest method? 

            // Default constructor.
            this.exportPath = @"C:\Users\andrew\Documents\";  // ConfigurationManager.AppSettings["exportPath"];
            this.documentName = @"C:\users\andrew\documents\sample_map.mxd";// ConfigurationManager.AppSettings["mapDocument"];

            //Add runtime binding prior to any ArcObjects code in the static void Main() method.
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);

            this.pMxDoc = this.getMxd(this.documentName); // Open map document

        }

         /// <summary>
         /// Runs at end of tests to close ArcMap and free up resources. 
         /// </summary>
        [TestFixtureTearDown]
        public void closeArcMap()
        {
            MxDocument mxDoc = (MxDocument)this.pMxDoc;
            mxDoc.Parent.Shutdown();  
            // ^ Is this the best way to do it, or does the runtime manager provide a method?
        }

        [Test]
        public void exportImageCreatesFileTest()
        {
            this.exportPath = @"C:\Users\andrew\Documents\";  // ConfigurationManager.AppSettings["exportPath"];
            this.documentName = @"C:\users\andrew\documents\sample_map.mxd";// ConfigurationManager.AppSettings["mapDocument"];            

            Console.WriteLine("Settings2 :Export Path {0},Map Document {1}", this.exportPath, this.documentName);

            string fileType = "pdf";
            string dpi = "300";

            // Exported file name is dynamically generated : 
            // pathFileName = @pathDocumentName + "-" mapframe + "-" + dpi.ToString() + "dpi." + exportType; 
            string exportFileName = String.Format("{0}-{1}-{2}dpi.{3}", this.documentName, "mapframe", dpi, fileType);
            Console.WriteLine("Map Title:\t{0}\nExport Filename:\t{1}", ((MxDocument)this.pMxDoc).Title,exportFileName);

            // Test export file not present already 
            Assert.IsFalse(System.IO.File.Exists(exportFileName));
            // Test            
            exportFileName = MapExport.exportImage(this.pMxDoc, fileType, dpi, this.documentName, "Main map");

            // Assert file exported. 
            Assert.IsTrue(System.IO.File.Exists(exportFileName));

            // TODO - Check file exported is valid image of the type requested. 

        }



        private IMxDocument getMxd(string mxdPath)
        {
            Console.WriteLine(mxdPath);

            MxDocument _pMxDoc = new MxDocumentClass();
            _pMxDoc.Parent.OpenDocument(mxdPath);
            Console.WriteLine(_pMxDoc.Title);
            return (IMxDocument)_pMxDoc;
        }

    }
}
