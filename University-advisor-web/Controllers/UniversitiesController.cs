using System;
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
    public class UniversitiesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Dictionary<string, object>> GetUniversities() 
        {
            return new UniversityModel().GetUniversities();
        }

        [HttpGet("autoCompleteSearch")]
        public IEnumerable<AutoCompleteItemModel> GetAutoCompleteSearchUniversities()
        {
            var listOfItems = new List<AutoCompleteItemModel>();
            var listOfUniversities = new UniversityModel().GetUniversities();
            foreach (var university in listOfUniversities)
            {
                listOfItems.Add(
                    new AutoCompleteItemModel(
                        university["name"].ToString(),
                        "View",
                        Convert.ToInt32(university["universityId"].ToString()))
                    );
            }
            return listOfItems;
        }
    }
}