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

        // GET: api/University/5
        [HttpGet("{id}", Name = "Get")]
        public AutoCompleteItemModel Get(int id)
        {
            return new AutoCompleteItemModel("string","reviews", id);
        }

        // POST: api/University
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/University/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
