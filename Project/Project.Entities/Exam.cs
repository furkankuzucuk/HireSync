namespace Project.Entities;

public class Exam {
     public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }

        public ICollection<UserExam> UserExams { get; set; }
        public ICollection<Question> Questions {get; set;}
}