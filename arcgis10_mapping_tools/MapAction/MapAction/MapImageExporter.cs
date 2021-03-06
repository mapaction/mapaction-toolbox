﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMap;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Desktop;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;

namespace MapAction
{
    /// <summary>
    /// This is a non-static type for exporting map images such as (but not limited to) within the Mapaction Export Tool.
    /// It's replacing some of the functionality of the static MapExport class.
    /// </summary>
    public class MapImageExporter
    {
        public const int SCREEN_RES_DPI = 96;
        private IMapDocument m_MapDoc;
        private string m_ExportBaseFileName;
        private string m_ExportDir;
        private IActiveView m_ViewToExport;
        private bool m_CanExport;
        private bool m_IsLayoutExport;

        /// <summary>
        /// Constructs a MapFrameExporter object, which can export images in any available type and size 
        /// from a single map document and layout or dataframe. Use one of these for each layout or 
        /// dataframe that is to be exported.
        /// </summary>
        /// <param name="pMapDoc"></param>
        /// <param name="outputFileBaseName">The output filename including the directory and base-name from the UI form</param>
        /// <param name="mapFrameName">The name of the dataframe to export; use null to export the layout</param>
        public MapImageExporter(IMapDocument pMapDoc, string outputFileBaseName, string mapFrameName)
        {
            m_MapDoc = pMapDoc;

            m_ExportDir = System.IO.Path.GetDirectoryName(outputFileBaseName);
            m_ExportBaseFileName = System.IO.Path.GetFileName(outputFileBaseName);
            m_CanExport = true;
            m_IsLayoutExport = false;

            if (mapFrameName == null)
            {
                m_ViewToExport = pMapDoc.ActiveView;
                // TODO we should use 
                // m_ViewToExport = (IActiveView)pMapDoc.PageLayout;
                // so that the user doesn't need to switch to layout view before running tool
                m_IsLayoutExport = true;
            }
            else if (PageLayoutProperties.detectMapFrame(pMapDoc, mapFrameName))
            {
                for (int i = 0; i <= pMapDoc.MapCount - 1; i++)
                {
                    if (pMapDoc.Map[i].Name == mapFrameName)
                    {
                        m_ViewToExport = pMapDoc.Map[i] as IActiveView;
                    }
                }
            }
            else
            {
                // we've requested export of a mapframe that can't be found / doesn't exist
                m_CanExport = false;
            }
        }

        /// <summary>
        /// build the fully qualified output filename according to the MA export conventions, 
        /// accounting for the different naming of the thumbnail file. 
        /// Also accounts for DDP / mapbook export types, where any dots 
        /// within the base filename need to be removed otherwise the GP tool will only use the part 
        /// before the first dot. Returns a glob pattern in the case of multi-file mapbook export.
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="dpi"></param>
        /// <returns></returns>
        private string GetExportFilename(MapActionExportTypes exportType, UInt16 dpi)
        {
            string fileExt;
            if (exportType == MapActionExportTypes.png_thumbnail_zip)
            {
                fileExt = "png";
                return System.IO.Path.Combine(m_ExportDir, "thumbnail.png");
            }
            else if (exportType == MapActionExportTypes.png_thumbnail_local)
            {
                fileExt = "png";
                return System.IO.Path.Combine(m_ExportDir,
                    m_ExportBaseFileName + "-thumbnail." + fileExt);
            }
            else if (exportType == MapActionExportTypes.pdf_ddp_multifile)
            {
                // return a glob pattern which will match all files exported. NB the export code can't use 
                // this to feed the gp tool directly: use pdf_ddp_singlefile for that for either ddp type
                fileExt = "pdf";
                return System.IO.Path.Combine(m_ExportDir,
                    m_ExportBaseFileName.Replace(".", "_") + "-mapbook_*." + fileExt);
            }
            else if (exportType == MapActionExportTypes.pdf_non_ddp || exportType == MapActionExportTypes.pdf_ddp_singlefile)
            {
                fileExt = "pdf";
            }
            else
            {
                fileExt = exportType.ToString(); // all other enum values can be directly tostringed to get the file ext they represent
            }

            string fullFileName;
            if (exportType == MapActionExportTypes.pdf_ddp_singlefile)
            { 
                fullFileName = m_ExportBaseFileName.Replace(".", "_") + "-mapbook." + fileExt;
            }
            else if (m_IsLayoutExport)
            {
                fullFileName = m_ExportBaseFileName + "-" + dpi.ToString() + "dpi." + fileExt;
            }
            else
            {
                fullFileName = m_ExportBaseFileName + "-mapframe-" + dpi.ToString() + "dpi." + fileExt;
            }
            return System.IO.Path.Combine(m_ExportDir, fullFileName);
        }

        /// <summary>
        /// Get the output image dimensions in pixels based on the aspect of the frame or layout, and the 
        /// requested dpi for exporting it at
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="outDPI"></param>
        /// <returns></returns>
        private XYDimensions GetUnconstrainedPixelSizeXY(UInt16 outDPI)
        {
            UInt32 width = (UInt32)(m_ViewToExport.ExportFrame.right * (outDPI / SCREEN_RES_DPI));
            UInt32 height = (UInt32)(m_ViewToExport.ExportFrame.bottom * (outDPI / SCREEN_RES_DPI));
            return new XYDimensions() { Height = height, Width = width };
        }

        /// <summary>
        /// Get the output image dimensions in pixels (representd as a tagRECT), based on the actual aspect 
        /// of the frame or layout and the requested dpi.
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="outDPI"></param>
        /// <returns></returns>
        private tagRECT GetExportTagRect(MapActionExportTypes exportType, UInt16 outDPI)
        {
            return GetExportTagRect(GetUnconstrainedPixelSizeXY(outDPI));
        }

        /// <summary>
        /// Get the output dimensions in pixels (represented as a tagRECT), constrained to a maximum size in X and/or Y.
        /// 
        /// Neither output dimension will exceed the corresponding dimension of the passed maxSize, if set. If the passed 
        /// maxSize is of a different aspect to the exported frame/layout, then the other dimension exported will be 
        /// appropriately reduced so that the output is of the same aspect ratio as the actual map.
        ///
        /// - If the mapframe is portrait, then the output will be maxSize.height high and the width will be 
        /// appropriately set from this, UNLESS that results in a width greater than maxSixe.width if that is set
        /// (i.e. if the input constraint is even narrower than the map), in which case the width constraint will be 
        /// used instead
        /// - If the mapframe is landscape, then the output will be maxSize.width wide and the height will be 
        /// appropriately set from this, UNLESS that results in a height greater than maxSize.height if that is set
        /// (i.e. if the input constraint is even less tall than the map), in which case the height constraint will be 
        /// used instead
        /// 
        /// To produce an output constrained in only one dimension, set the other dimension of the XYDimensions to 
        /// null; the unconstrained dimension will then be freely adjusted to match the map aspect.
        /// </summary>
        /// <param name="maxSize">XYDimensions object specifying the width and/or height that the output image must fit within</param>
        /// <returns></returns>
        private tagRECT GetExportTagRect(XYDimensions maxSize)
        {
            tagRECT exportRECT;
            exportRECT.left = 0;
            exportRECT.top = 0;

            int frameWidth = m_ViewToExport.ExportFrame.right;
            int frameHeight = m_ViewToExport.ExportFrame.bottom;
            // note that if this is being called from the dpi-based override (standard calling) then the 
            // aspect calculation will already be reflected in the maxSize parameter and is thus redundant 
            // here. 
            // Calculate the actual aspect of what we're exporting
            double mapAspect = (double)frameWidth / (double)frameHeight;

            if (mapAspect >= 1.0)
            {
                // landscape or square aspect ratio. Constrain to specified width unless this results in height being too great
                if (maxSize.Width != null)
                {
                    // Output to the specified width and set height to maintain aspect k
                    exportRECT.right = (int)maxSize.Width;
                    int outHeight = (int)(maxSize.Width / mapAspect);
                    if (maxSize.Height != null && outHeight > maxSize.Height)
                    {
                        // map is landscape but propotions are such that constraining to width would result in height being too great
                        // Recalculate based on the height as that is actually our limiting factor for this map.
                        XYDimensions newLimits = new XYDimensions() { Height = maxSize.Height, Width = null };
                        tagRECT updatedRect = GetExportTagRect(newLimits);
                        return updatedRect;
                    }
                    exportRECT.bottom = outHeight;
                }
                else if (maxSize.Height != null)
                {
                    // Only a height has been specified even though it's a landscape map. 
                    // Output to the specified height and set the width to maintain aspect
                    exportRECT.bottom = (int)maxSize.Height;
                    int outWidth = (int)(maxSize.Height * mapAspect);
                    // width is null i.e. unconstrained
                    exportRECT.right = outWidth;
                }
                else
                {
                    throw new ArgumentException("Either width or height must be specified in order to constrain output size");
                }
            }
            else
            {
                // portrait aspect ratio. Constrain to the specified height, and reduce width
                if (maxSize.Height != null)
                {
                    exportRECT.bottom = (int)maxSize.Height;
                    int outWidth = (int)(maxSize.Height * mapAspect);
                    if (maxSize.Width != null && outWidth > maxSize.Width)
                    {
                        // map is portrait but proportions are such that constraining to height would result in width being too great
                        // Recalculate based on the width as that is actually our limiting factor for this map.
                        XYDimensions newLimits = new XYDimensions() { Height = null, Width = maxSize.Width };
                        tagRECT updatedRect = GetExportTagRect(newLimits);
                        return updatedRect;
                    }
                    exportRECT.right = outWidth;
                }
                else if (maxSize.Width != null)
                {
                    exportRECT.right = (int)maxSize.Width;
                    int outHeight = (int)(maxSize.Width / mapAspect);
                    exportRECT.bottom = outHeight;
                }
                else
                {
                    throw new ArgumentException("Either width or height must be specified in order to constrain output size");
                }
            }
            return exportRECT;
        }

        /// <summary>
        /// Runs the export, which should have already been set up
        /// </summary>
        /// <param name="docExport"></param>
        /// <param name="expSize"></param>
        /// <returns></returns>
        private bool RunExport(IExport docExport, tagRECT expSize)
        {
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords(expSize.left, expSize.top, expSize.right, expSize.bottom);
            docExport.PixelBounds = envelope;

            try
            {
                System.Int32 hDC = docExport.StartExporting();
                m_ViewToExport.Output(hDC, (System.Int16)docExport.Resolution, ref expSize, null, null); // Explicit Cast and 'ref' keyword needed 
                docExport.FinishExporting();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error writing to file, probably a file permissions error.  Check the exception message below for details.");
                Debug.WriteLine(e.Message);
                return false;
            }
            finally
            {
                docExport.Cleanup();
            }
            return true;
        }

        /// <summary>
        /// Maps the enum of available export types onto the appropriate exporter class, and returns an 
        /// instance of whatevre this is as an IExport
        /// </summary>
        /// <param name="exportType"></param>
        /// <returns></returns>
        private IExport InitializeExporter(MapActionExportTypes exportType)
        {
            IExport docExport;

            // The Export*Class() type initializes a new export class of the desired type.
            if (exportType == MapActionExportTypes.pdf_non_ddp)
            { // Set PDF Export options
                docExport = new ExportPDF() as IExport;
                IExportPDF iPDF_export = (IExportPDF)docExport;
                iPDF_export.EmbedFonts = true;
                docExport = (IExport)iPDF_export;
            }
            else if (exportType == MapActionExportTypes.eps)
            {
                docExport = new ExportPS() as IExport;
            }
            else if (exportType == MapActionExportTypes.ai)
            {
                docExport = new ExportAI() as IExport;
            }
            else if (exportType == MapActionExportTypes.bmp)
            {
                docExport = new ExportBMP() as IExport;
            }
            else if (exportType == MapActionExportTypes.tiff)
            {
                docExport = new ExportTIFF() as IExport;
            }
            else if (exportType == MapActionExportTypes.svg)
            {
                docExport = new ExportSVG() as IExport;
            }
            else if (exportType == MapActionExportTypes.png)
            {
                docExport = new ExportPNG() as IExport;
            }
            else if (exportType == MapActionExportTypes.gif)
            {
                docExport = new ExportGIF() as IExport;
            }
            else if (exportType == MapActionExportTypes.emf)
            {
                docExport = new ExportEMF() as IExport;
            }
            else if (exportType == MapActionExportTypes.jpeg)
            {
                IExportJPEG m_export;
                docExport = new ExportJPEG() as IExport;
                if (docExport is IExportJPEG)
                {
                    m_export = (IExportJPEG)docExport;
                    m_export.ProgressiveMode = false;   //hardcoded progressive mode value here
                    m_export.Quality = 80;              //hardcoded quality value here
                    docExport = (IExport)m_export;
                }
            }
            else if (
                exportType == MapActionExportTypes.png_thumbnail_zip 
                || exportType == MapActionExportTypes.png_thumbnail_local)
            {
                docExport = new ExportPNG() as IExport;
            }
            else if (exportType == MapActionExportTypes.kmz)
            {
                // TODO - refactor to call the kmz exporter here
                throw new NotImplementedException("kmz to do");
            }
            else
            {
                // We can't get here unless the enum is modified or we've called this for one of the ddp export types
                throw new ArgumentException("Unexpected export type requested", "exportType");
                //return pathFileName;
            }
            return docExport;
        }

        /// <summary>
        /// Export a map image by specifying the size in pixels that the image should not exceed, in width and/or height.
        /// For example thumbnails, or other situations where the image needs to be of a specific size such as for website uses.
        /// Images below a certain size (currently 200 pixels) will be exported to a larger file which is then resampled down to 
        /// the requested size, due to the coarse appearance of exports from ArcMap at low resolutions.
        /// </summary>
        /// <param name="exportType">Export file type</param>
        /// <param name="maxSize">XYDimensions object specifying the width and/or height that the output image must fit within. 
        /// At least one of Height or Width must be set. The other may be null to constrain size only in one dimension.</param>
        /// <returns>string representing the path of the output file, or null if an error occurred.</returns>
        public string exportImage(MapActionExportTypes exportType, XYDimensions maxSize)
        {
            if (!m_CanExport)
            {
                return null;
            }

            if (exportType == MapActionExportTypes.pdf_ddp_singlefile || exportType == MapActionExportTypes.pdf_ddp_multifile)
            {
                return exportDataDrivenPagesImages(exportType);
            }

            // TODO we should probably change the filename to reflect a pixel size rather than a dpi - need 
            // to check what downstream things rely on the filename though
            string outFileName = GetExportFilename(exportType, SCREEN_RES_DPI);

            IExport docExport = InitializeExporter(exportType);
 
            if (maxSize.MaxDim < 200)
            {
                // If we export very small images (such as a thumbnail) from Arcmap, then they don't look great
                // as arc tries to make sure everything gets drawn regardless of size so the gridlines and text look
                // really black. 
                // In this case let's export to a much larger temporary image and then resize that using system image 
                // manipulation library.
                var fn = System.IO.Path.GetFileName(outFileName);
                var tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "__TMP__" + fn);
                docExport.ExportFileName = tempPath;
                maxSize.Height *= 10;
                maxSize.Width *= 10;
                tagRECT outDims = GetExportTagRect(maxSize);
                bool result = RunExport(docExport, outDims);
                if (result)
                {
                    Utilities.ResizeImageFile(tempPath, outFileName, 0.1);
                    System.IO.File.Delete(tempPath);
                    return outFileName;
                }
            }
            else
            {
                docExport.ExportFileName = outFileName;
                tagRECT outDims = GetExportTagRect(maxSize);
                bool result = RunExport(docExport, outDims);
                if (result)
                {
                    return outFileName;
                }
            }

            return null;
        }

        /// <summary>
        /// Export a map image by specifying resolution for the export in DPI. The image size will 
        /// vary according to size of the layout / dataframe exported. This is the normal method used by 
        /// the export tool.
        /// </summary>
        /// <param name="exportType">The file format to export</param>
        /// <param name="dpi">The resolution in DPI (more relevant for layout exports)</param>
        /// <returns>string representing the path of the output file, or null if an error occurred.</returns>
        public string exportImage(MapActionExportTypes exportType, UInt16 dpi)
        {
            if (!m_CanExport)
            {
                return null;
            }

            if (exportType == MapActionExportTypes.pdf_ddp_singlefile || exportType == MapActionExportTypes.pdf_ddp_multifile)
            {
                return exportDataDrivenPagesImages(exportType);
            }

            string outFileName = GetExportFilename(exportType, dpi);
            IExport docExport = InitializeExporter(exportType);
            docExport.ExportFileName = outFileName;
            docExport.Resolution = dpi;

            tagRECT outDims = GetExportTagRect(exportType, dpi);
            bool result = RunExport(docExport, outDims);
            if (result)
            {
                return outFileName;
            }
            return null;
        }

        /// <summary>
        /// allows override of the export filename, for use only in testing of e.g. multiple export sizes at the same time
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="maxSize"></param>
        /// <param name="fnOverRide"></param>
        /// <returns></returns>
        internal string testExportImage(MapActionExportTypes exportType, XYDimensions maxSize, string fnOverRide)
        {
            m_ExportBaseFileName = fnOverRide;
            if (!m_CanExport)
            {
                return null;
            }
            string outFileName = GetExportFilename(exportType, SCREEN_RES_DPI);

            IExport docExport = InitializeExporter(exportType);
            docExport.ExportFileName = outFileName;
            // docExport.resolution  = 96

            tagRECT outDims = GetExportTagRect(maxSize);
            bool result = RunExport(docExport, outDims);
            if (result)
            {
                return outFileName;
            }
            return null;
        }

        public string exportDataDrivenPagesImages(MapActionExportTypes exportType)
        {
            // If not DataDrivenPages then call exportImage as normal
            if (!PageLayoutProperties.isDataDrivenPagesEnabled(m_MapDoc))
            {
                return exportImage(MapActionExportTypes.pdf_non_ddp, 300);
            }
            else if  (!(exportType == MapActionExportTypes.pdf_ddp_singlefile || exportType == MapActionExportTypes.pdf_ddp_multifile))
            {
                throw new ArgumentException("Called exportDataDrivenPages with a non-ddp exportType");
            }
            else
            {
                string multiplePageParameter;
                // regardless of single/multiple file mode, pass the basic filename and folder to the GP export tool
                string filenameForExporter = GetExportFilename(MapActionExportTypes.pdf_ddp_singlefile, 300);
                // in the case of multiple files export, we return a glob pattern and with a single file export 
                // we return the actual filename as normal
                string filenameForReturn;
                if (exportType == MapActionExportTypes.pdf_ddp_multifile)
                {
                    multiplePageParameter = "PDF_MULTIPLE_FILES_PAGE_NAME";
                    // nb the python code does not specify resolution for ddp export so it defaults to 300dpi.
                    // No reason we couldn't pass that parameter down to it as well if we want to in future.
                    // DPI parameter isn't used by the filename generator in this case anyway
                    filenameForReturn = GetExportFilename(exportType, 300);
                    // In the case of a multi-page export, the tool might create different files from a previous run if the 
                    // user has changed the DDP driving data since then. These would all then be wrapped up into the 
                    // zip because they'd have the same stub and there's no other way to tell them apart from new ones.
                    // So, delete them first.
                    var pre_existing_multifiles = Directory.GetFiles(System.IO.Path.GetDirectoryName(filenameForReturn), 
                        System.IO.Path.GetFileName(filenameForReturn));
                    foreach (var item in  pre_existing_multifiles)
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch (IOException ex)
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    multiplePageParameter = "PDF_SINGLE_FILE";
                    filenameForReturn = filenameForExporter;
                }
                IGeoProcessor2 gp = new GeoProcessor() as IGeoProcessor2;
                gp.AddToolbox(Utilities.getExportGPToolboxPath());
                IVariantArray parameters = new VarArray();

                //parameters.Add(this.m_MapDoc.DocumentFilename);
                // HSG: the python script was already set to use current document if first parameter is None, however it's unclear how 
                // to pass None from C# (null doesn't work, empty string doesn't work; or maybe it did but the problem was the parameter 
                // being "Required" in the .tbx. Anyway.). 
                // Therefore python has been changed to use current document if first parameter is None, empty string, or "#", and the 
                // binary toolbox file has been changed to make this parameter optional.
                // So now we pass "#" to make export use document as it is currently rather than the last-saved MXD on disk
                // Other than that, the ordered params are MXD filename
                parameters.Add(null);
                parameters.Add(System.IO.Path.GetDirectoryName(filenameForExporter));
                parameters.Add(System.IO.Path.GetFileName(filenameForExporter));
                parameters.Add(multiplePageParameter);

                // DONE ~~TODO: Deal with having to save doc. Just use current document in tool by default? Make MXD optional parameter?~~ 
                // Execute the tool.  
                object sev = null;
                IGeoProcessorResult2 dpp_export_result = null;
                 
                try
                {
                    dpp_export_result = (IGeoProcessorResult2)gp.Execute("exportMapbook", parameters, null);
                    Console.WriteLine(gp.GetMessages(ref sev));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    string errorMsgs = gp.GetMessages(ref sev);
                    Console.WriteLine(errorMsgs);
                    return null;
                    // HSG: don't throw, that kills arcmap. Explain! Or better (done), handle the situation
                    //throw;
                }

                if (dpp_export_result == null)
                {
                    String gp_error_messages = gp.GetMessages(2);
                    Console.WriteLine("Export result was null, external GP code failed. Messages were: ");
                    Console.WriteLine(gp_error_messages);
                    return null;
                    //throw new Exception(gp_error_messages);
                }
                else
                {
                    String gp_messages = dpp_export_result.GetMessages(0);
                    // dictImageFileSizes["pdf"] = long.Parse(dpp_export_result.GetOutput(1).GetAsText()); // Outputs Zero indexed on number of results - not number of params.
                    //TODO: Page Count
                    System.Console.WriteLine(gp_messages);
                }
                var outputs = Directory.GetFiles(System.IO.Path.GetDirectoryName(filenameForReturn), System.IO.Path.GetFileName(filenameForReturn));
                if (outputs.Length > 0)
                {
                    return filenameForReturn;
                }
                else
                {
                    return null;
                }
                //dictFilePaths = new Dictionary<string, string>();
                //dictFilePaths["pdf"] = exportPathFileName + ".pdf";
            }
        }
    }

}
