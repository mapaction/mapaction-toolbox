using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapActionToolbar_Core
{
    public class ProductLayer
    {
        public string name;
        public float zoomMultiplier;
        public string columnName;

        public ProductLayer(string name, float zoomMultiplier, string columnName)
        {
            this.name = name;
            this.zoomMultiplier = zoomMultiplier;
            this.columnName = columnName;
        }
    }
}
