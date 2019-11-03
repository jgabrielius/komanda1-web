using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class LogHeaderModel
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
    }
    public class TemporaryHeader
    {
        public LogHeaderModel index { get; set; }
    }
}
