using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapActionToolbar_Core
{
    public class DataFrameProperties
    {
        public string scale     { get; set;}
        public string pageSize { get; set; }
        public double xMin { get; set; }
        public double yMin { get; set; }
        public double xMax { get; set; }
        public double yMax { get; set; }
 
        public DataFrameProperties()
        {
            this.scale = null;
            this.pageSize = null;
            this.xMin = 0;
            this.yMin = 0;
            this.xMax = 0;
            this.yMax = 0;
        }

        public DataFrameProperties(string scale, string pageSize, double xMin, double yMin, double xMax, double yMax)
        {
            this.scale = scale;
            this.pageSize = pageSize;
            this.xMin = xMin;
            this.yMin = yMin;
            this.xMax = xMax;
            this.yMax = yMax;
        }
    }
}
