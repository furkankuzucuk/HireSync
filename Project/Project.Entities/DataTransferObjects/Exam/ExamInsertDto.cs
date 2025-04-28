namespace Project.Entities.DataTransferObjects.Exam
{
    public record ExamInsertDto
    {
        public string ExamName { get; init; }
        public DateTime ExamDate { get; init; }
    }
}
