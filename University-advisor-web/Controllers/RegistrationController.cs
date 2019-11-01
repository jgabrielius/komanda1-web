using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationFormModel();
            return View(model);
        }

        //public IActionResult Display()
        //{
        //    return View("../Registration/Index");
        //}

        [HttpPost]
        public IActionResult SignUp(RegistrationFormModel model)
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