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
    public class ForumController : Controller
    {
        private readonly IErrorHandler _errorHandler;
        public ForumController(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var forumModel = new ForumModel();
            return View("../Pages/Forum/Index", forumModel);
        }

        [HttpGet]
        public IActionResult Question()
        {
            var forumModel = new ForumModel();
            return View(forumModel);
        }

        [HttpPost]
        public IActionResult Question(ForumModel forumModel)
        {
            forumModel.userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            forumModel.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            forumModel.SaveQuestion();
            return RedirectToAction("Index", "Forum");
        }

        public IActionResult ViewQuestion(int id)
        {
            var forumModel = new ForumModel(id);
            return View("../Pages/Forum/ViewQuestion", forumModel);
        }

        public IActionResult Reply(ForumModel forumModel)
        {
            forumModel.userIdReply = HttpContext.Session.GetInt32("UserId") ?? 0;
            forumModel.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            forumModel.SaveReply();
            forumModel.answer = String.Empty;
            return RedirectToAction("Index", "Forum");
        }
    }
}