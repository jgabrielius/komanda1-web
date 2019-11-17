using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University_advisor_web.Tools;

namespace University_advisor_web.Models
{
    public class ForumModel
    {
        public int userId { get; set; }
        public int questionId { get; set; }
        public string question { get; set; }
        public string message { get; set; }
        public int answerId { get; set; }
        public int userIdReply { get; set; }
        public string answer { get; set; }
        public ForumModel()
        {

        }

        public ForumModel(int questionId)
        {
            this.questionId = questionId;
            var sqlQuestion = SqlDriver.Row($"SELECT question, message, userId FROM questions WHERE questionId = {questionId};");
            question = sqlQuestion["question"].ToString();
            userId = sqlQuestion["userId"].ToString().TryParse(0);
            message = sqlQuestion["message"].ToString();
        }

        public void SaveQuestion()
        {
            Random random = new Random();
            int id = random.Next();
            questionId = id;
            SqlDriver.Execute("INSERT INTO questions (userId,questionId,question,message) " +
                "values (@0,@1,@2,@3)", new ArrayList() { userId, questionId, question, message});
        }

        public void SaveReply()
        {
            Random random = new Random();
            int id = random.Next();
            answerId = id;
            SqlDriver.Execute("INSERT INTO answers (userId,answerId,questionId,answer) " +
                "values (@0,@1,@2,@3)", new ArrayList() { userIdReply, answerId, questionId, answer });
        }

        public List<Dictionary<string, object>> GetAllQuestions()
        {
            return SqlDriver.Fetch($"SELECT * FROM questions");
        }
        public List<Dictionary<string, object>> GetAllReplies()
        {
            return SqlDriver.Fetch($"SELECT * FROM answers WHERE questionId={questionId}");
        }
    }

}
