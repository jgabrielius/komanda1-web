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

        public UserModel(string Username)
        {
            this.Username = Username;
            Password = SqlDriver.Row($"SELECT password from users where username='{Username}';")["password"].ToString();
            Email = SqlDriver.Row($"SELECT email from users where username='{Username}';")["email"].ToString();
        }

        public UserModel()
        {
        }

        public void ChangePassword()
        {
            SqlDriver.Execute($"UPDATE users SET password =@0 WHERE username=@1;", new ArrayList { NewPassword, Username });
        }

    }


}
