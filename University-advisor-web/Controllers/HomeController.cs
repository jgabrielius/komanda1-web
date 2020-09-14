using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Constants;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University_advisor_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRegistrationService _registration;
        private readonly IErrorHandler _errorHandler;

        public HomeController(ILogger logger,
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
            var model = new HomeModel();
            model.Map = new MapModel("Vilnius", "Universities");
            model.Registration = new RegistrationFormModel(_registration.GetAllUniversities(), _registration.GetAllCourses());
            //return View(model);
            return View("../Pages/Index", model);
        }

        [HttpGet]
        public IActionResult Display(HomeModel model)
        {
            return View("../Pages/Index", model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string address, double range)
        {
            var model = new HomeModel();
            model.Map = new MapModel(address, "Universities", range);
            model.Registration = new RegistrationFormModel(_registration.GetAllUniversities(), _registration.GetAllCourses());
            // Code to test if logging works correctly.
            _logger.Log(Messages.nearbyUniversitiesDisplayed);
            return PartialView("../Pages/Index", model);
        }
    }
}
