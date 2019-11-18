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
    public class AutoCompleteSeachController : ControllerBase
    {
        // GET: api/AutoCompleteSeach
        [HttpGet("courses")]

        public IEnumerable<AutoCompleteItemModel> GetCourses()
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

        [HttpGet("universities")]
        public IEnumerable<AutoCompleteItemModel> GetUniversities()
        {
            var listOfItems = new List<AutoCompleteItemModel>();
            var listOfUniversities = new UniversityModel().GetUniversities();
            foreach (var university in listOfUniversities)
            {
                listOfItems.Add(
                    new AutoCompleteItemModel(
                        university["name"].ToString(),
                        "View",
                        Convert.ToInt32(university["universityId"].ToString()))
                    );
            }
            return listOfItems;
        }
    }
}
