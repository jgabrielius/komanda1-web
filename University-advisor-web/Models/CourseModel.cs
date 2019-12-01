using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class CourseModel
    {
        public int StudyProgramId { get; set; }
        public int UniversityId { get; set; }
        public string Group { get; set; }
        public string Direction { get; set; }
        public string City { get; set; }
        public string Program { get; set; }
        public string AIKOS_Link { get; set; }
        public string HS_Link { get; set; }
        public List<Dictionary<string, object>> CoursesList()
        {
            var sqlData = SqlDriver.Fetch("SELECT * FROM studyProgrammes");
            return sqlData;
        }
        public List<Dictionary<string, object>> CityList()
        {
            return SqlDriver.Fetch("SELECT DISTINCT(city) FROM studyProgrammes");
        }

        public List<Dictionary<string, object>> GroupList()
        {
            return SqlDriver.Fetch("SELECT * FROM courseGroup");
        }

        public List<Dictionary<string, object>> DirectionList()
        {
            return SqlDriver.Fetch("SELECT DISTINCT(direction) FROM studyProgrammes");
        }
    }

}
