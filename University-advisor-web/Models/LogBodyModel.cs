using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class LogBodyModel
    {
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
    }
}
