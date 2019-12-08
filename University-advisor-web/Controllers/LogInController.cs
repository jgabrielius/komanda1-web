using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Constants;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;
using University_advisor_web.Tools;

namespace University_advisor_web.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILogInService _login;
        private readonly IErrorHandler _errorHandler;

        private delegate void FailedLogIn(string s);

        public LogInController(ILogger logger, ILogInService login, IErrorHandler errorHandler)
        {
            _logger = logger;
            _login = login;
            _errorHandler = errorHandler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeModel();
            //return View(model);
            return View("../Pages/LogIn/Index");
        }

        [HttpPost]
        public IActionResult LogIn(HomeModel model)
        {
            FailedLogIn failedLogIn = LogFailedLogIn;
            if (_login.ValidateFields(model.User))
            {
                failedLogIn.Invoke(Messages.userLoggedIn);
                _logger.LogStats(model);
                HttpContext.Session.SetInt32("UserId", model.User.UserId);
                HttpContext.Session.SetInt32("UserUniversityId", model.User.UniversityId);
                HttpContext.Session.SetInt32("UserCourseId", model.User.CourseId);
                model.Map = new MapModel("Vilnius", "Universities");
                return View("../Pages/Index", model);
            }
            else
            {
                _errorHandler.ShowError(this, Messages.wrongUsernameOrPassword);
                _logger.Log(Messages.userLogInError);
            }
            return RedirectToAction("Index", "Home");
        }

        private void LogFailedLogIn(string message)
        {
            _logger.Log(message);
        }

    }
}