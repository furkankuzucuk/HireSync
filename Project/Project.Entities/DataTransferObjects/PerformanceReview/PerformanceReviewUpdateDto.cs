namespace Project.Entities.DataTransferObjects.PerformanceReview
{
    public record PerformanceReviewUpdateDto
    {
        public double AverageScore { get; set; }   // UserExam'lerin ortalaması
        public byte PerformanceRate { get; set; }  // 1-5 arası puan
        public string ReviewSummary { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
