using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        public SettingsController(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public IActionResult Index()
        {
            var model = new UserModel(HttpContext.Session.GetInt32("UserId") ?? 0);
            model.Universities = model.GetAllUniversities();
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangePassword(_passwordHasher);
            return View("../Settings/ChangeSuccessfull", model);
        }

        [HttpPost]
        public IActionResult ChangeEmail(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeEmail();
            return View("../Settings/ChangeSuccessfull", model);
        }

        [HttpPost]
        public IActionResult ChangeUniversity(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeUniversity();
            return View("../Settings/ChangeSuccessfull", model);
        }
    }
}