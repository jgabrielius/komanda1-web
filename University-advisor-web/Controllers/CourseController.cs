using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        // GET: api/Course
        [HttpGet]
        public IEnumerable<AutoCompleteItemModel> Get()
        {
            var listOfItems = new List<AutoCompleteItemModel>();
            var listOfCourses = new UniversityModel().GetAllCoursesWithUniversityNames();
            foreach (var course in listOfCourses)
            {
                listOfItems.Add(
                    new AutoCompleteItemModel(
                        $"{course["program"].ToString()} ({course["name"].ToString()})",
                        "CourseReview",
                        Convert.ToInt32(course["studyProgramId"].ToString()))
                    );
            }
            return listOfItems;
        }
    }
}