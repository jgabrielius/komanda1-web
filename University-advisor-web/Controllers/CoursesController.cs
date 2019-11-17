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
    public class CoursesController : ControllerBase
    {
        // GET: api/Course
        [HttpGet]
        public IEnumerable<CourseModel> Get()
        {
            var data = new List<CourseModel>();
            foreach (var course in new CourseModel().getAllCoursesData())
            {
                data.Add(new CourseModel() 
                {
                    StudyProgramId = Convert.ToInt32(course["studyProgramId"].ToString()),
                    UniversityId = Convert.ToInt32(course["universityId"].ToString()),
                    Program = course["program"].ToString()
                });
            }
            return data;
        }
    }
}
