using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ILogger _logger;

        public RegistrationController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegistrationFormModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult SignUp(RegistrationFormModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    // TODO: When model state is valid, new user has to be created
            //    return View(model);
            //}

            _logger.Log("User has been sucessfully registered", "WARN");
            _logger.LogStats(model.User);   

            return View("../Registration/UserCreated", model);
        }

       
    }
}