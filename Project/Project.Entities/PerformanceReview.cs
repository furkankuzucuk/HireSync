namespace Project.Entities;

public class PerformanceReview {
    public int PerformanceReviewId { get; set; }  // Primary Key

    public int UserId { get; set; }   // Foreign Key
    public User User { get; set; }    // Navigation Property to User

    public byte PerformanceRate { get; set; }   // Tinyint (byte veri tipi)
    public string ReviewText { get; set; }      // Nvarchar(1000)

    public int ExamId { get; set; }     // Foreign Key
    public Exam Exam { get; set; }      // Navigation Property to Exam

    public DateTime ReviewDate { get; set; }   // DateTime
}