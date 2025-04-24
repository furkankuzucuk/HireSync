namespace Project.Entities.DataTransferObjects.Exam
{
    public record ExamUpdateDto
    {
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
    }
}
