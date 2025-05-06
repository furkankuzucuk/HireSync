// UserExam.cs
namespace Project.Entities
{
    public class UserExam
    {
        public int UserExamId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public Dictionary<int, string> UserAnswers { get; set; } // QuestionId, UserAnswer şeklinde 
        public int Score { get; set; }
    }
}
