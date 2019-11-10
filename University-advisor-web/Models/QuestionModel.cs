using System;
using System.Collections;
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

        public void SaveQuestion()
        {
            SqlDriver.Execute("INSERT INTO questions (userId,questionId,question,message) " +
                "values (@0,@1,@2,@3)", new ArrayList() { userId, questionId, question, message});
        }

        public List<QuestionModel> GetAllQuestions()
        {
            var temporaryList = new List<QuestionModel>()
            {
                new QuestionModel(){userId=8236, questionId=0, question="Question related to VU", message="I am planning to study and..."},
                new QuestionModel(){userId=1241, questionId=1, question="How to use UniCompare?", message="University advisor has a feature..."},
                new QuestionModel(){userId=4124, questionId=2, question="Help me with university choice", message="I don't know where to study..."},
                new QuestionModel(){userId=1231, questionId=3, question="How to sign up?", message="I cannot find tab sign up..."},
            };

            return temporaryList;
        }
    }

}
