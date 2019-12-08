using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_advisor_web.Models;

namespace University_advisor_web.Controllers
{
    public class PreferencesController : Controller
    {
        public IActionResult SetPreferences()
        {
            var model = new UserModel(HttpContext.Session.GetInt32("UserId") ?? 0);
            return View(model);
        }
        public IActionResult Index()
        {
            var recommendations = new List<RecommendationModel>();
            var model = new UserModel(HttpContext.Session.GetInt32("UserId") ?? 0);
            var recommendedCourses = new CourseModel().RecommendedCourses(model);
            if(recommendedCourses == null) return RedirectToAction("SetPreferences", model);
            var uniIds = new List<int>();
            var usedUniversities = new List<UniversityModel>();
            foreach (var course in recommendedCourses)
            {
                int universityId = Convert.ToInt32(course["universityId"].ToString());
                if (!uniIds.Contains(universityId))
                {
                    uniIds.Add(universityId);
                    usedUniversities.Add(new UniversityModel(universityId));
                }
            }

            foreach (var university in usedUniversities)
            {
                var newUniversityRecommendations = new RecommendationModel();
                newUniversityRecommendations.University = university;
                newUniversityRecommendations.Courses = new List<CourseModel>();

                foreach (var course in recommendedCourses)
                {
                    var universityId = Convert.ToInt32(course["universityId"].ToString());
                    if (newUniversityRecommendations.University.UniversityId == universityId)
                    {
                        var courseId = Convert.ToInt32(course["studyProgramId"].ToString());
                        var uniCourse = new CourseModel(courseId);
                        newUniversityRecommendations.Courses.Add(uniCourse);
                    }
                }
                recommendations.Add(newUniversityRecommendations);
            }

            return View(recommendations);
        }
        [HttpPost]
        public IActionResult PostPreferences(UserModel model)
        {
            model.ChangeCityPreferences();
            model.ChangeGroupPreferences();
            model.ChangeDirectionPreferences();
            model = new UserModel(model.UserId);
            return RedirectToAction("Index", model);
        }
    }
}