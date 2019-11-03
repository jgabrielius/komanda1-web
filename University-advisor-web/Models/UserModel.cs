using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPassword2 { get; set; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
        public string NewEmail2 { get; set; }

        public UserModel(string Username)
        {
            this.Username = Username;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.University = University;
            this.Status = Status;
            Password = SqlDriver.Row($"SELECT password from users where username='{Username}';")["password"].ToString();  
            FirstName = SqlDriver.Row($"SELECT first_name from users where username='{Username}';")["first_name"].ToString();
            LastName = SqlDriver.Row($"SELECT last_name from users where username='{Username}';")["last_name"].ToString();
            Email = SqlDriver.Row($"SELECT email from users where username='{Username}';")["email"].ToString();
            University = SqlDriver.Row($"SELECT universities.name from universities, users where universities.universityId = users.universityid and users.username = '{Username}';")["name"].ToString();
            Status = SqlDriver.Row($"SELECT status from users where username='{Username}';")["status"].ToString();
        }

        public UserModel()
        {
        }

        public void ChangePassword()
        {
            SqlDriver.Execute($"UPDATE users SET password =@0 WHERE username=@1;", new ArrayList { NewPassword, Username });
        }
        public void ChangeEmail()
        {
            SqlDriver.Execute($"UPDATE users SET email =@0 WHERE username=@1;", new ArrayList { NewEmail, Username });
        }

    }


}
