namespace Project.Entities.DataTransferObjects.PerformanceReview
{
    public record PerformanceReviewDto
    {
        public int PerformanceReviewId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }  // Optional, if you want to include User name
        public byte PerformanceRate { get; set; }
        public string ReviewText { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; } // Optional, if you want to include Exam name
        public DateTime ReviewDate { get; set; }
    }
}
