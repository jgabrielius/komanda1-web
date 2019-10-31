using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class ReviewModel
    {
        public int universityId { get; set; }
        public UniversityModel universityModel;
        public ReviewModel()
        {
            universityModel = new UniversityModel();
        }
        public ReviewModel(int universityId)
        {
            this.universityId = universityId;
            universityModel = new UniversityModel(universityId);
        }
    }
}
