namespace Project.Entities.DataTransferObjects.PerformanceReview
{
    public record PerformanceReviewInsertDto
    {
        public int UserId { get; set; }
        public byte PerformanceRate { get; set; }
        public string ReviewText { get; set; }
        public int ExamId { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
