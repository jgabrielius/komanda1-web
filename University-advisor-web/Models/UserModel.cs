using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;

namespace University_advisor_web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public string Course { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPassword2 { get; set; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
        public string NewEmail2 { get; set; }
        public string SelectedUniversity { get; set; }
        public string SelectedStatus { get; set; }
        public List<SelectListItem> Universities { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        public UserModel(int userId)
        {
            var sqlUser = SqlDriver.Row($"SELECT username, email, first_name, last_name, universities.name, studyProgrammes.program, status from universities, users, studyProgrammes WHERE users.universityid = universities.universityId and userId = " + userId.ToString() + ";");
            UserId = userId;
            Username = sqlUser["username"].ToString();
            Email = sqlUser["email"].ToString();
            FirstName = sqlUser["first_name"].ToString();
            LastName = sqlUser["last_name"].ToString();
            University = sqlUser["name"].ToString();
            Course = sqlUser["program"].ToString();
            Status = sqlUser["status"].ToString();
        }

        public UserModel()
        {
        }
        public void GetCurrentPassword(int userId)
        {
            Password = SqlDriver.Row($"SELECT password FROM users WHERE userId= " + userId.ToString() + ";")["password"].ToString();
        }
        public void GetCurrentEmail(int userId)
        {
            Email = SqlDriver.Row($"SELECT email FROM users WHERE userId= " + userId.ToString() + ";")["email"].ToString();
        }

        public void ChangePassword(IPasswordHasher passwordHasher)
        {
            SqlDriver.Execute($"UPDATE users SET password =@0 WHERE userid=@1;", new ArrayList { passwordHasher.CreateMD5(NewPassword), UserId });
        }
        public void ChangeEmail()
        {
            SqlDriver.Execute($"UPDATE users SET email =@0 WHERE userid=@1;", new ArrayList { NewEmail, UserId });
        }

        public void ChangeStatus()
        {
            SqlDriver.Execute($"UPDATE users SET status =@0 WHERE userid=@1;", new ArrayList { SelectedStatus, UserId });
        }

        public void ChangeUniversity()
        {
            var newUniversityIdFromDB = SqlDriver.Row("SELECT universityid from universities WHERE name ='" + SelectedUniversity + "';");
            var newUniversityId = newUniversityIdFromDB["universityId"].ToString();
            SqlDriver.Execute("UPDATE users SET universityid =@0 WHERE userid =@1;", new ArrayList { newUniversityId, UserId });
        }

        public List<SelectListItem> GetAllUniversities()
        {
            var universityResult = SqlDriver.Fetch("SELECT name, universityId FROM universities");
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

        public List<SelectListItem> GetAllStatuses()
        {
            List<SelectListItem> statuses = new List<SelectListItem>
            {
                new SelectListItem("Student", "Student"),
                new SelectListItem("Lecturer", "Lecturer"),
                new SelectListItem("Graduate", "Graduate")
            };
            return statuses;
        }
    }


}
