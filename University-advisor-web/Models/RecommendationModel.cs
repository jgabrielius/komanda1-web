using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Models;

namespace University_advisor_web.Models
{
    public class RecommendationModel
    {
        public UniversityModel University { get; set; }
        public List<CourseModel> Courses { get; set; }
        public List<Dictionary<string, object>> GetCourseGroups()
        {
            return new CourseModel().GroupList();
        }
        public List<Dictionary<string, object>> GetCourseCities()
        {
            return new CourseModel().CityList();
        }
    }
}