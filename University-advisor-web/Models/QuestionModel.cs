using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_advisor_web.Models
{
    public class QuestionModel
    {
        public int userId { get; set; }
        public int questionId { get; set; }
        public string question { get; set; }
        public string message { get; set; }

    }
}
