namespace Project.Entities
{
    public class UserAnswer
    {
        public int UserAnswerId { get; set; }
        
        public int UserExamId { get; set; }
        public UserExam UserExam { get; set; }
        
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
        public string SelectedAnswer { get; set; } // Kullanıcının seçtiği cevap
        public bool IsCorrect { get; set; } // Doğru cevaplanmış mı?
    }
}