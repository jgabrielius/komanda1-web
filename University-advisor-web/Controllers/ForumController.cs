using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class ForumController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Question()
        {
            var userQuestion = new QuestionModel();
            return View(userQuestion);
        }

        [HttpPost]
        public IActionResult Question(QuestionModel userQuestion)
        {
            //userQuestion.userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            //userQuestion.SaveQuestion();
            return View("../Forum/SubmittedQuestion", userQuestion);
        }

        [HttpGet]
        public IActionResult Questions()
        {
            // Here has to FetchAllQuestions method.
            var questions = new QuestionModel();
            return View(questions);
        }
    }
}