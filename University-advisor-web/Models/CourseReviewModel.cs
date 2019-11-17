using System.Collections.Generic;
using System.Collections;

namespace University_advisor_web.Models
{
    public class CourseReviewModel
    {
        public int StudyProgramId { get; set; }
        public int UserId { get; set; }
        public string StudyProgramName { get; set; }
        public string Presentation { get; set; }
        public string Clarity { get; set; }
        public string Feedback { get; set; }
        public string Encouragement { get; set; }
        public string Effectiveness { get; set; }
        public string Satisfaction { get; set; }

        public Dictionary<string,string> ratingTypes = new Dictionary<string, string>() {
            {"presentation","Presentation of content"},
            {"clarity","Clarity of expectations"},
            {"feedback","Clear feedback on performance"},
            {"encouragement","Encouragment of participation/discussion"},
            {"effectiveness","Overall teaching effectiveness"},
            {"satisfaction","How satisfied were you with this course"}
        };

        public CourseReviewModel() { }
        public CourseReviewModel (int studyProgramId)
        {
            this.StudyProgramId = studyProgramId;
            StudyProgramName = SqlDriver.Row($"SELECT program FROM studyProgrammes WHERE studyProgramId = {studyProgramId}")["program"].ToString();
        }

        public void SaveReviews()
        {
            SqlDriver.Execute("INSERT INTO courseReviews (presentation,clarity,feedback,encouragement,effectiveness,satisfaction,courseId,userId) " +
                "values (@0,@1,@2,@3,@4,@5,@6,@7)", new ArrayList() {Presentation,Clarity,Feedback,Encouragement,Effectiveness,Satisfaction,StudyProgramId,UserId});
        }

        public bool IsDuplicate()
        {
            return SqlDriver.Exists($"SELECT * FROM courseReviews WHERE userId ={UserId} AND courseId={StudyProgramId}");
        }
    }
}
