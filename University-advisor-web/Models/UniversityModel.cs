using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class UniversityModel
    {
        public int universityId { get; set; }
        public string universityName { get; set; }

        public UniversityModel(){}
        public UniversityModel(int universityId)
        {
            this.universityId = universityId;
            universityName= SqlDriver.Row($"SELECT name FROM universities WHERE universityId = {universityId}")["name"].ToString();
        }

        public List<Dictionary<string,object>> GetUniversities()
        {
            return SqlDriver.Fetch("SELECT * FROM universities");
        }

        public List<Dictionary<string, object>> GetUniversitiesWithRatings()
        {
            return SqlDriver.Fetch("SELECT u.universityId,name,avg(variety) as variety,avg(availability) as availability,avg(accessability) as accessability,avg(quality) as quality,avg(unions) as unions,avg(cost) as cost " +
                "FROM universities u LEFT JOIN universityReviews ur ON u.universityId=ur.universityId " +
                "GROUP BY u.universityId,name");
        }

        public List<Dictionary<string, object>> GetCourses()
        {
            return SqlDriver.Fetch($"SELECT * FROM studyProgrammes WHERE universityId = {universityId}");
        }
        public List<Dictionary<string, object>> GetCoursesWithRatings()
        {
            return SqlDriver.Fetch($"SELECT *," +
                $"avg(presentation) as presentation,avg(clarity) as clarity,avg(feedback) as feedback, avg(encouragement) as encouragement,avg(effectiveness) as effectiveness,avg(satisfaction) as satisfaction " +
                $"FROM studyProgrammes left join courseReviews on studyProgramId=courseId" +
                $" WHERE universityId = {universityId} group by studyProgramId,[group], direction, program, city");
        }
    }
}
