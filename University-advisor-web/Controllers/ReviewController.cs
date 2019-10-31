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
            var model = new ReviewModel(id);
            return View(model);
        }
    }
}