using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class UniversityModel
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string Description { get; set; }
        public string Variety { get; set; }
        public string Availability { get; set; }
        public string Accessability { get; set; }
        public string Quality { get; set; }
        public string Unions { get; set; }
        public string Cost { get; set; }
        public string Image { get; set; }

        public UniversityModel(){}
        public UniversityModel(int universityId)
        {
            this.UniversityId = universityId;
            var sqlUniversity = SqlDriver.Row($"SELECT name, description, image FROM universities WHERE universityId = {universityId};");
            UniversityName = sqlUniversity["name"].ToString();
            Description = sqlUniversity["description"].ToString();
            Image = sqlUniversity["image"].ToString();
            var sqlUniversityReviews = SqlDriver.Row($"SELECT round(avg(variety),1) as variety, round(avg(availability),1) as availability," +
                $"round(avg(accessability),1) as accessability, round(avg(quality),1) as quality, round(avg(unions),1) as unions, " +
                $"round(avg(cost),1) as cost" +
                $" FROM universityReviews WHERE universityId = {universityId};");
            if (!String.IsNullOrEmpty(sqlUniversityReviews["variety"].ToString()))
            {
                Variety = sqlUniversityReviews["variety"].ToString();
                Availability = sqlUniversityReviews["availability"].ToString();
                Accessability = sqlUniversityReviews["accessability"].ToString();
                Quality = sqlUniversityReviews["quality"].ToString();
                Unions = sqlUniversityReviews["unions"].ToString();
                Cost = sqlUniversityReviews["cost"].ToString();
            }
            else
            {
                Variety = "N/A";
                Availability = "N/A";
                Accessability = "N/A";
                Quality = "N/A";
                Unions = "N/A";
                Cost = "N/A";
            }     
        }

        public List<Dictionary<string,object>> GetUniversities()
        {
            return SqlDriver.Fetch("SELECT * FROM universities");
        }

        public List<Dictionary<string, object>> GetAllReviews()
        {
            return SqlDriver.Fetch($"SELECT users.status, studyProgrammes.program, universityReviews.review," +
                $"  universityReviews.date FROM universityReviews, studyProgrammes, users WHERE review IS NOT NULL AND " +
                $"studyProgrammes.studyProgramId=users.courseId AND users.userId=universityReviews.userId " +
                $"AND universityReviews.universityId={UniversityId}");
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
            return SqlDriver.Fetch($"SELECT * FROM studyProgrammes WHERE universityId = {UniversityId}");
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
                $" WHERE universityId = {UniversityId} group by studyProgramId,[group], direction, program, city");
        }
    }
}
