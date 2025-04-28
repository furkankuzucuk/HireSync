namespace Project.Entities.DataTransferObjects.Exam
{
    public record ExamDto
    {
        public int ExamId { get; init; }
        public string ExamName { get; init; }
        public DateTime ExamDate { get; init; }
    }
}
