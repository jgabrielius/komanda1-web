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
        public string description { get; set; }
        public string variety { get; set; }
        public string availability { get; set; }
        public string accessability { get; set; }
        public string quality { get; set; }
        public string unions { get; set; }
        public string cost { get; set; }

        public UniversityModel(){}
        public UniversityModel(int universityId)
        {
            this.universityId = universityId;
            var sqlUniversity = SqlDriver.Row($"SELECT name, description FROM universities WHERE universityId = {universityId};");
            universityName = sqlUniversity["name"].ToString();
            description = sqlUniversity["description"].ToString();           
            var sqlUniversityReviews = SqlDriver.Row($"SELECT round(avg(variety),1) as variety, round(avg(availability),1) as availability," +
                $"round(avg(accessability),1) as accessability, round(avg(quality),1) as quality, round(avg(unions),1) as unions, " +
                $"round(avg(cost),1) as cost" +
                $" FROM universityReviews WHERE universityId = {universityId};");
            if (!String.IsNullOrEmpty(sqlUniversityReviews["variety"].ToString()))
            {
                variety = sqlUniversityReviews["variety"].ToString();
                availability = sqlUniversityReviews["availability"].ToString();
                accessability = sqlUniversityReviews["accessability"].ToString();
                quality = sqlUniversityReviews["quality"].ToString();
                unions = sqlUniversityReviews["unions"].ToString();
                cost = sqlUniversityReviews["cost"].ToString();
            }
            else
            {
                variety = "N/A";
                availability = "N/A";
                accessability = "N/A";
                quality = "N/A";
                unions = "N/A";
                cost = "N/A";
            }     
        }

        public List<Dictionary<string,object>> GetUniversities()
        {
            return SqlDriver.Fetch("SELECT * FROM universities");
        }

        public List<Dictionary<string, object>> GetUniversitiesWithRatings()
        {
            return SqlDriver.Fetch("SELECT u.universityId, name, round(avg(variety),1) as variety, round(avg(availability),1) as availability, " +
                "round(avg(accessability),1) as accessability, round(avg(quality),1) as quality, round(avg(unions),1) as unions, " +
                "round(avg(cost),1) as cost " +
                "FROM universities u LEFT JOIN universityReviews ur ON u.universityId=ur.universityId " +
                "GROUP BY u.universityId,name");
        }

        public List<Dictionary<string, object>> GetCourses()
        {
            return SqlDriver.Fetch($"SELECT * FROM studyProgrammes WHERE universityId = {universityId}");
        }
        public List<Dictionary<string, object>> GetAllCoursesWithUniversityNames()
        {
            return SqlDriver.Fetch("SELECT courses.program, courses.studyProgramId, uni.name " +
                "FROM studyProgrammes courses " +
                "LEFT JOIN universities uni WHERE courses.universityId = uni.universityId");
        }
        public List<Dictionary<string, object>> GetCoursesWithRatings()
        {
            return SqlDriver.Fetch($"SELECT *," +
                $"round(avg(presentation),1) as presentation, round(avg(clarity),1) as clarity, round(avg(feedback),1) as feedback," +
                $" round(avg(encouragement),1) as encouragement, round(avg(effectiveness),1) as effectiveness, " +
                $"round(avg(satisfaction),1) as satisfaction " +
                $"FROM studyProgrammes left join courseReviews on studyProgramId=courseId" +
                $" WHERE universityId = {universityId} group by studyProgramId,[group], direction, program, city");
        }
    }
}
