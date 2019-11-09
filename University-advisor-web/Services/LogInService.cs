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
    public class LogInService : ILogInService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger _logger;

        public LogInService(IPasswordHasher passwordHasher, ILogger logger)
        {
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public bool ValidateFields(UserModel user)
        {
            var hashedPassword = _passwordHasher.CreateMD5(user.Password);
            Dictionary<string, object> result = SqlDriver.Row($"SELECT userid from users WHERE username='{user.Username}' AND password='{hashedPassword}';");
            if(result != null && result.Count > 0)
            {
                user.UserId = (int)(long)result["userId"];
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
