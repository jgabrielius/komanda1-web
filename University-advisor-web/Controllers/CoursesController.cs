﻿using System;
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
        // GET: api/Courses
        [HttpGet]
        public IEnumerable<CourseModel> GetCourses()
        {
            var data = new List<CourseModel>();
            foreach (var course in new CourseModel().CoursesList())
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

        [HttpGet("details")]
        public IEnumerable<CourseModel> GetCoursesWithDetails()
        {
            var data = new List<CourseModel>();
            foreach (var course in new CourseModel().CoursesList())
            {
                data.Add(new CourseModel()
                {
                    StudyProgramId = Convert.ToInt32(course["studyProgramId"].ToString()),
                    UniversityId = Convert.ToInt32(course["universityId"].ToString()),
                    Group =  course["group"].ToString(),
                    Direction = course["direction"].ToString(),
                    City = course["city"].ToString(),
                    Program = course["program"].ToString(),
                    AIKOS_Link = course["AIKOS_link"].ToString(),
                    HS_Link = course["HS_link"].ToString(),
                });
            }
            return data;
        }

        [HttpGet("groups")]
        public IEnumerable<CourseGroupModel> GetCourseGroups() 
        {
            var data = new List<CourseGroupModel>();
            foreach (var group in new CourseGroupModel().CourseGroups())
            {
                data.Add(new CourseGroupModel()
                {
                    GroupId = Convert.ToInt32(group["groupId"].ToString()),
                    Group = group["group"].ToString(),
                    ImageId = Convert.ToInt32(group["imageId"].ToString()),
                });
            }
            return data;
        }

        [HttpGet("cities")]
        public IEnumerable<string> GetCourseCities()
        {
            var data = new List<string>();
            foreach (var city in new CourseModel().CityList())
            {
                data.Add(city["city"].ToString());
            }
            return data;
        }
    }
}
