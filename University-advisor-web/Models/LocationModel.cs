using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class LocationModel
    {
        public string Place_id { get; set; }
        public string Licence { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Display_name { get; set; }
        public string[] Boundingbox { get; set; }
        public float Importance { get; set; }
    }
}
