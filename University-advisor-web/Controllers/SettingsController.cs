using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(UserModel model)
        {
            model.Username = "test";
            model.Password = "password";

            model.ChangePassword();
            return View("../Settings/PasswordChanged", model);

        }
    }
}