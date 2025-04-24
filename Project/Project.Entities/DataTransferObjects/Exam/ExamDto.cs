namespace Project.Entities.DataTransferObjects.Exam
{
    public record ExamDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
    }
}
