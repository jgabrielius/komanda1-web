using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;

namespace University_advisor_web.Services
{
    public class RegistrationService : IRegistrationService
    {
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
    }
}
