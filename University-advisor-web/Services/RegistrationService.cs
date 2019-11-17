using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;
using University_advisor_web.Models;

namespace University_advisor_web.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger _logger;

        public RegistrationService(IPasswordHasher passwordHasher, ILogger logger)
        {
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public bool AddUser(UserModel user)
        {
            if (!CheckIfUserExists(user))
            {
                try
                {
                    var universityId = SqlDriver.Row($"SELECT universityId from universities WHERE name='{user.University}';")["universityId"].ToString();
                    var courseId = SqlDriver.Row($"SELECT studyProgramId from studyProgrammes WHERE program='{user.Course}';")["studyProgramId"].ToString();
                    if (SqlDriver.Execute("INSERT INTO users (username, first_name, last_name, email, universityId, courseId, status, password) " +
                        "values (@0,@1,@2,@3,@4,@5,@6,@7)", 
                        new ArrayList() { user.Username, user.FirstName, user.LastName, user.Email, universityId, courseId , user.Status, _passwordHasher.CreateMD5(user.Password)}))
                    {
                        return true;
                    }
                    else
                    {
                        _logger.Log("Query cannot be executed");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log("User cannot be added" + Environment.NewLine + ex.Message, "ERROR");
                    return false;
                }
            }
            return false;
        }

        private bool CheckIfUserExists(UserModel user)
        {
            var row = SqlDriver.Row($"SELECT username from users where username='{user.Username}' and email='{user.Email}';");

            if (row == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<SelectListItem> GetAllUniversities()
        {
            var universityResult = SqlDriver.Fetch("SELECT name FROM universities");
            var universities = new List<SelectListItem>();
            if (universityResult.Count != 0)
            {
                foreach (Dictionary<string, object> row in universityResult)
                {
                    universities.Add(new SelectListItem(row["name"].ToString(), row["name"].ToString()));
                }
            }
            return universities;
        }

        public List<SelectListItem> GetAllCourses()
        {
            var courseResult = SqlDriver.Fetch("Select program FROM studyProgrammes");
            var courses = new List<SelectListItem>();
            if (courseResult.Count != 0)
            {
                foreach (Dictionary<string, object> row in courseResult)
                {
                    courses.Add(new SelectListItem(row["program"].ToString(), row["program"].ToString()));
                }
            }
            return courses;
        }
    }
}
