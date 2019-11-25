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
        private delegate int Randomize();
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
            Randomize randomize = delegate ()
            {
                Random random = new Random();
                return random.Next();
            };
            var id = randomize();
            questionId = id;
            SqlDriver.Execute("INSERT INTO questions (userId,questionId,question,message) " +
                "values (@0,@1,@2,@3)", new ArrayList() { userId, questionId, question, message});
        }

        public void SaveReply()
        {
            Randomize randomize = delegate ()
            {
                Random random = new Random();
                return random.Next();
            };
            var id = randomize();
            answerId = id;
            SqlDriver.Execute("INSERT INTO answers (userId,answerId,questionId,answer) " +
                "values (@0,@1,@2,@3)", new ArrayList() { userIdReply, answerId, questionId, answer });
        }

        public List<Dictionary<string, object>> GetAllQuestions()
        {
            return SqlDriver.Fetch($"SELECT * FROM questions");
        }

        public List<Dictionary<string, object>> GetAllUserQuestions(int userId) {
            return SqlDriver.Fetch($"SELECT * FROM questions WHERE userId = {userId};");
        }

        public List<Dictionary<string, object>> GetAllUserRepliedQuestions(int userId)
        {
            return SqlDriver.Fetch($"SELECT questions.questionId, questions.userId, question, message FROM questions, answers WHERE answers.userId = {userId} AND answers.questionId = questions.questionId;");
        }

        public List<Dictionary<string, object>> GetAllReplies()
        {
            return SqlDriver.Fetch($"SELECT * FROM answers WHERE questionId={questionId}");
        }
    }

}
