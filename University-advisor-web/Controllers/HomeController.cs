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

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeModel();
            model.Map = new MapModel("Vilnius", "Universities");
            //return View(model);
            return View("../Pages/Index");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(string address, double range)
        {
            var model = new HomeModel();
            model.Map = new MapModel(address, "Universities", range);
            // Code to test if logging works correctly.
            _logger.Log(Messages.nearbyUniversitiesDisplayed);
            return View(model);
        }
    }
}
