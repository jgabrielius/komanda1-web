using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;

namespace University_advisor_web.Tools
{
    public class ErrorHandler:IErrorHandler
    {
        public void ShowError(Controller controller, string message, string title = "Error")
        {
            controller.TempData["errorTitle"] = title;
            controller.TempData["errorMessage"] = message;
        }
    }
}
