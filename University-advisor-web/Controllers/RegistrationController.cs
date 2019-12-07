using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_advisor_web.Constants;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;
using University_advisor_web.Tools;

namespace University_advisor_web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRegistrationService _registration;
        private readonly IErrorHandler _errorHandler;

        public RegistrationController(ILogger logger, 
                                      IRegistrationService registration,
                                      IErrorHandler errorHandler)
        {
            _logger = logger;
            _registration = registration;
            _errorHandler = errorHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationFormModel(_registration.GetAllUniversities(), _registration.GetAllCourses());
            //return View(model);
            return View("../Pages/SignUp/Index");
        }
       
        [HttpPost]
        public IActionResult SignUp(RegistrationFormModel model)
        {
            if (_registration.AddUser(model.User))
            {
                _logger.Log(Messages.userRegistered);
                _logger.LogStats(model.User);
                return View("../Home/Index");
            }
            else
            {
                _errorHandler.ShowError(this, Messages.userRegistrationError);
                _logger.Log(Messages.userRegistrationError);
                return View("../Home/Index");
            }
        }

    }
}