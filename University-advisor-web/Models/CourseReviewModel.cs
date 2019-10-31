using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class CourseReviewModel
    {
        public int studyProgramId { get; set; }
        public string studyProgramName { get; set; }
        public string presentation { get; set; }
        public string clarity { get; set; }
        public string feedback { get; set; }
        public string encouragement { get; set; }
        public string effectiveness { get; set; }
        public string satisfaction { get; set; }

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
            this.studyProgramId = studyProgramId;
            studyProgramName = SqlDriver.Row("SELECT program FROM studyProgrammes WHERE studyProgramId")["program"].ToString();
        }
    }
}
