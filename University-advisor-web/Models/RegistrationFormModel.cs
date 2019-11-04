﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class RegistrationFormModel
    {
        public RegistrationFormModel()
        {

        }
        public RegistrationFormModel(List<SelectListItem> universityList)
        {
            Universities = universityList;
            Statuses = new List<SelectListItem>
            {
                new SelectListItem("Student", "Student"),
                new SelectListItem("Lecturer", "Lecturer"),
                new SelectListItem("Graduate", "Graduate")
            };
        }
        public UserModel User { get; set; }
        public List<SelectListItem> Universities { get; set; }
        public List<SelectListItem> Statuses { get; set; }
    }
}
