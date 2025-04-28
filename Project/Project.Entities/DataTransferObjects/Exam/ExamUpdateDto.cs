namespace Project.Entities.DataTransferObjects.Exam
{
    public record ExamUpdateDto
    {
        public string ExamName { get; init; }
        public DateTime ExamDate { get; init; }
    }
}
