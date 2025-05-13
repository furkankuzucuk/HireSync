namespace Project.Entities.DataTransferObjects
{
    public class ExamSubmissionDto
    {
        public int ExamId { get; set; }
        public List<UserAnswerInsertDto> Answers { get; set; }
    }
}