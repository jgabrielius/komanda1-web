using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.EntityFramework
{
    public class Log
    {
        public int LogId { get; set; }
        public string Query { get; set; }

        public string Date { get; set; }
    }
}
