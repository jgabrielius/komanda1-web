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
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(UserModel model)
        {
            model.Username = "test";

            if (model.CurrentPassword.Equals(model.Password))
            {
                if (model.NewPassword.Equals(model.Password))
                {
                    //New password cannot be the same as old one.
                    return View("../Settings/PasswordChangeError", model);
                }
                else if (model.NewPassword.Equals(model.NewPassword2))
                {
                    model.ChangePassword(model.NewPassword);
                    return View("../Settings/PasswordChanged", model);
                }
                else
                {
                    //Password doesnt match.
                    return View("../Settings/PasswordChangeError", model);
                }
            }
            else
            {
                //Incorrect current password
                return View("../Settings/PasswordChangeError", model);
            }
        }
    }
}