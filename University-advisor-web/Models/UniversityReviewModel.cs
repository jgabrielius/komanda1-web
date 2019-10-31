using System.Collections.Generic;
using System.Collections;

namespace University_advisor_web.Models
{
    public class UniversityReviewModel
    {
        public int universityId { get; set; }
        public string universityName { get; set; }
        public int userId { get; set; }
        public string variety { get; set; }
        public string availability { get; set; }
        public string accessability { get; set; }
        public string quality { get; set; }
        public string unions { get; set; }
        public string cost { get; set; }

        public Dictionary<string,string> ratingTypes = new Dictionary<string, string>() {
            {"variety","Variety of courses"},
            {"availability","Availability of extracurricular activities "},
            {"accessability","Access to faculty"},
            {"quality","Quality of academic facilities (library, PCs, etc.)"},
            {"unions","Student unions"},
            {"cost","Cost of studying"}
        };

        public UniversityReviewModel() { }
        public UniversityReviewModel(int universityId)
        {
            this.universityId = universityId;
            universityName = SqlDriver.Row($"SELECT name FROM universities WHERE universityId ={universityId}")["name"].ToString();
        }

        public void SaveReviews()
        {
            SqlDriver.Execute("INSERT INTO universityReviews (variety,availability,accessability,quality,unions,cost,universityId,userId) " +
                "values (@0,@1,@2,@3,@4,@5,@6,@7)", new ArrayList() { variety, availability, accessability, quality, unions, cost, universityId, userId});
        }
    }
}
