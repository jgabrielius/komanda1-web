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
    public class SettingsController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IErrorHandler _errorHandler;
        private readonly ILogger _logger;

        public SettingsController(IPasswordHasher passwordHasher, IErrorHandler errorHandler, ILogger logger)
        {
            _passwordHasher = passwordHasher;
            _errorHandler = errorHandler;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new UserModel(HttpContext.Session.GetInt32("UserId") ?? 0);
            model.Universities = model.GetAllUniversities();
            model.Statuses = model.GetAllStatuses();
            model.Courses = model.GetCourses(HttpContext.Session.GetInt32("UserId") ?? 0);
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0; 
            model.GetCurrentPassword(model.UserId);
            if (_passwordHasher.CreateMD5(model.CurrentPassword) == model.Password)
            {
                if (_passwordHasher.CreateMD5(model.CurrentPassword) == _passwordHasher.CreateMD5(model.NewPassword))
                {
                    _errorHandler.ShowError(this, Messages.newPasswordSameAsOldError);
                }
                else
                {
                    if (_passwordHasher.CreateMD5(model.NewPassword) == _passwordHasher.CreateMD5(model.NewPassword2))
                    {
                        model.ChangePassword(_passwordHasher);
                        _errorHandler.ShowError(this, Messages.passwordChangeSuccessfull, "Success");
                        return RedirectToAction("Index", model);
                    }
                    else
                    {
                        _errorHandler.ShowError(this, Messages.passwordsDontMatch);
                    }
                }
            }
            else
            {
                _errorHandler.ShowError(this, Messages.incorrectPassword);
            }
            return RedirectToAction("Index", model);

        }

        [HttpPost]
        public IActionResult ChangeEmail(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (model.CurrentEmail == model.Email)
            {
                if (model.CurrentEmail == model.NewEmail)
                {
                    _errorHandler.ShowError(this, Messages.newEmailSameAsOldError);
                }
                else
                {
                    if (model.NewEmail == model.NewEmail2)
                    {
                        model.ChangeEmail();
                        _errorHandler.ShowError(this, Messages.emailChangeSuccessfull, "Success");
                        return RedirectToAction("Index", model);
                    }
                    else
                    {
                        _errorHandler.ShowError(this, Messages.emailsDontMatch);
                    }
                }
            }
            else
            {
                _errorHandler.ShowError(this, Messages.incorrectEmail);
            }
            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public IActionResult ChangeUniversity(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeUniversity();
            HttpContext.Session.SetInt32("UserUniversityId", Convert.ToInt32(model.UniversityId));
            _errorHandler.ShowError(this, Messages.universityChangeSuccessfull, "Success");
            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public IActionResult ChangeCourse(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeCourse();
            HttpContext.Session.SetInt32("UserCourseId", Convert.ToInt32(model.CourseId));
            _errorHandler.ShowError(this, Messages.courseChangeSuccessfull, "Success");
            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public IActionResult ChangeStatus(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeStatus();
            _errorHandler.ShowError(this, Messages.statusChangeSuccessfull, "Success");
            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public IActionResult UploadFile(UserModel model)
        {
            var rec = new CardRecognition(_logger);
            var result = rec.StartStudentCardValidation(model.File);
            _errorHandler.ShowError(this, result.Information, "Information");
            return RedirectToAction("Index", model);
        }
    }
}