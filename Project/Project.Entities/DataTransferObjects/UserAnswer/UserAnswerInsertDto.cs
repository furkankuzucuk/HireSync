namespace Project.Entities.DataTransferObjects
{
    public class UserAnswerInsertDto
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }
}