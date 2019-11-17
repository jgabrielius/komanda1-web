using System.Collections.Generic;
using System.Collections;

namespace University_advisor_web.Models
{
    public class UniversityReviewModel
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int UserId { get; set; }
        public string Variety { get; set; }
        public string Availability { get; set; }
        public string Accessability { get; set; }
        public string Quality { get; set; }
        public string Unions { get; set; }
        public string Cost { get; set; }

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
            this.UniversityId = universityId;
            UniversityName = SqlDriver.Row($"SELECT name FROM universities WHERE universityId ={universityId}")["name"].ToString();
        }

        public void SaveReviews()
        {
            SqlDriver.Execute("INSERT INTO universityReviews (variety,availability,accessability,quality,unions,cost,universityId,userId) " +
                "values (@0,@1,@2,@3,@4,@5,@6,@7)", new ArrayList() { Variety, Availability, Accessability, Quality, Unions, Cost, UniversityId, UserId});
        }

        public bool IsDuplicate()
        {
            return SqlDriver.Exists($"SELECT * FROM universityReviews WHERE userId ={UserId} AND universityId={UniversityId}");
        }
    }
}
