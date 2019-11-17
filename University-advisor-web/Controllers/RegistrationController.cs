using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRegistrationService _registration;

        public RegistrationController(ILogger logger, 
                                      IRegistrationService registration)
        {
            _logger = logger;
            _registration = registration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationFormModel(_registration.GetAllUniversities(), _registration.GetAllCourses());
            return View(model);
        }
       

        [HttpPost]
        public IActionResult SignUp(RegistrationFormModel model)
        {
            if(_registration.AddUser(model.User))
            {
                _logger.Log("User has been sucessfully registered");
                _logger.LogStats(model.User);
                return View("../Registration/UserCreated", model);
            }
            else
            {
                // Message for user that user cannot be created.
                _logger.Log("User cannot be created");
                return View("../Home/Index");
            }
        }


       
    }
}