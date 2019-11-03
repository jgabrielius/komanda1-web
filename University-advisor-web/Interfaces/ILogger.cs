using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Interfaces
{
    public interface ILogger
    {
        public void Log(string message, string level = "INFO");
        public void LogStats(object logObject);
    }
}
