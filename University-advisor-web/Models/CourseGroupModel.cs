using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class CourseGroupModel
    {
        public int GroupId { get; set; }
        public string Group { get; set; }
        public int ImageId { get; set; }

        public List<Dictionary<string, object>> CourseGroups()
        {
            return SqlDriver.Fetch("SELECT * FROM courseGroup");
        }
    }
}
