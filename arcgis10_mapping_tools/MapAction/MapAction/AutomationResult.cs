using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAction
{
    public class AutomationResult
    {
        public string layerName { get; set; }
        public string dateStamp { get; set; }
        public string dataSource { get; set; }
        public bool added { get; set; }
        public string message { get; set; }
    }
}
