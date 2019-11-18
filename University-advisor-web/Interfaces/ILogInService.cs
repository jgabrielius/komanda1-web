using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using University_advisor_web.Models;

namespace University_advisor_web.Interfaces
{
    public interface ILogInService
    {
        public bool ValidateFields(UserModel user);
    }
}