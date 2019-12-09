using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserModel(HttpContext.Session.GetInt32("UserId") ?? 0);
            return View(model);
        }

        [HttpPost]
        public IActionResult PostPreferences(UserModel model) 
        {
            model.ChangeCityPreferences();
            model.ChangeGroupPreferences();
            model.ChangeDirectionPreferences();
            model = new UserModel(model.UserId);
            return RedirectToAction("Index", model);
        }
    }
}