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
    public class LogInController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILogInService _login;

        public LogInController(ILogger logger, ILogInService login)
        {
            _logger = logger;
            _login = login;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LogIn(UserModel model)
        {
           if (_login.ValidateFields(model))
            {
                _logger.Log("User logged in.");
                _logger.LogStats(model);
                HttpContext.Session.SetInt32("UserId", model.UserId);
                return View("../LogIn/LogInSuccessfull", model);
            }
            else
            {
                // Message for user that log in failed.
                _logger.Log("User log in failed.");
            }
            return View("../LogIn/Index", model);
        }

    }
}