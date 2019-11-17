using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using University_advisor_web.Constants;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;
using static University_advisor_web.Models.UniversityReviewModel;

namespace University_advisor_web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger _logger;
        private readonly IErrorHandler _errorHandler;

        public delegate void Print(int value);

        public ReviewController(ILogger logger,IErrorHandler errorHandler)
        {
            _logger = logger;
            _errorHandler = errorHandler;
        }
        public IActionResult Index()
        {
            var model = new UniversityModel();
            return View(model);
        }

        public IActionResult View(int id)
        {
            Lazy<UniversityModel> _model = new Lazy<UniversityModel>(() => new UniversityModel(id));
            var model = _model.Value;
            return View(model);
        }

        public IActionResult CourseReview(int id)
        {
            var model = new CourseReviewModel(id)
            {
                UserId = HttpContext.Session.GetInt32("UserId") ?? 0
            };
            if (model.IsDuplicate())
            {
                _errorHandler.ShowError(this, Messages.reviewAlreadySubmitted, "Alert");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Log(Messages.courseReviewSubmitted);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult SubmitCourseReview(CourseReviewModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId")??0;
            model.SaveReviews();
            return RedirectToAction("Index");
        }

        public IActionResult UniversityReview(int id)
        {
            var model = new UniversityReviewModel(id)
            {
                UserId = HttpContext.Session.GetInt32("UserId") ?? 0
            };
            model.DuplicateReview += DuplicateReview;
            if (model.IsDuplicate())
            {
                return RedirectToAction("View", new { id });
            }
            _logger.Log(Messages.universityReviewSubmitted);
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitUniversityReview(UniversityReviewModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.SaveReviews();
            return RedirectToAction("Index");
        }

        private void DuplicateReview(object sender, DuplicateReviewEventArgs e)
        {
            _errorHandler.ShowError(this, e.Message, "Alert");
        }
    }
}