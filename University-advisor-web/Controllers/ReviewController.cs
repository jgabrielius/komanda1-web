using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            var model = new UniversityModel();
            return View(model);
        }

        public IActionResult View(int id)
        {
            var model = new UniversityModel(id);
            return View(model);
        }

        public IActionResult CourseReview(int id)
        {
            var model = new CourseReviewModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitCourseReview(CourseReviewModel model)
        {
            model.userId = HttpContext.Session.GetInt32("UserId")??0;
            model.SaveReviews();
            return RedirectToAction("Index");
        }

        public IActionResult UniversityReview(int id)
        {
            var model = new UniversityReviewModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitUniversityReview(UniversityReviewModel model)
        {
            model.userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            model.SaveReviews();
            return RedirectToAction("Index");
        }
    }
}