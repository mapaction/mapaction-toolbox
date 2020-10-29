using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MapAction
{
    /// <summary>
    /// The different types that can be exported by the MapImageExporter
    /// </summary>
    public enum MapActionExportTypes
    {
        pdf_non_ddp,
        eps,
        ai,
        bmp,
        tiff,
        svg,
        png,
        gif,
        emf,
        jpeg,
        png_thumbnail_zip,
        png_thumbnail_local,
        kmz,
        pdf_ddp_singlefile,
        pdf_ddp_multifile
    }

    /// <summary>
    /// Holds a pair of values to represent width and height of an image. There is probably something 
    /// already existing for this... but no tuples at .net 3.5...
    /// </summary>
    public struct XYDimensions
    {
        public UInt32? Width;
        public UInt32? Height;
        public UInt32 MaxDim
        {
            get
            {
                if (Width.HasValue && Height.HasValue)
                {
                    return Math.Max(Width.Value, Height.Value);
                }
                else if (Width.HasValue) { return Width.Value; }
                else if (Height.HasValue) { return Height.Value; }
                else { return 0; }
            }
        }
    }
    
}
