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
        private readonly IErrorHandler _errorHandler;

        public LogInController(ILogger logger, ILogInService login, IErrorHandler errorHandler)
        {
            _logger = logger;
            _login = login;
            _errorHandler = errorHandler;
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
                return View("../Home/Index", model);
            }
            else
            {
                _errorHandler.ShowError(this, "Wrong username or password");
                _logger.Log("User log in failed.");
            }
            return View("../LogIn/Index", model);
        }

    }
}