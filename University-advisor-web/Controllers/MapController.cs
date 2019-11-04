using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University_advisor_web.Controllers
{
    public class MapController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var model = new MapModel();
            model.Address = "Vilnius";
            model.Range = 500;
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(string address, double range)
        {
            var model = new MapModel();
            model.Address = address;
            model.Range = range;
            return View(model);
        }
    }
}
