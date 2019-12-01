using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class CourseModel
    {
        public int StudyProgramId { get; set; }
        public string StudyProgramName { get; set; }
        public string Description { get; set; }
        public int UniversityId { get; set; }
        public string Group{get;set;}
        public string Direction{get;set;}
        public string City{get;set;}
        public string Program{get;set;}
        public string AIKOS_Link { get; set; }
        public string HS_Link { get; set; }
        public string Presentation { get; set; }
        public string Clarity { get; set; }
        public string Feedback { get; set; }
        public string Encouragement { get; set; }
        public string Effectiveness { get; set; }
        public string Satisfaction { get; set; }

        public CourseModel() { }
        public CourseModel(int studyProgramId)
        {
            this.StudyProgramId = studyProgramId;
            StudyProgramName = SqlDriver.Row($"SELECT program FROM studyProgrammes WHERE studyProgramId = {studyProgramId}")["program"].ToString();
            Description = SqlDriver.Row($"SELECT description FROM studyProgrammes WHERE studyProgramId = {studyProgramId}")["description"].ToString();
            var sqlCourseReviews = SqlDriver.Row($"SELECT round(avg(presentation),1) as presentation, round(avg(clarity),1) as clarity," +
                $"round(avg(feedback),1) as feedback, round(avg(encouragement),1) as encouragement, round(avg(effectiveness),1) as effectiveness, " +
                $"round(avg(satisfaction),1) as satisfaction" +
                $" FROM courseReviews WHERE courseId = {studyProgramId};");
            if (!String.IsNullOrEmpty(sqlCourseReviews["presentation"].ToString()))
            {
                Presentation = sqlCourseReviews["presentation"].ToString();
                Clarity = sqlCourseReviews["clarity"].ToString();
                Feedback = sqlCourseReviews["feedback"].ToString();
                Encouragement = sqlCourseReviews["encouragement"].ToString();
                Effectiveness = sqlCourseReviews["effectiveness"].ToString();
                Satisfaction = sqlCourseReviews["satisfaction"].ToString();
            }
            else
            {
                Presentation = "N/A";
                Clarity = "N/A";
                Feedback = "N/A";
                Encouragement = "N/A";
                Effectiveness = "N/A";
                Satisfaction = "N/A";
            }
        }
        public long CountReviews()
        {
            return (long)SqlDriver.Row($"SELECT COUNT(*) as count FROM courseReviews WHERE review IS NOT NULL AND courseId={StudyProgramId}")["count"];
        }
        public List<Dictionary<string, object>> GetAllReviews()
        {
            return SqlDriver.Fetch($"SELECT users.status, studyProgrammes.program, courseReviews.review," +
                $"  courseReviews.date FROM courseReviews, studyProgrammes, users WHERE review IS NOT NULL AND " +
                $"studyProgrammes.studyProgramId=users.courseId AND users.userId=courseReviews.userId " +
                $"AND courseReviews.courseId={StudyProgramId}");
        }
        public List<Dictionary<string, object>> getAllCoursesData()
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
