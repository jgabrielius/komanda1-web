using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class StudyProgrammeModel
    {
        public List<Dictionary<string, object>> CityList() {
            return SqlDriver.Fetch("SELECT DISTINCT(city) FROM studyProgrammes");
        }

        public List<Dictionary<string, object>> GroupList() {
            return SqlDriver.Fetch("SELECT DISTINCT(group) FROM studyProgrammes");
        }

        public List<Dictionary<string, object>> DirectionList() {
            return SqlDriver.Fetch("SELECT DISTINCT(direction) FROM studyProgrammes");
        }
    }
}
