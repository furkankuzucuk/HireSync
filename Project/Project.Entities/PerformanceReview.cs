namespace Project.Entities;

public class PerformanceReview {
        public int PerformanceReviewId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public double AverageScore { get; set; }   // UserExam'lerin ortalaması
        public byte PerformanceRate { get; set; }  // 1-5 arası puan
        public string ReviewSummary { get; set; }
        public DateTime ReviewDate { get; set; }
}