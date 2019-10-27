using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    // TODO: When model state is valid, new user has to be created
            //    return View(model);
            //}
            return View("../Registration/UserCreated", model);
        }
    }
}