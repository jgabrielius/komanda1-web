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
        private readonly IPasswordHasher _passwordHasher;

        public RegistrationController(ILogger logger, 
                                      IRegistrationService registration,
                                      IPasswordHasher passwordHasher)
        {
            _logger = logger;
            _registration = registration;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationFormModel(_registration.GetAllUniversities());
            return View(model);
        }
       

        [HttpPost]
        public IActionResult SignUp(RegistrationFormModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    // TODO: When model state is valid, new user has to be created
            //    return View(model);
            //}

            _logger.Log("User has been sucessfully registered", "WARN");
            _logger.LogStats(model.User);   

            return View("../Registration/UserCreated", model);
        }


       
    }
}