using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapAction;
using Newtonsoft.Json;

namespace MapActionToolbars
{
    public static class ShowAutomationResults
    {
        static ShowAutomationResults()
        {
            String rawJson = "{ \"result\": \"Success\", \"productName\": \"Country Overview\", \"classification\": \"Core\", \"results\": [ { \"name\": \"Settlements - Places\", \"dateStamp\": \"01-08-2019 20:45:12\", \"dataSource\": \"D:/MapAction/2019-06-25 - Automation - El Salvador/GIS/2_Active_Data/229_stle/slv_stle_stl_pt_s0_osm_pp_places.shp\", \"added\": true, \"message\": \"Layer added successfully\" },	{ \"name\": \"Transport - Airports\", \"dateStamp\": \"01-08-2019 20:45:19\", \"dataSource\": \"D:/MapAction/2019-06-25 - Automation - El Salvador/GIS/2_Active_Data/232_tran/wrl_tran_air_pt_s0_ouairports_pp_airports.shp\", \"added\": true, \"message\": \"Layer added successfully\" } ] }";

            AutomationReport resultCollection = JsonConvert.DeserializeObject<AutomationReport>(rawJson);

            Console.WriteLine(resultCollection.results.Count);

            results = resultCollection.results;
        }
        private static string result { get; set; }
        private static string productName { get; set; }
        private static string classification { get; set; }
        private static List<AutomationResult> results;

        public static List<AutomationResult> Results { get => results; }

        public static List<AutomationResult> GetResults()
        {
            return results;
        }

    }
}
