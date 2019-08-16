using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAction
{
    public class AutomationReport
    {
        public string result { get; set; }
        public string summary { get; set; }
        public string productName { get; set; }
        public string classfication { get; set; }
        public List<AutomationResult> results { get; set; }
    }
}
