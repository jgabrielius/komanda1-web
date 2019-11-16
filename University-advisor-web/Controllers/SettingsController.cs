﻿using System;
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
        private readonly IErrorHandler _errorHandler;

        public SettingsController(IPasswordHasher passwordHasher, IErrorHandler errorHandler)
        {
            _passwordHasher = passwordHasher;
            _errorHandler = errorHandler;
        }
        public IActionResult Index()
        {
            var model = new UserModel(HttpContext.Session.GetInt32("UserId") ?? 0);
            model.Universities = model.GetAllUniversities();
            model.Statuses = model.GetAllStatuses();
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
                    _errorHandler.ShowError(this, "New password can't be the same as old one.");
                }
                else
                {
                    if (_passwordHasher.CreateMD5(model.NewPassword) == _passwordHasher.CreateMD5(model.NewPassword2))
                    {
                        model.ChangePassword(_passwordHasher);
                        return View("../Settings/ChangeSuccessfull", model);
                    }
                    else
                    {
                        _errorHandler.ShowError(this, "Passwords don't match.");
                    }
                }
            }
            else
            {
                _errorHandler.ShowError(this, "Incorrect password.");
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
                    _errorHandler.ShowError(this, "New email can't be the same as old one.");
                }
                else
                {
                    if (model.NewEmail == model.NewEmail2)
                    {
                        model.ChangeEmail();
                        return View("../Settings/ChangeSuccessfull", model);
                    }
                    else
                    {
                        _errorHandler.ShowError(this, "Emails don't match.");
                    }
                }
            }
            else
            {
                _errorHandler.ShowError(this, "Incorrect email.");
            }
            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public IActionResult ChangeUniversity(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeUniversity();
            return View("../Settings/ChangeSuccessfull", model);
        }

        [HttpPost]
        public IActionResult ChangeStatus(UserModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.ChangeStatus();
            return View("../Settings/ChangeSuccessfull", model);
        }
    }
}