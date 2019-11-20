using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Constants;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

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
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult LogIn(UserModel model)
        {
            FailedLogIn failedLogIn = LogFailedLogIn;
            if (_login.ValidateFields(model))
            {
                failedLogIn.Invoke(Messages.userLoggedIn);
                _logger.LogStats(model);
                HttpContext.Session.SetInt32("UserId", model.UserId);
                HttpContext.Session.SetInt32("UserUniversityId", model.UniversityId);
                HttpContext.Session.SetInt32("UserCourseId", model.CourseId);
                return View("../Home/Index", model);
            }
            else
            {
                _errorHandler.ShowError(this, Messages.wrongUsernameOrPassword);
                _logger.Log(Messages.userLogInError);
            }
            return RedirectToAction("Index", model);
        }

        private void LogFailedLogIn(string message)
        {
            _logger.Log(message);
        }

    }
}