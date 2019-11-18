using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Interfaces
{
    public interface IErrorHandler
    {
        public void ShowError(Controller controller, string message, string title = "Error");
    }
}
