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
    public class UniversityController : ControllerBase
    {
        // GET: api/University
        [HttpGet]
        public IEnumerable<AutoCompleteItemModel> Get()
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
