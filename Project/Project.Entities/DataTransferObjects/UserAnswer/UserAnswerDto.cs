// UserAnswerInsertDto.cs
namespace Project.Entities.DataTransferObjects
{
    public class UserAnswerDto
    {
        public int UserAnswerId { get; set; }
        public int UserExamId { get; set; }
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; } // Kullanıcının seçtiği cevap
        public bool IsCorrect { get; set; } // Doğru cevaplanmış mı?
    }
}
