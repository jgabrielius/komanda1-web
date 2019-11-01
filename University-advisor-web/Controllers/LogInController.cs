using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LogIn()
        {
            return View("Index");
        }

    }
}