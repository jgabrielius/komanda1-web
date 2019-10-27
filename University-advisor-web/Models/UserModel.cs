using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Universities = new List<SelectListItem>
            {
                // TODO: Needs to be fetched from DB
                new SelectListItem("Vilnius university", "Vilnius university"),
                new SelectListItem("ISM", "ISM"),
                new SelectListItem("Vilnius Gediminas Technical University", "Vilnius Gediminas Technical University")
            };
            Statuses = new List<SelectListItem>
            {
                new SelectListItem("Student", "Student"),
                new SelectListItem("Lecturer", "Lecturer"),
                new SelectListItem("Graduate", "Graduate")
            };
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> Universities { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public string Password { get; set; }
    }
}
